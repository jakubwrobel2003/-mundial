using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using MundialPrediction.Core.Interfaces;
using MundialPrediction.Infrastructure.Database;
using MundialPrediction.Infrastructure.ExternalApis;
using MundialPrediction.Infrastructure.RAG;
using MundialPrediction.Infrastructure.Services;

// Szukaj .env traversując w górę od katalogu projektu aż do roota
static string? FindEnvFile()
{
    var dir = new DirectoryInfo(AppContext.BaseDirectory);
    while (dir != null)
    {
        var candidate = Path.Combine(dir.FullName, ".env");
        if (File.Exists(candidate)) return candidate;
        dir = dir.Parent;
    }
    return null;
}

var envPath = FindEnvFile();
if (envPath != null)
{
    Env.Load(envPath);
    Console.WriteLine($"[ENV] Załadowano: {envPath}");
}
else
{
    Console.WriteLine("[ENV] UWAGA: Nie znaleziono pliku .env");
}

var builder = WebApplication.CreateBuilder(args);

// ── Kontrolery + Swagger ───────────────────────────────────────────────────
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Mundial 2026 Prediction API",
        Version = "v1",
        Description = "Predykcje meczów MŚ 2026 z Poisson xG, Claude AI, Tavily i football-data.org"
    });
});

// ── CORS ──────────────────────────────────────────────────────────────────
// Na Railway frontend wywołuje /api/ przez nginx proxy (same-origin), więc CORS
// jest potrzebne tylko lokalnie. Dodajemy też obsługę Railway public domain.
var allowedOrigins = new List<string> { "http://localhost:4200" };
var railwayFrontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL");
if (!string.IsNullOrWhiteSpace(railwayFrontendUrl))
    allowedOrigins.Add(railwayFrontendUrl.TrimEnd('/'));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
        policy.WithOrigins(allowedOrigins.ToArray())
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// ── Memory Cache ─────────────────────────────────────────────────────────
builder.Services.AddMemoryCache();

// ── SQLite + EF Core ───────────────────────────────────────────────────────
var dbPath = Environment.GetEnvironmentVariable("DATABASE_PATH")
             ?? builder.Configuration["Database:Path"]
             ?? "./mundial2026.db";

// Upewnij się że katalog bazy danych istnieje (ważne gdy Volume nie jest zamontowane)
var dbDir = Path.GetDirectoryName(dbPath);
if (!string.IsNullOrEmpty(dbDir) && !Directory.Exists(dbDir))
    Directory.CreateDirectory(dbDir);

builder.Services.AddDbContext<MundialDbContext>(opt =>
    opt.UseSqlite($"Data Source={dbPath}"));

// ── Konfiguracja z .env / appsettings ────────────────────────────────────
var fdApiKey = Environment.GetEnvironmentVariable("FOOTBALL_DATA_API_KEY") ?? "";
var fdBaseUrl = (Environment.GetEnvironmentVariable("FOOTBALL_DATA_BASE_URL")
                ?? "https://api.football-data.org/v4").TrimEnd('/') + "/";
var fdRateLimit = int.TryParse(Environment.GetEnvironmentVariable("FOOTBALL_DATA_RATE_LIMIT_PER_MINUTE"), out var r) ? r : 10;
var fdCacheSec = int.TryParse(Environment.GetEnvironmentVariable("FOOTBALL_DATA_CACHE_SECONDS"), out var c) ? c : 120;

var tavilyKey = Environment.GetEnvironmentVariable("TAVILY_API_KEY") ?? "";
var tavilyBase = (Environment.GetEnvironmentVariable("TAVILY_BASE_URL")
                 ?? "https://api.tavily.com").TrimEnd('/') + "/";
var tavilyMax = int.TryParse(Environment.GetEnvironmentVariable("TAVILY_MAX_RESULTS"), out var tm) ? tm : 5;
var tavilyDepth = Environment.GetEnvironmentVariable("TAVILY_SEARCH_DEPTH") ?? "basic";

var anthropicKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY") ?? "";
var anthropicBase = Environment.GetEnvironmentVariable("ANTHROPIC_BASE_URL") ?? "https://api.anthropic.com/";
var claudeModel = Environment.GetEnvironmentVariable("CLAUDE_MODEL") ?? "claude-sonnet-4-6";
var claudeMaxTokens = int.TryParse(Environment.GetEnvironmentVariable("CLAUDE_MAX_TOKENS"), out var mt) ? mt : 2000;
var claudeTemp = double.TryParse(Environment.GetEnvironmentVariable("CLAUDE_TEMPERATURE"),
    System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var t) ? t : 0.3;

// ── Named HttpClients ─────────────────────────────────────────────────────
builder.Services.AddHttpClient("footballdata", http =>
{
    http.BaseAddress = new Uri(fdBaseUrl);
    if (!string.IsNullOrEmpty(fdApiKey))
        http.DefaultRequestHeaders.Add("X-Auth-Token", fdApiKey);
    http.DefaultRequestHeaders.Add("Accept", "application/json");
    http.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpClient("tavily", http =>
{
    http.BaseAddress = new Uri(tavilyBase);
    http.DefaultRequestHeaders.Add("Accept", "application/json");
    http.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpClient("anthropic", http =>
{
    http.BaseAddress = new Uri(anthropicBase);
    if (!string.IsNullOrEmpty(anthropicKey))
        http.DefaultRequestHeaders.Add("x-api-key", anthropicKey);
    http.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
    http.DefaultRequestHeaders.Add("Accept", "application/json");
    http.Timeout = TimeSpan.FromSeconds(90);
});

// ── Zewnętrzne klienty jako Singleton ────────────────────────────────────
builder.Services.AddSingleton<FootballDataClient>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var cache = sp.GetRequiredService<Microsoft.Extensions.Caching.Memory.IMemoryCache>();
    var logger = sp.GetRequiredService<ILogger<FootballDataClient>>();
    return new FootballDataClient(factory.CreateClient("footballdata"), cache, logger, fdCacheSec, fdRateLimit);
});

builder.Services.AddSingleton<TavilySearchClient>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var logger = sp.GetRequiredService<ILogger<TavilySearchClient>>();
    return new TavilySearchClient(factory.CreateClient("tavily"), logger, tavilyKey, tavilyMax, tavilyDepth);
});

builder.Services.AddSingleton<ClaudeAnalysisClient>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var logger = sp.GetRequiredService<ILogger<ClaudeAnalysisClient>>();
    return new ClaudeAnalysisClient(factory.CreateClient("anthropic"), logger, claudeModel, claudeMaxTokens, claudeTemp);
});

// ── Core usługi predykcji ─────────────────────────────────────────────────
builder.Services.AddSingleton<ITeamRepository, TeamRepository>();
builder.Services.AddSingleton<PredictionEngine>();
builder.Services.AddScoped<IPredictionService, PredictionService>();

// ── RAG + Enriched prediction ─────────────────────────────────────────────
builder.Services.AddScoped<KnowledgeBase>();
builder.Services.AddScoped<TeamHistoryService>();
builder.Services.AddScoped<PlayerClubStatsService>();
builder.Services.AddScoped<EnrichedPredictionService>();

// ── Terminarz + warmup ────────────────────────────────────────────────────
builder.Services.AddScoped<MundialPrediction.API.Services.ScheduleService>();
builder.Services.AddHostedService<MundialPrediction.API.Services.ScheduleWarmupService>();

// ── Port (Railway wstrzykuje PORT, lokalnie domyślnie 5000) ──────────────
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// ─────────────────────────────────────────────────────────────────────────
var app = builder.Build();

// ── Inicjalizacja bazy danych ─────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MundialDbContext>();
    await db.Database.EnsureCreatedAsync();
    await db.EnsureFts5TableAsync();
}

// ── Middleware ─────────────────────────────────────────────────────────────
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mundial 2026 API v1"));

app.UseCors("Angular");

// SPA static files
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

// Angular router fallback – /api/* obsłużone przez kontrolery powyżej
if (File.Exists(Path.Combine(app.Environment.WebRootPath ?? "", "index.html")))
    app.MapFallbackToFile("index.html");

app.Run();
