using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;
using MundialPrediction.Infrastructure.Database;
using MundialPrediction.Infrastructure.Database.Entities;
using MundialPrediction.Infrastructure.ExternalApis;

namespace MundialPrediction.Infrastructure.Services;

// Zbiera historię meczów reprezentacji z dostępnych kompetycji free tier:
//   - WC 2026: zagrane mecze turniejowe (na żywo, cache memory 2min)
//   - EC 2024: pełny turniej dla Europejczyków (SQLite cache 7 dni, dane nie zmienią się)
//
// Strategia cache (2 warstwy):
//   1. Sprawdź SQLite – jeśli świeże (EC: 7 dni, WC: 2 min z memory cache), użyj
//   2. Fetch z football-data.org (1 call na kompetycję, nie na drużynę!)
//   3. Zapisz do SQLite przed zwróceniem
//
// Koszt API: max 2 zapytania niezależnie od liczby drużyn w analizie.
public class TeamHistoryService
{
    private static readonly TimeSpan EcCacheTtl = TimeSpan.FromDays(7);

    private readonly FootballDataClient _fdClient;
    private readonly MundialDbContext _db;
    private readonly ILogger<TeamHistoryService> _logger;

    public TeamHistoryService(
        FootballDataClient fdClient,
        MundialDbContext db,
        ILogger<TeamHistoryService> logger)
    {
        _fdClient = fdClient;
        _db = db;
        _logger = logger;
    }

    public async Task<TeamRecentForm?> GetRecentFormAsync(string teamName, int limit = 20)
    {
        // Odkryj football-data.org ID z danych WC 2026 (jest już w memory cache)
        var fdId = await _fdClient.GetFdTeamIdAsync(teamName);
        if (fdId == null)
        {
            _logger.LogWarning("Brak FD ID dla drużyny: {Team}", teamName);
            return null;
        }

        // Pobierz mecze z obu źródeł równolegle
        var (wc2026, ec2024) = await FetchBothCompetitionsAsync();

        var allMatches = new List<RecentMatch>();
        allMatches.AddRange(ExtractTeamMatches(wc2026, fdId.Value, "FIFA World Cup 2026"));
        allMatches.AddRange(ExtractTeamMatches(ec2024, fdId.Value, "UEFA Euro 2024"));

        // Posortuj malejąco po dacie, ogranicz do limit
        var sorted = allMatches
            .Where(m => m.Date > DateTime.MinValue)
            .OrderByDescending(m => m.Date)
            .Take(limit)
            .ToList();

        if (sorted.Count == 0)
        {
            _logger.LogInformation("Brak historycznych meczów dla {Team} (fd:{Id}) w dostępnych kompetycjach", teamName, fdId.Value);
        }

        return new TeamRecentForm
        {
            TeamName = teamName,
            Played = sorted.Count,
            Won = sorted.Count(x => x.Result == "W"),
            Drawn = sorted.Count(x => x.Result == "D"),
            Lost = sorted.Count(x => x.Result == "L"),
            GoalsScored = sorted.Sum(x => x.GoalsFor),
            GoalsConceded = sorted.Sum(x => x.GoalsAgainst),
            Matches = sorted
        };
    }

    // Pobiera dane WC 2026 (memory cache) i EC 2024 (SQLite 7 dni → API) równolegle
    private async Task<(JsonNode? wc, JsonNode? ec)> FetchBothCompetitionsAsync()
    {
        var wcTask = _fdClient.GetWC2026MatchesAsync();
        var ecTask = GetEcWithSqliteCacheAsync();
        await Task.WhenAll(wcTask, ecTask);
        return (await wcTask, await ecTask);
    }

    // EC 2024: SQLite cache 7 dni (dane historyczne, nie zmienią się)
    private async Task<JsonNode?> GetEcWithSqliteCacheAsync()
    {
        const string key = "fd:ec:2024";
        var cached = await _db.ApiCache.FindAsync(key);
        if (cached != null && cached.ExpiresAt > DateTime.UtcNow)
        {
            _logger.LogDebug("Cache SQLite hit: EC 2024");
            return JsonNode.Parse(cached.ResponseJson);
        }

        _logger.LogInformation("Pobieram Euro 2024 z football-data.org (zostanie zapisane na 7 dni)");
        var data = await _fdClient.GetEuro2024MatchesAsync();
        if (data == null) return null;

        var json = data.ToJsonString();
        if (cached != null)
        {
            cached.ResponseJson = json;
            cached.CachedAt = DateTime.UtcNow;
            cached.ExpiresAt = DateTime.UtcNow.Add(EcCacheTtl);
            _db.ApiCache.Update(cached);
        }
        else
        {
            _db.ApiCache.Add(new ApiCacheEntity
            {
                CacheKey = key,
                ResponseJson = json,
                CachedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.Add(EcCacheTtl)
            });
        }
        await _db.SaveChangesAsync();
        return data;
    }

