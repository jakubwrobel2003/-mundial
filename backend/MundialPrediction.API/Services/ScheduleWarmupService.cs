using Microsoft.EntityFrameworkCore;
using MundialPrediction.Infrastructure.Database;

namespace MundialPrediction.API.Services;

/// <summary>
/// Podgrzewa cache terminarza WC 2026 przy starcie serwera, żeby pierwsze żądanie użytkownika
/// było szybkie nawet po restarcie (np. Railway cold start).
/// </summary>
public class ScheduleWarmupService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ScheduleWarmupService> _logger;

    public ScheduleWarmupService(IServiceScopeFactory scopeFactory, ILogger<ScheduleWarmupService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Poczekaj aż app jest w pełni uruchomiona i baza zainicjalizowana
        await Task.Delay(TimeSpan.FromSeconds(6), stoppingToken);
        if (stoppingToken.IsCancellationRequested) return;

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MundialDbContext>();

            // Sprawdź czy SQLite cache już jest ważny – jeśli tak, nie rób nic
            var cached = await db.ApiCache
                .Where(c => c.CacheKey == ScheduleService.CACHE_KEY && c.ExpiresAt > DateTime.UtcNow)
                .FirstOrDefaultAsync(stoppingToken);

            if (cached != null)
            {
                _logger.LogInformation("Warmup: terminarz już w cache (wygasa {Exp:HH:mm}), pomijam", cached.ExpiresAt);
                return;
            }

            _logger.LogInformation("Warmup: podgrzewam cache terminarza WC 2026...");
            var svc = scope.ServiceProvider.GetRequiredService<ScheduleService>();
            var result = await svc.GetScheduleAsync();
            _logger.LogInformation("Warmup: terminarz załadowany ({Count} meczów)", result?.Count ?? 0);
        }
        catch (OperationCanceledException) { /* normalny shutdown */ }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Warmup terminarza nie powiódł się (niekrytyczne)");
        }
    }
}
