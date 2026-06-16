using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;
using MundialPrediction.Infrastructure.Database;
using MundialPrediction.Infrastructure.Database.Entities;
using MundialPrediction.Infrastructure.ExternalApis;

namespace MundialPrediction.Infrastructure.Services;

// Pobiera statystyki klubowe kluczowych zawodników z lig dostępnych w free tierze.
// Strategia:
//   1. SQLite cache (24h) – nie traf w API przy każdej predykcji
//   2. 1 call na ligę (limit=50 scorerów) – obejmuje większość gwiazd
//   3. Match po nazwie gracza (fuzzy: contains + odwrócona kolejność imion)
public class PlayerClubStatsService
{
    private static readonly TimeSpan CacheTtl = TimeSpan.FromHours(24);
    private const int Season = 2024;  // sezon 2024/25

    // Mapowanie fragmentu nazwy klubu → kod ligi football-data.org
    private static readonly List<(string clubFragment, string competitionCode)> ClubLeagueMap = new()
    {
        // Premier League
        ("Liverpool", "PL"), ("Manchester City", "PL"), ("Arsenal", "PL"),
        ("Manchester United", "PL"), ("Chelsea", "PL"), ("Tottenham", "PL"),
        ("Newcastle", "PL"), ("Aston Villa", "PL"), ("Brighton", "PL"),
        ("West Ham", "PL"), ("Everton", "PL"), ("Brentford", "PL"),
        // La Liga
        ("Real Madrid", "PD"), ("Barcelona", "PD"), ("Atletico", "PD"),
        ("Sevilla", "PD"), ("Valencia", "PD"), ("Villarreal", "PD"),
        ("Athletic Club", "PD"), ("Real Sociedad", "PD"), ("Betis", "PD"),
        // Bundesliga
        ("Bayern", "BL1"), ("Dortmund", "BL1"), ("RB Leipzig", "BL1"),
        ("Leverkusen", "BL1"), ("Frankfurt", "BL1"), ("Stuttgart", "BL1"),
        ("Wolfsburg", "BL1"), ("Gladbach", "BL1"),
        // Serie A
        ("Inter", "SA"), ("Juventus", "SA"), ("AC Milan", "SA"),
        ("Napoli", "SA"), ("Roma", "SA"), ("Lazio", "SA"),
        ("Atalanta", "SA"), ("Fiorentina", "SA"), ("Torino", "SA"),
        // Ligue 1
        ("Paris Saint-Germain", "FL1"), ("PSG", "FL1"), ("Monaco", "FL1"),
        ("Lyon", "FL1"), ("Marseille", "FL1"), ("Lille", "FL1"),
        ("Lens", "FL1"), ("Nice", "FL1"),
        // Eredivisie
        ("Ajax", "DED"), ("PSV", "DED"), ("Feyenoord", "DED"), ("AZ", "DED"),
        // Primeira Liga
        ("Benfica", "PPL"), ("Porto", "PPL"), ("Sporting CP", "PPL"),
        ("Sporting", "PPL"), ("Braga", "PPL"),
        // Champions League – jako fallback dla graczy z mniej znanych lig
        // (CL scorers obejmują graczy z całej Europy)
    };

    // Wewnętrzny cache konkurencji: code → lista (playerName, teamName, goals, assists)
    private readonly Dictionary<string, List<ScorerEntry>> _scorerCache = new();
    private readonly SemaphoreSlim _fetchLock = new(1, 1);

    private readonly FootballDataClient _fdClient;
    private readonly MundialDbContext _db;
    private readonly ILogger<PlayerClubStatsService> _logger;

    public PlayerClubStatsService(
        FootballDataClient fdClient,
        MundialDbContext db,
        ILogger<PlayerClubStatsService> logger)
    {
        _fdClient = fdClient;
        _db = db;
        _logger = logger;
    }

    // Zwraca statystyki klubowe gracza lub null jeśli nie znaleziono.
    public async Task<ClubPlayerStats?> GetPlayerStatsAsync(string playerName, string clubName)
    {
        var competitionCode = ResolveCompetition(clubName);
        if (competitionCode == null)
        {
            _logger.LogDebug("Brak mapowania ligi dla klubu: {Club}", clubName);
            return null;
        }

        var scorers = await GetScorersAsync(competitionCode);
        var match = FindPlayer(scorers, playerName);
        if (match == null) return null;

        return new ClubPlayerStats
        {
            PlayerName = playerName,
            Club = clubName,
            Competition = CompetitionName(competitionCode),
            Goals = match.Goals,
            Assists = match.Assists,
            Penalties = match.Penalties
        };
    }

    // Pobiera listę graczy z ich statystykami klubowymi dla podanej drużyny (top KeyStars)
    public async Task<List<ClubPlayerStats>> GetTeamPlayersStatsAsync(
        IEnumerable<(string name, string club, bool isStar)> players, int topN = 6)
    {
        var targets = players
            .OrderByDescending(p => p.isStar)
            .Take(topN)
            .ToList();

        var results = new List<ClubPlayerStats>();
        foreach (var (name, club, _) in targets)
        {
            var stats = await GetPlayerStatsAsync(name, club);
            if (stats != null) results.Add(stats);
        }
        return results;
    }

