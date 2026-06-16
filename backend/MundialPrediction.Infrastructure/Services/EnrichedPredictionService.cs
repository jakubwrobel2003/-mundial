using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MundialPrediction.Core.Interfaces;
using MundialPrediction.Core.Models;
using MundialPrediction.Infrastructure.Database;
using MundialPrediction.Infrastructure.Database.Entities;
using MundialPrediction.Infrastructure.ExternalApis;
using MundialPrediction.Infrastructure.RAG;

namespace MundialPrediction.Infrastructure.Services;

// Orkiestruje pełny pipeline analizy meczu:
// 1. Predykcja bazowa (Poisson engine)
// 2. Historia ostatnich 20 meczów obu drużyn (football-data.org + SQLite cache 24h)
// 3. Dane na żywo z WC 2026 (wynik jeśli mecz już rozegrany)
// 4. Wyszukiwanie w internecie przez Tavily
// 5. RAG – kontekst z bazy wiedzy
// 6. Synteza przez Claude
// 7. Zapis historii do SQLite
public class EnrichedPredictionService
{
    private readonly IPredictionService _basePrediction;
    private readonly FootballDataClient _footballData;
    private readonly TavilySearchClient _tavily;
    private readonly ClaudeAnalysisClient _claude;
    private readonly KnowledgeBase _rag;
    private readonly TeamHistoryService _teamHistory;
    private readonly PlayerClubStatsService _playerStats;
    private readonly MundialDbContext _db;
    private readonly ILogger<EnrichedPredictionService> _logger;

    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = false };

    public EnrichedPredictionService(
        IPredictionService basePrediction,
        FootballDataClient footballData,
        TavilySearchClient tavily,
        ClaudeAnalysisClient claude,
        KnowledgeBase rag,
        TeamHistoryService teamHistory,
        PlayerClubStatsService playerStats,
        MundialDbContext db,
        ILogger<EnrichedPredictionService> logger)
    {
        _basePrediction = basePrediction;
        _footballData = footballData;
        _tavily = tavily;
        _claude = claude;
        _rag = rag;
        _teamHistory = teamHistory;
        _playerStats = playerStats;
        _db = db;
        _logger = logger;
    }

    public async Task<EnrichedMatchPrediction> AnalyzeAsync(
        string homeTeamId,
        string awayTeamId,
        string stage = "Group",
        bool useWeb = true,
        bool useFootballData = true)
    {
        // 1. Bazowa predykcja z lokalnego silnika Poisson
        var prediction = await _basePrediction.PredictCustomMatchAsync(homeTeamId, awayTeamId, stage);
        var home = prediction.HomeTeam;
        var away = prediction.AwayTeam;

        // 2. Historia ostatnich 20 meczów – SQLite cache (24h), potem API
        //    Oba pobieramy równolegle; rate limiter i tak kolejkuje jeśli limit wyczerpany
        TeamRecentForm? homeForm = null;
        TeamRecentForm? awayForm = null;

        if (useFootballData)
        {
            var (hf, af) = await FetchBothFormsAsync(home.Name, away.Name);
            homeForm = hf;
            awayForm = af;
        }

        // 3. Statystyki klubowe kluczowych zawodników (SQLite 24h per liga)
        var homePlayerStats = await _playerStats.GetTeamPlayersStatsAsync(
            home.Players.Select(p => (p.Name, p.Club, p.IsKeyStar)));
        var awayPlayerStats = await _playerStats.GetTeamPlayersStatsAsync(
            away.Players.Select(p => (p.Name, p.Club, p.IsKeyStar)));

        // 4. Wynik konkretnego meczu WC 2026 + Tavily (równolegle)
        string? footballDataContext = null;
        string? tavilyHomeNews = null;
        string? tavilyAwayNews = null;
        string? tavilyH2H = null;
        string? tavilyHomeQualifiers = null;
        string? tavilyAwayQualifiers = null;

        Task<string?> fdTask = Task.FromResult<string?>(null);
        Task<string?> homeNewsTask = Task.FromResult<string?>(null);
        Task<string?> awayNewsTask = Task.FromResult<string?>(null);
        Task<string?> h2hTask = Task.FromResult<string?>(null);
        Task<string?> homeQualTask = Task.FromResult<string?>(null);
        Task<string?> awayQualTask = Task.FromResult<string?>(null);

        if (useFootballData)
            fdTask = GetFootballDataContextAsync(home.Name, away.Name);

        if (useWeb)
        {
            homeNewsTask = GetTavilyContextAsync(home.Name, "news");
            awayNewsTask = GetTavilyContextAsync(away.Name, "news");
            h2hTask = GetTavilyContextAsync($"{home.Name} {away.Name}", "h2h");
            homeQualTask = GetQualifiersContextAsync(homeTeamId, home.Name);
            awayQualTask = GetQualifiersContextAsync(awayTeamId, away.Name);
        }

        await Task.WhenAll(fdTask, homeNewsTask, awayNewsTask, h2hTask, homeQualTask, awayQualTask);

        footballDataContext = await fdTask;
        tavilyHomeNews = await homeNewsTask;
        tavilyAwayNews = await awayNewsTask;
        tavilyH2H = await h2hTask;
        tavilyHomeQualifiers = await homeQualTask;
        tavilyAwayQualifiers = await awayQualTask;

        // Zapisz do RAG: wiadomości (6h) i kwalifikacje (7 dni – zmieniają się rzadko)
        if (!string.IsNullOrWhiteSpace(tavilyHomeNews))
            await _rag.UpsertAsync(homeTeamId, "team_news", $"{home.Name} – aktualne wiadomości",
                tavilyHomeNews, validFor: TimeSpan.FromHours(6));
        if (!string.IsNullOrWhiteSpace(tavilyAwayNews))
            await _rag.UpsertAsync(awayTeamId, "team_news", $"{away.Name} – aktualne wiadomości",
                tavilyAwayNews, validFor: TimeSpan.FromHours(6));
        if (!string.IsNullOrWhiteSpace(tavilyHomeQualifiers))
            await _rag.UpsertAsync(homeTeamId, "qualifiers", $"{home.Name} – eliminacje/towarzyskie",
                tavilyHomeQualifiers, validFor: TimeSpan.FromDays(7));
        if (!string.IsNullOrWhiteSpace(tavilyAwayQualifiers))
            await _rag.UpsertAsync(awayTeamId, "qualifiers", $"{away.Name} – eliminacje/towarzyskie",
                tavilyAwayQualifiers, validFor: TimeSpan.FromDays(7));

        // 3. RAG – pobierz kontekst z bazy wiedzy
        var ragQuery = $"{home.Name} {away.Name} {stage} World Cup 2026";
        var ragDocs = await _rag.SearchAsync(ragQuery);

        // Dorzuć też dokumenty per-drużyna
        var homeRag = await _rag.GetByEntityAsync(homeTeamId);
        var awayRag = await _rag.GetByEntityAsync(awayTeamId);
        ragDocs.AddRange(homeRag);
        ragDocs.AddRange(awayRag);

        var ragContext = ragDocs.Count > 0 ? KnowledgeBase.BuildContext(ragDocs.DistinctBy(r => r.Title)) : null;

        // 6. Synteza przez Claude
        string? homeFormContext = homeForm != null ? TeamHistoryService.BuildFormContext(homeForm) : null;
        string? awayFormContext = awayForm != null ? TeamHistoryService.BuildFormContext(awayForm) : null;

        string? claudeAnalysis = null;
        if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY")))
        {
            claudeAnalysis = await _claude.AnalyzeMatchAsync(
                home, away, stage,
                tavilyHomeNews, tavilyAwayNews, tavilyH2H,
                ragContext, footballDataContext,
                homeFormContext, awayFormContext,
                tavilyHomeQualifiers, tavilyAwayQualifiers,
                homePlayerStats, awayPlayerStats);
        }

        // 6. Zapisz do historii
        var entity = new PredictionHistoryEntity
        {
            HomeTeamId = homeTeamId,
            AwayTeamId = awayTeamId,
            Stage = stage,
            HomeWinProbability = prediction.HomeWinProbability,
            DrawProbability = prediction.DrawProbability,
            AwayWinProbability = prediction.AwayWinProbability,
            HomeExpectedGoals = prediction.HomeExpectedGoals,
            AwayExpectedGoals = prediction.AwayExpectedGoals,
            PredictedHomeGoals = prediction.PredictedHomeGoals,
            PredictedAwayGoals = prediction.PredictedAwayGoals,
            PredictionJson = JsonSerializer.Serialize(prediction, JsonOpts),
            ClaudeAnalysis = claudeAnalysis,
            UsedFootballData = footballDataContext != null,
            UsedTavily = tavilyHomeNews != null || tavilyAwayNews != null,
            UsedRag = ragDocs.Count > 0
        };
        _db.PredictionHistory.Add(entity);
        await _db.SaveChangesAsync();

        return new EnrichedMatchPrediction
        {
            BasePrediction = prediction,
            ClaudeAnalysis = claudeAnalysis,
            FootballDataContext = footballDataContext,
            HomeRecentForm = homeFormContext,
            AwayRecentForm = awayFormContext,
            HomeFormData = homeForm,
            AwayFormData = awayForm,
            HomeFormStats = homeForm != null ? $"{homeForm.Won}W {homeForm.Drawn}D {homeForm.Lost}L | {homeForm.GoalsScored}:{homeForm.GoalsConceded}" : null,
            AwayFormStats = awayForm != null ? $"{awayForm.Won}W {awayForm.Drawn}D {awayForm.Lost}L | {awayForm.GoalsScored}:{awayForm.GoalsConceded}" : null,
            HomePlayerClubStats = homePlayerStats,
            AwayPlayerClubStats = awayPlayerStats,
            TavilyHomeNews = tavilyHomeNews,
            TavilyAwayNews = tavilyAwayNews,
            TavilyH2H = tavilyH2H,
            TavilyHomeQualifiers = tavilyHomeQualifiers,
            TavilyAwayQualifiers = tavilyAwayQualifiers,
            RagDocumentsUsed = ragDocs.Count,
            DataSourcesUsed = BuildSourcesList(footballDataContext, homeForm, homePlayerStats.Count, tavilyHomeNews, tavilyAwayNews, ragDocs.Count)
        };
    }

    // Zwraca historię predykcji dla pary drużyn
    public async Task<List<PredictionHistoryEntity>> GetHistoryAsync(string homeId, string awayId)
    {
        return await _db.PredictionHistory
            .Where(h => h.HomeTeamId == homeId && h.AwayTeamId == awayId)
            .OrderByDescending(h => h.CreatedAt)
            .Take(10)
            .ToListAsync();
    }

    // Pozwala ręcznie dodać dokument do RAG bazy wiedzy
    public async Task AddKnowledgeAsync(string entityId, string docType, string title, string content, string? sourceUrl = null)
    {
        await _rag.UpsertAsync(entityId, docType, title, content, sourceUrl);
    }

    private async Task<string?> GetFootballDataContextAsync(string homeName, string awayName)
    {
        try
        {
            var matches = await _footballData.GetMatchesAsync();
            if (matches == null) return null;

            // Znajdź mecz w danych football-data.org
            var matchesArr = matches["matches"]?.AsArray();
            if (matchesArr == null) return null;

            var found = matchesArr.FirstOrDefault(m =>
            {
                var homeTeam = m?["homeTeam"]?["name"]?.ToString() ?? "";
                var awayTeam = m?["awayTeam"]?["name"]?.ToString() ?? "";
                return (homeTeam.Contains(homeName, StringComparison.OrdinalIgnoreCase) ||
                        awayTeam.Contains(homeName, StringComparison.OrdinalIgnoreCase)) &&
                       (homeTeam.Contains(awayName, StringComparison.OrdinalIgnoreCase) ||
                        awayTeam.Contains(awayName, StringComparison.OrdinalIgnoreCase));
            });

            if (found == null) return $"Brak meczu {homeName} vs {awayName} w football-data.org";

            return $"Status: {found["status"]} | " +
                   $"Data: {found["utcDate"]} | " +
                   $"Wynik: {found["score"]?["fullTime"]?["home"]} - {found["score"]?["fullTime"]?["away"]} | " +
                   $"Faza: {found["stage"]} | Stadion: {found["venue"]}";
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "FootballData context error");
            return null;
        }
    }

    private async Task<string?> GetTavilyContextAsync(string query, string type)
    {
        TavilyResult? result = type switch
        {
            "h2h" => await _tavily.SearchHeadToHeadAsync(query.Split(' ')[0], string.Join(' ', query.Split(' ').Skip(1))),
            _ => await _tavily.SearchTeamNewsAsync(query)
        };
        return FormatTavilyResult(result);
    }

    // Kwalifikacje i towarzyskie – sprawdza RAG najpierw (TTL 7 dni), Tavily jeśli brak
    private async Task<string?> GetQualifiersContextAsync(string teamId, string teamName)
    {
        var existing = await _rag.GetByEntityAsync(teamId);
        var qualDoc = existing.FirstOrDefault(d => d.DocType == "qualifiers");
        if (qualDoc != null)
        {
            _logger.LogDebug("RAG hit: kwalifikacje {Team}", teamName);
            return qualDoc.Content;
        }

        var result = await _tavily.SearchQualifiersAndFriendliesAsync(teamName);
        return FormatTavilyResult(result);
    }

    private static string? FormatTavilyResult(TavilyResult? result)
    {
        if (result == null) return null;
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(result.Answer))
            parts.Add(result.Answer);
        foreach (var item in result.Results.Take(3))
            parts.Add($"• {item.Title}: {item.Content[..Math.Min(200, item.Content.Length)]}...");
        return parts.Count > 0 ? string.Join("\n", parts) : null;
    }

    private async Task<(TeamRecentForm? home, TeamRecentForm? away)> FetchBothFormsAsync(string homeName, string awayName)
    {
        // Pobieramy sekwencyjnie – jeden mecz wymaga 1 API call, dwa wymagają 2.
        // Rate limiter kolejkuje jeśli wyczerpany (czeka na reset okna 60s).
        var home = await _teamHistory.GetRecentFormAsync(homeName);
        var away = await _teamHistory.GetRecentFormAsync(awayName);
        return (home, away);
    }

    private static List<string> BuildSourcesList(string? fd, TeamRecentForm? form, int playerStatsCount, string? homeNews, string? awayNews, int ragCount)
    {
        var sources = new List<string> { "Poisson xG Engine (local)" };
        if (form != null && form.Played > 0) sources.Add($"football-data.org EC/WC historia ({form.Played} meczów)");
        if (playerStatsCount > 0) sources.Add($"football-data.org statystyki klubowe ({playerStatsCount} zawodników)");
        if (fd != null) sources.Add("football-data.org (wynik WC 2026)");
        if (homeNews != null || awayNews != null) sources.Add("Tavily wiadomości");
        if (ragCount > 0) sources.Add($"RAG Knowledge Base ({ragCount} docs)");
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY")))
            sources.Add("Claude AI Synthesis");
        return sources;
    }
}

public class EnrichedMatchPrediction
{
    public MatchPrediction BasePrediction { get; set; } = null!;
    public string? ClaudeAnalysis { get; set; }
    public string? FootballDataContext { get; set; }
    public string? HomeRecentForm { get; set; }
    public string? AwayRecentForm { get; set; }
    public TeamRecentForm? HomeFormData { get; set; }
    public TeamRecentForm? AwayFormData { get; set; }
    public string? HomeFormStats { get; set; }
    public string? AwayFormStats { get; set; }
    public List<ClubPlayerStats> HomePlayerClubStats { get; set; } = new();
    public List<ClubPlayerStats> AwayPlayerClubStats { get; set; } = new();
    public string? TavilyHomeNews { get; set; }
    public string? TavilyAwayNews { get; set; }
    public string? TavilyH2H { get; set; }
    public string? TavilyHomeQualifiers { get; set; }
    public string? TavilyAwayQualifiers { get; set; }
    public int RagDocumentsUsed { get; set; }
    public List<string> DataSourcesUsed { get; set; } = new();
}
