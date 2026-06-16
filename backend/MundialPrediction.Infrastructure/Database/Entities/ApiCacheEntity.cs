namespace MundialPrediction.Infrastructure.Database.Entities;

// Cache odpowiedzi z football-data.org – żeby nie przepalać limitu 10/min
public class ApiCacheEntity
{
    public string CacheKey { get; set; } = string.Empty;   // np. "football-data:matches:WC2026"
    public string ResponseJson { get; set; } = string.Empty;
    public DateTime CachedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
}
