using Microsoft.EntityFrameworkCore;
using MundialPrediction.API.Models;
using MundialPrediction.Core.Interfaces;
using MundialPrediction.Infrastructure.Database;
using MundialPrediction.Infrastructure.Database.Entities;
using MundialPrediction.Infrastructure.ExternalApis;
using System.Text.Json;

namespace MundialPrediction.API.Services;

public class ScheduleService
{
    public const string CACHE_KEY = "wc2026:schedule:v2";
    public static readonly TimeSpan CACHE_TTL = TimeSpan.FromHours(6);

    private static readonly JsonSerializerOptions _camel =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    // fd.org używa innych nazw niż nasza baza – mapujemy przed dopasowaniem
    private static readonly Dictionary<string, string> FdNameAliases = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Türkiye"]            = "Turkey",
        ["IR Iran"]            = "Iran",
        ["Côte d'Ivoire"]      = "Ivory Coast",
        ["United States"]      = "USA",
        ["Korea Republic"]     = "South Korea",
        ["Korea DPR"]          = "North Korea",
        ["China PR"]           = "China",
        ["Republic of Ireland"] = "Ireland",
        ["Bosnia and Herzegovina"] = "Bosnia",
        ["North Macedonia"]    = "North Macedonia",
    };

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

    private readonly FootballDataClient _fd;
    private readonly ITeamRepository _teams;
    private readonly MundialDbContext _db;
    private readonly ILogger<ScheduleService> _logger;

    public ScheduleService(
        FootballDataClient fd, ITeamRepository teams,
        MundialDbContext db, ILogger<ScheduleService> logger)
    {
        _fd = fd; _teams = teams; _db = db; _logger = logger;
    }

    /// <summary>
    /// Zwraca terminarz WC 2026. null = fd.org niedostępne.
    /// </summary>
    public async Task<List<WcMatchDto>?> GetScheduleAsync()
    {
        var sqlCached = await _db.ApiCache
            .Where(c => c.CacheKey == CACHE_KEY && c.ExpiresAt > DateTime.UtcNow)
            .FirstOrDefaultAsync();

        if (sqlCached != null)
        {
            _logger.LogInformation("SQLite cache hit: terminarz WC 2026");
            return JsonSerializer.Deserialize<List<WcMatchDto>>(sqlCached.ResponseJson, _camel);
        }

        var raw = await _fd.GetMatchesAsync();
        if (raw == null) return null;

        var allTeams = (await _teams.GetAllTeamsAsync()).ToList();

        var fdNameToTeam = new Dictionary<string, Core.Models.Team>(StringComparer.OrdinalIgnoreCase);
        foreach (var t in allTeams) fdNameToTeam[t.Name] = t;

        string? ResolveId(string? fdName)
        {
            if (string.IsNullOrEmpty(fdName)) return null;
            // Normalizuj nazwę (fd.org alias → nasza nazwa)
            if (FdNameAliases.TryGetValue(fdName, out var alias)) fdName = alias;
            if (fdNameToTeam.TryGetValue(fdName, out var exact)) return exact.Id;
            var partial = allTeams.FirstOrDefault(t =>
                t.Name.Contains(fdName, StringComparison.OrdinalIgnoreCase) ||
                fdName.Contains(t.Name, StringComparison.OrdinalIgnoreCase) ||
                t.ShortName.Equals(fdName, StringComparison.OrdinalIgnoreCase));
            return partial?.Id;
        }

        var matchesArr = raw["matches"]?.AsArray();
        if (matchesArr == null) return [];

        _logger.LogInformation("fd.org zwrócił {Total} meczów WC 2026", matchesArr.Count);

        var result = new List<WcMatchDto>();
        foreach (var m in matchesArr)
        {
            if (m == null) continue;

            var fdStage   = m["stage"]?.ToString() ?? "";
            var fdGroup   = m["group"]?.ToString();
            var utcDate   = m["utcDate"]?.ToString();
            var status    = m["status"]?.ToString() ?? "TIMED";
            var matchday  = m["matchday"]?.GetValue<int?>() ?? 0;

            var homefdName = m["homeTeam"]?["name"]?.ToString();
            var awayfdName = m["awayTeam"]?["name"]?.ToString();
            var homeTbd = string.IsNullOrEmpty(homefdName) || homefdName == "TBD";
            var awayTbd = string.IsNullOrEmpty(awayfdName) || awayfdName == "TBD";

            var homeId   = homeTbd ? null : ResolveId(homefdName);
            var awayId   = awayTbd ? null : ResolveId(awayfdName);
            var homeTeam = homeId != null ? allTeams.FirstOrDefault(t => t.Id == homeId) : null;
            var awayTeam = awayId != null ? allTeams.FirstOrDefault(t => t.Id == awayId) : null;

            result.Add(new WcMatchDto
            {
                Matchday  = matchday,
                Stage     = StageMap.TryGetValue(fdStage, out var s) ? s : fdStage,
                FdStage   = fdStage,
                Group     = fdGroup?.Replace("GROUP_", "Grupa "),
                UtcDate   = utcDate,
                Status    = status,
                HomeTeam  = new WcTeamRef
                {
                    Id        = homeId,
                    FdName    = homefdName ?? "TBD",
                    Name      = homeTbd ? "TBD" : (homeTeam?.Name ?? homefdName ?? "?"),
                    FlagEmoji = homeTbd ? "🏳️" : (homeTeam?.FlagEmoji ?? "🏳️"),
                    Known     = homeId != null,
                },
                AwayTeam  = new WcTeamRef
                {
                    Id        = awayId,
                    FdName    = awayfdName ?? "TBD",
                    Name      = awayTbd ? "TBD" : (awayTeam?.Name ?? awayfdName ?? "?"),
                    FlagEmoji = awayTbd ? "🏳️" : (awayTeam?.FlagEmoji ?? "🏳️"),
                    Known     = awayId != null,
                },
                ScoreHome = m["score"]?["fullTime"]?["home"]?.GetValue<int?>(),
                ScoreAway = m["score"]?["fullTime"]?["away"]?.GetValue<int?>(),
                Venue     = m["venue"]?.ToString(),
            });
        }

        result = result
            .OrderBy(m => StageOrder(m.FdStage))
            .ThenBy(m => m.Matchday)
            .ThenBy(m => m.UtcDate)
            .ToList();

        _logger.LogInformation("Terminarz WC 2026: {Count} meczów – zapisuję do SQLite", result.Count);

        var json = JsonSerializer.Serialize(result, _camel);
        var entry = await _db.ApiCache.FindAsync(CACHE_KEY);
        if (entry == null)
            _db.ApiCache.Add(new ApiCacheEntity { CacheKey = CACHE_KEY, ResponseJson = json, CachedAt = DateTime.UtcNow, ExpiresAt = DateTime.UtcNow.Add(CACHE_TTL) });
        else
            (entry.ResponseJson, entry.CachedAt, entry.ExpiresAt) = (json, DateTime.UtcNow, DateTime.UtcNow.Add(CACHE_TTL));
        await _db.SaveChangesAsync();

        return result;
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
