using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundialPrediction.Core.Interfaces;
using MundialPrediction.Infrastructure.Database;
using MundialPrediction.Infrastructure.Database.Entities;
using MundialPrediction.Infrastructure.ExternalApis;
using System.Text.Json;

namespace MundialPrediction.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly FootballDataClient _fd;
    private readonly ITeamRepository _teams;
    private readonly MundialDbContext _db;
    private readonly ILogger<MatchesController> _logger;

    private static readonly JsonSerializerOptions _camel =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    private const string SCHEDULE_CACHE_KEY = "wc2026:schedule:v2";
    private static readonly TimeSpan SCHEDULE_TTL = TimeSpan.FromHours(6);

    // Mapowanie stage kodów fd.org → nasze etapy
    private static readonly Dictionary<string, string> StageMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["GROUP_STAGE"]    = "Group",
        ["ROUND_OF_32"]    = "Round of 32",
        ["ROUND_OF_16"]    = "Round of 16",
        ["QUARTER_FINALS"] = "Quarter-Final",
        ["SEMI_FINALS"]    = "Semi-Final",
        ["THIRD_PLACE"]    = "Semi-Final",
        ["FINAL"]          = "Final",
    };

    public MatchesController(FootballDataClient fd, ITeamRepository teams, MundialDbContext db, ILogger<MatchesController> logger)
    {
        _fd = fd;
        _teams = teams;
        _db = db;
        _logger = logger;
    }

    /// <summary>
    /// Terminarz MŚ 2026 z football-data.org, zmapowany na wewnętrzne ID drużyn.
    /// </summary>
    [HttpGet("wc2026")]
    public async Task<IActionResult> GetSchedule()
    {
        try
        {
            // SQLite persistent cache – przeżywa restarty Railway (TTL 6h)
            var sqlCached = await _db.ApiCache
                .Where(c => c.CacheKey == SCHEDULE_CACHE_KEY && c.ExpiresAt > DateTime.UtcNow)
                .FirstOrDefaultAsync();

            if (sqlCached != null)
            {
                _logger.LogInformation("SQLite cache hit: terminarz WC 2026");
                var cached = JsonSerializer.Deserialize<List<WcMatchDto>>(sqlCached.ResponseJson, _camel);
                return Ok(cached);
            }

            var raw = await _fd.GetMatchesAsync();
            if (raw == null)
                return StatusCode(503, new { error = "football-data.org niedostępne lub brak klucza API" });

            var allTeams = (await _teams.GetAllTeamsAsync()).ToList();

            // Budujemy szybką mapę: fdName → nasz Team (fuzzy)
            var fdNameToTeam = new Dictionary<string, Core.Models.Team>(StringComparer.OrdinalIgnoreCase);
            foreach (var t in allTeams)
                fdNameToTeam[t.Name] = t;

            string? ResolveTeamId(string? fdName)
            {
                if (string.IsNullOrEmpty(fdName)) return null;
                if (fdNameToTeam.TryGetValue(fdName, out var exact)) return exact.Id;
                var partial = allTeams.FirstOrDefault(t =>
                    t.Name.Contains(fdName, StringComparison.OrdinalIgnoreCase) ||
                    fdName.Contains(t.Name, StringComparison.OrdinalIgnoreCase) ||
                    t.ShortName.Equals(fdName, StringComparison.OrdinalIgnoreCase));
                return partial?.Id;
            }

            var matchesArr = raw["matches"]?.AsArray();
            if (matchesArr == null) return Ok(Array.Empty<object>());

            _logger.LogInformation("fd.org zwrócił {Total} meczów WC 2026", matchesArr.Count);

            var result = new List<WcMatchDto>();

            foreach (var m in matchesArr)
            {
                if (m == null) continue;

                var fdStage  = m["stage"]?.ToString() ?? "";
                var fdGroup  = m["group"]?.ToString();
                var utcDate  = m["utcDate"]?.ToString();
                var status   = m["status"]?.ToString() ?? "TIMED";
                var matchday = m["matchday"]?.GetValue<int?>() ?? 0;

                var homefdName = m["homeTeam"]?["name"]?.ToString();
                var awayfdName = m["awayTeam"]?["name"]?.ToString();

                // TBD = drużyna jeszcze nieznana (faza pucharowa) – pokazuj ale oznacz known=false
                var homeTbd = string.IsNullOrEmpty(homefdName) || homefdName == "TBD";
                var awayTbd = string.IsNullOrEmpty(awayfdName) || awayfdName == "TBD";

                var homeId = homeTbd ? null : ResolveTeamId(homefdName);
                var awayId = awayTbd ? null : ResolveTeamId(awayfdName);

                var homeTeam = homeId != null ? allTeams.FirstOrDefault(t => t.Id == homeId) : null;
                var awayTeam = awayId != null ? allTeams.FirstOrDefault(t => t.Id == awayId) : null;

                var scoreHome = m["score"]?["fullTime"]?["home"]?.GetValue<int?>();
                var scoreAway = m["score"]?["fullTime"]?["away"]?.GetValue<int?>();

                result.Add(new WcMatchDto
                {
                    Matchday    = matchday,
                    Stage       = StageMap.TryGetValue(fdStage, out var s) ? s : fdStage,
                    FdStage     = fdStage,
                    Group       = fdGroup?.Replace("GROUP_", "Grupa "),
                    UtcDate     = utcDate,
                    Status      = status,
                    HomeTeam    = new WcTeamRef
                    {
                        Id        = homeId,
                        FdName    = homefdName ?? "TBD",
                        Name      = homeTbd ? "TBD" : (homeTeam?.Name ?? homefdName ?? "?"),
                        FlagEmoji = homeTbd ? "🏳️" : (homeTeam?.FlagEmoji ?? "🏳️"),
                        Known     = homeId != null,
                    },
                    AwayTeam    = new WcTeamRef
                    {
                        Id        = awayId,
                        FdName    = awayfdName ?? "TBD",
                        Name      = awayTbd ? "TBD" : (awayTeam?.Name ?? awayfdName ?? "?"),
                        FlagEmoji = awayTbd ? "🏳️" : (awayTeam?.FlagEmoji ?? "🏳️"),
                        Known     = awayId != null,
                    },
                    ScoreHome   = scoreHome,
                    ScoreAway   = scoreAway,
                    Venue       = m["venue"]?.ToString(),
                });
            }

            // Sortuj: faza grupowa wg matchday, potem data
            result = result
                .OrderBy(m => StageOrder(m.FdStage))
                .ThenBy(m => m.Matchday)
                .ThenBy(m => m.UtcDate)
                .ToList();

            _logger.LogInformation("Terminarz WC 2026: {Count} meczów – zapisuję do SQLite cache", result.Count);

            // Zapisz do SQLite cache
            var json = JsonSerializer.Serialize(result, _camel);
            var entry = await _db.ApiCache.FindAsync(SCHEDULE_CACHE_KEY);
            if (entry == null)
                _db.ApiCache.Add(new ApiCacheEntity { CacheKey = SCHEDULE_CACHE_KEY, ResponseJson = json, CachedAt = DateTime.UtcNow, ExpiresAt = DateTime.UtcNow.Add(SCHEDULE_TTL) });
            else
                (entry.ResponseJson, entry.CachedAt, entry.ExpiresAt) = (json, DateTime.UtcNow, DateTime.UtcNow.Add(SCHEDULE_TTL));
            await _db.SaveChangesAsync();

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Błąd pobierania terminarza WC 2026");
            return StatusCode(500, new { error = "Błąd wewnętrzny" });
        }
    }

    private static int StageOrder(string fdStage) => fdStage switch
    {
        "GROUP_STAGE"    => 0,
        "ROUND_OF_32"    => 1,
        "ROUND_OF_16"    => 2,
        "QUARTER_FINALS" => 3,
        "SEMI_FINALS"    => 4,
        "THIRD_PLACE"    => 5,
        "FINAL"          => 6,
        _                => 99,
    };
}

public class WcMatchDto
{
    public int Matchday     { get; set; }
    public string Stage     { get; set; } = "";
    public string FdStage   { get; set; } = "";
    public string? Group    { get; set; }
    public string? UtcDate  { get; set; }
    public string Status    { get; set; } = "TIMED";
    public WcTeamRef HomeTeam { get; set; } = new();
    public WcTeamRef AwayTeam { get; set; } = new();
    public int? ScoreHome   { get; set; }
    public int? ScoreAway   { get; set; }
    public string? Venue    { get; set; }
}

public class WcTeamRef
{
    public string? Id       { get; set; }
    public string FdName    { get; set; } = "";
    public string Name      { get; set; } = "";
    public string FlagEmoji { get; set; } = "🏳️";
    public bool Known       { get; set; }
}
