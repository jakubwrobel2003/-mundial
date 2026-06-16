using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace MundialPrediction.Infrastructure.ExternalApis;

// Klient Tavily – szukanie aktualnych informacji w internecie:
// kontuzje, forma, konferencje prasowe, head-to-head historia itp.
public class TavilySearchClient
{
    private readonly HttpClient _http;
    private readonly ILogger<TavilySearchClient> _logger;
    private readonly string _apiKey;
    private readonly int _maxResults;
    private readonly string _searchDepth;

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public TavilySearchClient(
        HttpClient http,
        ILogger<TavilySearchClient> logger,
        string apiKey,
        int maxResults = 5,
        string searchDepth = "basic")
    {
        _http = http;
        _logger = logger;
        _apiKey = apiKey;
        _maxResults = maxResults;
        _searchDepth = searchDepth;
    }

    // Szuka aktualnych wiadomości o drużynie (kontuzje, forma, skład)
    public async Task<TavilyResult?> SearchTeamNewsAsync(string teamName)
    {
        var query = $"{teamName} FIFA World Cup 2026 squad injuries form news";
        return await SearchAsync(query, includeDomains: new[] { "bbc.com", "goal.com", "espn.com", "skysports.com", "transfermarkt.com" });
    }

    // Szuka historii spotkań dwóch drużyn
    public async Task<TavilyResult?> SearchHeadToHeadAsync(string team1, string team2)
    {
        var query = $"{team1} vs {team2} head to head history World Cup statistics";
        return await SearchAsync(query);
    }

    // Szuka previews i analiz meczu
    public async Task<TavilyResult?> SearchMatchPreviewAsync(string team1, string team2)
    {
        var query = $"{team1} vs {team2} World Cup 2026 prediction preview analysis";
        return await SearchAsync(query);
    }

    // Szuka raportów o kontuzjach
    public async Task<TavilyResult?> SearchInjuriesAsync(string teamName)
    {
        var query = $"{teamName} injury report World Cup 2026 squad availability";
        return await SearchAsync(query, includeDomains: new[] { "transfermarkt.com", "goal.com", "bbc.com" });
    }

    // Szuka wyników z eliminacji i meczów towarzyskich (eliminacje są poza free tierem API)
    public async Task<TavilyResult?> SearchQualifiersAndFriendliesAsync(string teamName)
    {
        var query = $"{teamName} national team 2024 2025 qualifiers friendly matches results form";
        return await SearchAsync(query, includeDomains: new[] { "transfermarkt.com", "soccerway.com", "footballdatabase.eu", "bbc.com", "goal.com" });
    }

    // Ogólne szukanie
    public async Task<TavilyResult?> SearchAsync(string query, string[]? includeDomains = null)
    {
        _logger.LogInformation("Tavily search: {Query}", query);

        var request = new TavilyRequest
        {
            ApiKey = _apiKey,
            Query = query,
            SearchDepth = _searchDepth,
            MaxResults = _maxResults,
            IncludeDomains = includeDomains,
            IncludeAnswer = true,
            IncludeRawContent = false
        };

        try
        {
            var resp = await _http.PostAsJsonAsync("search", request, JsonOpts);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<TavilyResult>(JsonOpts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Tavily error dla: {Query}", query);
            return null;
        }
    }
}

public class TavilyRequest
{
    public string ApiKey { get; set; } = string.Empty;
    public string Query { get; set; } = string.Empty;
    public string SearchDepth { get; set; } = "basic";
    public int MaxResults { get; set; } = 5;
    public string[]? IncludeDomains { get; set; }
    public bool IncludeAnswer { get; set; } = true;
    public bool IncludeRawContent { get; set; } = false;
}

public class TavilyResult
{
    public string? Answer { get; set; }          // Syntetyczna odpowiedź Tavily AI
    public string? Query { get; set; }
    public List<TavilyResultItem> Results { get; set; } = new();
}

public class TavilyResultItem
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public double Score { get; set; }
    public string? PublishedDate { get; set; }
}