    // Filtruje mecze danej drużyny z odpowiedzi kompetycji
    private static List<RecentMatch> ExtractTeamMatches(JsonNode? competitionData, int fdTeamId, string competitionName)
    {
        var result = new List<RecentMatch>();
        var arr = competitionData?["matches"]?.AsArray();
        if (arr == null) return result;

        foreach (var m in arr)
        {
            if (m?["status"]?.ToString() != "FINISHED") continue;

            var homeId = m?["homeTeam"]?["id"]?.GetValue<int?>() ?? 0;
            var awayId = m?["awayTeam"]?["id"]?.GetValue<int?>() ?? 0;
            if (homeId != fdTeamId && awayId != fdTeamId) continue;

            var homeName = m?["homeTeam"]?["shortName"]?.ToString()
                        ?? m?["homeTeam"]?["name"]?.ToString() ?? "?";
            var awayName = m?["awayTeam"]?["shortName"]?.ToString()
                        ?? m?["awayTeam"]?["name"]?.ToString() ?? "?";

            var homeGoals = m?["score"]?["fullTime"]?["home"]?.GetValue<int?>() ?? 0;
            var awayGoals = m?["score"]?["fullTime"]?["away"]?.GetValue<int?>() ?? 0;

            var isHome = homeId == fdTeamId;
            var gf = isHome ? homeGoals : awayGoals;
            var ga = isHome ? awayGoals : homeGoals;
            var opponent = isHome ? awayName : homeName;

            var stage = m?["stage"]?.ToString() ?? "";
            var result2 = gf > ga ? "W" : gf < ga ? "L" : "D";

            var dateStr = m?["utcDate"]?.ToString();
            var date = DateTime.TryParse(dateStr, out var d) ? d : DateTime.MinValue;

            result.Add(new RecentMatch
            {
                Date = date,
                Opponent = opponent,
                Result = result2,
                GoalsFor = gf,
                GoalsAgainst = ga,
                Competition = $"{competitionName} ({FormatStage(stage)})",
                IsHome = isHome
            });
        }
        return result;
    }

    private static string FormatStage(string stage) => stage switch
    {
        "GROUP_STAGE" => "Faza grupowa",
        "LAST_16" => "1/8 finału",
        "LAST_32" => "1/16 finału",
        "QUARTER_FINALS" => "Ćwierćfinał",
        "SEMI_FINALS" => "Półfinał",
        "FINAL" => "Finał",
        _ => stage
    };

    public static string BuildFormContext(TeamRecentForm form)
    {
        if (form.Played == 0)
            return "Brak dostępnych danych historycznych w tej edycji (WC 2026 jeszcze nie zagrał / drużyna spoza UEFA).";

        var sb = new StringBuilder();
        sb.AppendLine($"Ostatnie {form.Played} meczów: {form.Won}W {form.Drawn}D {form.Lost}L | " +
                      $"Gole zdobyte: {form.GoalsScored} ({(double)form.GoalsScored / form.Played:F2}/mecz) | " +
                      $"Gole stracone: {form.GoalsConceded} ({(double)form.GoalsConceded / form.Played:F2}/mecz)");

        foreach (var m in form.Matches)
        {
            var venue = m.IsHome ? "D" : "W";
            sb.AppendLine($"  {m.Date:yyyy-MM-dd} [{venue}] vs {m.Opponent,-18} {m.GoalsFor}-{m.GoalsAgainst} ({m.Result})  {m.Competition}");
        }
        return sb.ToString();
    }
}

public class TeamRecentForm
{
    public string TeamName { get; set; } = string.Empty;
    public int Played { get; set; }
    public int Won { get; set; }
    public int Drawn { get; set; }
    public int Lost { get; set; }
    public int GoalsScored { get; set; }
    public int GoalsConceded { get; set; }
    public List<RecentMatch> Matches { get; set; } = new();
}

public class RecentMatch
{
    public DateTime Date { get; set; }
    public string Opponent { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public string Competition { get; set; } = string.Empty;
    public bool IsHome { get; set; }
}
