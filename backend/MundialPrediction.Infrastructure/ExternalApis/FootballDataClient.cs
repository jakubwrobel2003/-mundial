using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MundialPrediction.Infrastructure.ExternalApis;

// Klient football-data.org z wbudowanym rate limiterem (10 req/min)
// i dwuwarstwowym cache (memory + SQLite przez wyższy poziom).
public class FootballDataClient
{
    private const string WC2026 = "WC";
    private const int Season = 2026;

    private readonly HttpClient _http;
    private readonly IMemoryCache _cache;
    private readonly ILogger<FootballDataClient> _logger;
    private readonly TimeSpan _cacheTtl;

    // FixedWindowRateLimiter: 10 tokenów na 60 sekund.
    // QueueLimit=50 oznacza że AcquireAsync czeka w kolejce zamiast rzucać wyjątkiem –
    // program sam czeka na reset okna bez dodatkowego kodu.
    private readonly RateLimiter _rateLimiter;

    // Mapa: nazwa drużyny → football-data.org team ID; budowana leniwie z danych WC 2026
    private Dictionary<string, int>? _teamNameToFdId;
    private readonly SemaphoreSlim _mappingLock = new(1, 1);

    private static readonly JsonSerializerOptions JsonOpts =
        new() { PropertyNameCaseInsensitive = true };

    public FootballDataClient(
        HttpClient http,
        IMemoryCache cache,
        ILogger<FootballDataClient> logger,
        int cacheSec = 3600,
        int ratePerMin = 10)
    {
        _http = http;
        _cache = cache;
        _logger = logger;
        _cacheTtl = TimeSpan.FromSeconds(cacheSec);

        _rateLimiter = new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
        {
            PermitLimit = ratePerMin,
            Window = TimeSpan.FromMinutes(1),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 50  // max zapytań w kolejce
        });
    }

    // Zwraca mecze MŚ 2026 (faza grupowa + pucharowa).
    // Jawny zakres dat bo fd.org bez nich zwraca tylko ~10 najbliższych dni.
    public async Task<JsonNode?> GetMatchesAsync(string? status = null)
    {
        var key = $"fd:matches:{status ?? "all"}";
        return await GetCachedAsync(key, async () =>
        {
            var url = $"competitions/{WC2026}/matches?season={Season}&dateFrom=2026-06-01&dateTo=2026-08-01";
            if (status != null) url += $"&status={status}";
            return await FetchAsync(url);
        });
    }

    // Tabele grup MŚ 2026
    public async Task<JsonNode?> GetStandingsAsync()
    {
        return await GetCachedAsync("fd:standings", async () =>
            await FetchAsync($"competitions/{WC2026}/standings?season={Season}"));
    }

    // Skład drużyny (po FIFA team ID)
    public async Task<JsonNode?> GetTeamAsync(int teamId)
    {
        return await GetCachedAsync($"fd:team:{teamId}", async () =>
            await FetchAsync($"teams/{teamId}"));
    }

    // Strzelcy MŚ 2026
    public async Task<JsonNode?> GetTopScorersAsync()
    {
        return await GetCachedAsync("fd:scorers", async () =>
            await FetchAsync($"competitions/{WC2026}/scorers?season={Season}&limit=20"));
    }

    // Jeden konkretny mecz po ID
    public async Task<JsonNode?> GetMatchAsync(int matchId)
    {
        return await GetCachedAsync($"fd:match:{matchId}", async () =>
            await FetchAsync($"matches/{matchId}"));
    }

    // Mecze Euro 2024 (kompletne, cachujemy długo bo dane historyczne nie zmienią się)
    public async Task<JsonNode?> GetEuro2024MatchesAsync()
    {
        return await GetCachedAsync("fd:ec:2024", async () =>
            await FetchAsync("competitions/EC/matches?season=2024"));
    }

    // Mecze WC 2026 (alias dla czytelności w serwisach wyżej)
    public Task<JsonNode?> GetWC2026MatchesAsync() => GetMatchesAsync();

    // Top scorerzy z wybranej ligi – 1 call na ligę, cache w SQLite przez wywołującego
    public async Task<JsonNode?> GetLeagueScorersAsync(string competitionCode, int season = 2024)
    {
        // Używamy krótkiego memory cache (2min) – SQLite cache jest zarządzany przez PlayerClubStatsService
        return await GetCachedAsync($"fd:scorers:{competitionCode}:{season}", async () =>
            await FetchAsync($"competitions/{competitionCode}/scorers?season={season}&limit=50"));
    }

    // Zwraca football-data.org team ID dla podanej nazwy drużyny.
    // Buduje mapę leniwie z danych meczów WC 2026 (żeby nie hard-kodować IDs).
    public async Task<int?> GetFdTeamIdAsync(string teamName)
    {
        if (_teamNameToFdId == null)
        {
            await _mappingLock.WaitAsync();
            try { if (_teamNameToFdId == null) await BuildTeamMappingAsync(); }
            finally { _mappingLock.Release(); }
        }

        if (_teamNameToFdId!.TryGetValue(teamName, out var exact)) return exact;

        var partial = _teamNameToFdId.FirstOrDefault(kv =>
            kv.Key.Contains(teamName, StringComparison.OrdinalIgnoreCase) ||
            teamName.Contains(kv.Key, StringComparison.OrdinalIgnoreCase));
        return partial.Key != null ? partial.Value : (int?)null;
    }

    private async Task BuildTeamMappingAsync()
    {
        var matches = await GetMatchesAsync();
        var map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        var arr = matches?["matches"]?.AsArray();
        if (arr == null) return;

        foreach (var m in arr)
        {
            foreach (var side in new[] { "homeTeam", "awayTeam" })
            {
                var name = m?[side]?["name"]?.ToString();
                var shortName = m?[side]?["shortName"]?.ToString();
                var id = m?[side]?["id"]?.GetValue<int?>();
                if (name != null && id.HasValue)
                {
                    map[name] = id.Value;
                    if (shortName != null) map[shortName] = id.Value;
                }
            }
        }
        _teamNameToFdId = map;
        _logger.LogInformation("Mapa drużyn football-data.org: {Count} wpisów", map.Count);
    }

    // --- prywatne ---

    private async Task<JsonNode?> GetCachedAsync(string key, Func<Task<JsonNode?>> fetch)
    {
        if (_cache.TryGetValue(key, out JsonNode? cached))
        {
            _logger.LogDebug("Cache hit: {Key}", key);
            return cached;
        }

        var result = await fetch();
        if (result != null)
            _cache.Set(key, result, _cacheTtl);

        return result;
    }

    private async Task<JsonNode?> FetchAsync(string path)
    {
        using var lease = await _rateLimiter.AcquireAsync(permitCount: 1);
        if (!lease.IsAcquired)
        {
            _logger.LogWarning("FootballData rate limit – zapytanie odrzucone: {Path}", path);
            return null;
        }

        try
        {
            _logger.LogInformation("FootballData GET {Path}", path);
            var resp = await _http.GetAsync(path);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync();
                _logger.LogError("FootballData HTTP {Status} dla {Path}: {Body}", (int)resp.StatusCode, path, body[..Math.Min(300, body.Length)]);
                return null;
            }
            var json = await resp.Content.ReadAsStringAsync();
            return JsonNode.Parse(json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "FootballData error dla {Path}", path);
            return null;
        }
    }
}
