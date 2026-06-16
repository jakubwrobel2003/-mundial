using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using MundialPrediction.Core.Models;

namespace MundialPrediction.Infrastructure.ExternalApis;

// Klient Anthropic API – Claude analizuje dane z football-data.org,
// wyniki Tavily i fragmenty z RAG bazy wiedzy, zwracając pogłębioną analizę.
public class ClaudeAnalysisClient
{
    private readonly HttpClient _http;
    private readonly ILogger<ClaudeAnalysisClient> _logger;
    private readonly string _model;
    private readonly int _maxTokens;
    private readonly double _temperature;

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public ClaudeAnalysisClient(
        HttpClient http,
        ILogger<ClaudeAnalysisClient> logger,
        string model = "claude-sonnet-4-6",
        int maxTokens = 2000,
        double temperature = 0.3)
    {
        _http = http;
        _logger = logger;
        _model = model;
        _maxTokens = maxTokens;
        _temperature = temperature;
    }

    // Generuje pogłębioną analizę meczu na podstawie:
    // - predykcji z silnika Poisson (bazowe dane)
    // - aktualnych wiadomości z Tavily
    // - fragmentów z bazy wiedzy RAG
    // - realnych danych z football-data.org
    public async Task<string?> AnalyzeMatchAsync(
        Team home,
        Team away,
        string stage,
        string? tavilyHomeNews,
        string? tavilyAwayNews,
        string? tavilyH2H,
        string? ragContext,
        string? footballDataContext,
        string? homeRecentForm = null,
        string? awayRecentForm = null,
        string? homeQualifiers = null,
        string? awayQualifiers = null,
        List<Services.ClubPlayerStats>? homePlayerStats = null,
        List<Services.ClubPlayerStats>? awayPlayerStats = null)
    {
        _logger.LogInformation("Claude analiza: {Home} vs {Away}", home.Name, away.Name);

        var systemPrompt = """
            Jesteś ekspertem od analizy piłkarskiej MŚ 2026. Analizujesz dane statystyczne,
            aktualne wiadomości i historię meczów, żeby dać precyzyjną, obiektywną analizę.
            Nie powtarzaj danych wejściowych – syntetyzuj je w nowe spostrzeżenia.
            Pisz po polsku, zwięźle, konkretnie. Używaj liczb i faktów.
            """;

        var userContent = BuildAnalysisPrompt(home, away, stage,
            tavilyHomeNews, tavilyAwayNews, tavilyH2H, ragContext, footballDataContext,
            homeRecentForm, awayRecentForm, homeQualifiers, awayQualifiers,
            homePlayerStats, awayPlayerStats);

        var request = new ClaudeRequest
        {
            Model = _model,
            MaxTokens = _maxTokens,
            Temperature = _temperature,
            System = systemPrompt,
            Messages = new[]
            {
                new ClaudeMessage { Role = "user", Content = userContent }
            }
        };

        try
        {
            var resp = await _http.PostAsJsonAsync("v1/messages", request, JsonOpts);
            resp.EnsureSuccessStatusCode();
            var result = await resp.Content.ReadFromJsonAsync<ClaudeResponse>(JsonOpts);
            return result?.Content?.FirstOrDefault()?.Text;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Claude API error dla {Home} vs {Away}", home.Name, away.Name);
            return null;
        }
    }

    private static string BuildAnalysisPrompt(
        Team home, Team away, string stage,
        string? homeNews, string? awayNews, string? h2h,
        string? rag, string? footballData,
        string? homeRecentForm, string? awayRecentForm,
        string? homeQualifiers, string? awayQualifiers,
        List<Services.ClubPlayerStats>? homePlayerStats,
        List<Services.ClubPlayerStats>? awayPlayerStats)
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine($"## Mecz do analizy: {home.Name} ({home.FlagEmoji}) vs {away.Name} ({away.FlagEmoji})");
        sb.AppendLine($"Etap: {stage} | FIFA ranking: #{home.FifaRanking} vs #{away.FifaRanking}");
        sb.AppendLine();

        if (!string.IsNullOrWhiteSpace(homeRecentForm))
        {
            sb.AppendLine($"### EC/WC Historia: {home.Name}");
            sb.AppendLine(homeRecentForm);
            sb.AppendLine();
        }

        if (!string.IsNullOrWhiteSpace(awayRecentForm))
        {
            sb.AppendLine($"### EC/WC Historia: {away.Name}");
            sb.AppendLine(awayRecentForm);
            sb.AppendLine();
        }