    // --- prywatne ---

    private async Task<List<ScorerEntry>> GetScorersAsync(string competitionCode)
    {
        // Sprawdź memory cache
        if (_scorerCache.TryGetValue(competitionCode, out var cached))
            return cached;

        await _fetchLock.WaitAsync();
        try
        {
            if (_scorerCache.TryGetValue(competitionCode, out cached))
                return cached;

            // Sprawdź SQLite
            var key = $"fd:scorers:{competitionCode}:{Season}";
            var dbEntry = await _db.ApiCache.FindAsync(key);
            if (dbEntry != null && dbEntry.ExpiresAt > DateTime.UtcNow)
            {
                _logger.LogDebug("Cache SQLite hit: scorerzy {Code}", competitionCode);
                var fromDb = ParseScorers(JsonNode.Parse(dbEntry.ResponseJson));
                _scorerCache[competitionCode] = fromDb;
                return fromDb;
            }

            // Pobierz z API
            _logger.LogInformation("Pobieram scorerów {Code} sezon {Season}", competitionCode, Season);
            var data = await _fdClient.GetLeagueScorersAsync(competitionCode, Season);
            if (data == null)
            {
                _scorerCache[competitionCode] = new();
                return new();
            }

            var json = data.ToJsonString();
            var parsed = ParseScorers(data);

            // Zapisz do SQLite
            if (dbEntry != null)
            {
                dbEntry.ResponseJson = json;
                dbEntry.CachedAt = DateTime.UtcNow;
                dbEntry.ExpiresAt = DateTime.UtcNow.Add(CacheTtl);
                _db.ApiCache.Update(dbEntry);
            }
            else
            {
                _db.ApiCache.Add(new ApiCacheEntity
                {
                    CacheKey = key,
                    ResponseJson = json,
                    CachedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.Add(CacheTtl)
                });
            }
            await _db.SaveChangesAsync();

            _scorerCache[competitionCode] = parsed;
            return parsed;
        }
        finally
        {
            _fetchLock.Release();
        }
    }

    private static List<ScorerEntry> ParseScorers(JsonNode? data)
    {
        var result = new List<ScorerEntry>();
        var arr = data?["scorers"]?.AsArray();
        if (arr == null) return result;

        foreach (var s in arr)
        {
            var name = s?["player"]?["name"]?.ToString();
            var team = s?["team"]?["name"]?.ToString() ?? "";
            var goals = s?["goals"]?.GetValue<int?>() ?? 0;
            var assists = s?["assists"]?.GetValue<int?>() ?? 0;
            var penalties = s?["penalties"]?.GetValue<int?>() ?? 0;
            if (name != null)
                result.Add(new ScorerEntry(name, team, goals, assists, penalties));
        }
        return result;
    }

    private static ScorerEntry? FindPlayer(List<ScorerEntry> scorers, string playerName)
    {
        // Exact match first
        var exact = scorers.FirstOrDefault(s =>
            string.Equals(s.Name, playerName, StringComparison.OrdinalIgnoreCase));
        if (exact != null) return exact;

        // Last name match (np. "Kylian Mbappé" → szukaj "Mbapp")
        var parts = playerName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var lastName = parts.Last();
        return scorers.FirstOrDefault(s =>
            s.Name.Contains(lastName, StringComparison.OrdinalIgnoreCase) ||
            playerName.Contains(s.Name.Split(' ').Last(), StringComparison.OrdinalIgnoreCase));
    }

    private static string? ResolveCompetition(string clubName)
    {
        foreach (var (fragment, code) in ClubLeagueMap)
            if (clubName.Contains(fragment, StringComparison.OrdinalIgnoreCase))
                return code;
        return null;
    }

    private static string CompetitionName(string code) => code switch
    {
        "PL" => "Premier League 24/25",
        "PD" => "La Liga 24/25",
        "BL1" => "Bundesliga 24/25",
        "SA" => "Serie A 24/25",
        "FL1" => "Ligue 1 24/25",
        "DED" => "Eredivisie 24/25",
        "PPL" => "Primeira Liga 24/25",
        "CL" => "Champions League 24/25",
        _ => code
    };

    private record ScorerEntry(string Name, string Team, int Goals, int Assists, int Penalties);
}

public class ClubPlayerStats
{
    public string PlayerName { get; set; } = string.Empty;
    public string Club { get; set; } = string.Empty;
    public string Competition { get; set; } = string.Empty;
    public int Goals { get; set; }
    public int Assists { get; set; }
    public int Penalties { get; set; }

    public override string ToString() =>
        $"{PlayerName} ({Club}): {Goals}G {Assists}A {(Penalties > 0 ? $"({Penalties}pk) " : "")}{Competition}";
}