        if (!string.IsNullOrWhiteSpace(homeQualifiers))
        {
            sb.AppendLine($"### Eliminacje i towarzyskie: {home.Name} (Tavily)");
            sb.AppendLine(homeQualifiers);
            sb.AppendLine();
        }

        if (!string.IsNullOrWhiteSpace(awayQualifiers))
        {
            sb.AppendLine($"### Eliminacje i towarzyskie: {away.Name} (Tavily)");
            sb.AppendLine(awayQualifiers);
            sb.AppendLine();
        }

        if (homePlayerStats?.Count > 0)
        {
            sb.AppendLine($"### Statystyki klubowe zawodników {home.Name} (sezon 2024/25)");
            foreach (var p in homePlayerStats) sb.AppendLine($"  • {p}");
            sb.AppendLine();
        }

        if (awayPlayerStats?.Count > 0)
        {
            sb.AppendLine($"### Statystyki klubowe zawodników {away.Name} (sezon 2024/25)");
            foreach (var p in awayPlayerStats) sb.AppendLine($"  • {p}");
            sb.AppendLine();
        }

        sb.AppendLine("### Statystyki bazowe (silnik Poisson)");
        sb.AppendLine($"{home.Name}: {home.GoalsPerGame} G/mecz, {home.GoalsConcededPerGame} stracone/mecz, {home.Possession}% posiadanie, forma: {home.RecentResults}");
        sb.AppendLine($"{away.Name}: {away.GoalsPerGame} G/mecz, {away.GoalsConcededPerGame} stracone/mecz, {away.Possession}% posiadanie, forma: {away.RecentResults}");
        sb.AppendLine($"Styl gry: {home.Name}={home.PrimaryStyle} vs {away.Name}={away.PrimaryStyle}");
        sb.AppendLine();

        if (!string.IsNullOrWhiteSpace(footballData))
        {
            sb.AppendLine("### Dane na żywo z football-data.org");
            sb.AppendLine(footballData);
            sb.AppendLine();
        }

        if (!string.IsNullOrWhiteSpace(homeNews))
        {
            sb.AppendLine($"### Aktualne wiadomości: {home.Name}");
            sb.AppendLine(homeNews);
            sb.AppendLine();
        }

        if (!string.IsNullOrWhiteSpace(awayNews))
        {
            sb.AppendLine($"### Aktualne wiadomości: {away.Name}");
            sb.AppendLine(awayNews);
            sb.AppendLine();
        }

        if (!string.IsNullOrWhiteSpace(h2h))
        {
            sb.AppendLine("### Historia bezpośrednich spotkań");
            sb.AppendLine(h2h);
            sb.AppendLine();
        }

        if (!string.IsNullOrWhiteSpace(rag))
        {
            sb.AppendLine("### Kontekst z bazy wiedzy");
            sb.AppendLine(rag);
            sb.AppendLine();
        }

        sb.AppendLine("---");
        sb.AppendLine("Na podstawie powyższych danych napisz analizę w następującym formacie:");
        sb.AppendLine("1. **Kluczowy czynnik** (1-2 zdania – co zdecyduje o wyniku)");
        sb.AppendLine("2. **Przewaga taktyczna** (kto i dlaczego ma przewagę)");
        sb.AppendLine("3. **Zagrożenia dla faworyta** (co może pójść nie tak)");
        sb.AppendLine("4. **Propozycje zakładowe** (konkretnie: Over/Under, kartki, czyste konto – z uzasadnieniem)");
        sb.AppendLine("5. **Mój wyrok** (wynik i krótkie podsumowanie w 1 zdaniu)");

        return sb.ToString();
    }
}

// --- DTOs Anthropic API ---

public class ClaudeRequest
{
    public string Model { get; set; } = string.Empty;
    public int MaxTokens { get; set; }
    public double Temperature { get; set; }
    public string? System { get; set; }
    public ClaudeMessage[] Messages { get; set; } = Array.Empty<ClaudeMessage>();
}

public class ClaudeMessage
{
    public string Role { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}

public class ClaudeResponse
{
    public List<ClaudeContent>? Content { get; set; }
    public string? StopReason { get; set; }
    public ClaudeUsage? Usage { get; set; }
}

public class ClaudeContent
{
    public string Type { get; set; } = string.Empty;
    public string? Text { get; set; }
}

public class ClaudeUsage
{
    public int InputTokens { get; set; }
    public int OutputTokens { get; set; }
}
