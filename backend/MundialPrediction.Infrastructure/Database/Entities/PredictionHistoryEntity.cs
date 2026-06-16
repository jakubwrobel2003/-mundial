namespace MundialPrediction.Infrastructure.Database.Entities;

public class PredictionHistoryEntity
{
    public int Id { get; set; }
    public string HomeTeamId { get; set; } = string.Empty;
    public string AwayTeamId { get; set; } = string.Empty;
    public string Stage { get; set; } = string.Empty;

    // Wyniki predykcji
    public double HomeWinProbability { get; set; }
    public double DrawProbability { get; set; }
    public double AwayWinProbability { get; set; }
    public double HomeExpectedGoals { get; set; }
    public double AwayExpectedGoals { get; set; }
    public int PredictedHomeGoals { get; set; }
    public int PredictedAwayGoals { get; set; }

    // Pełna odpowiedź JSON (do podglądu)
    public string PredictionJson { get; set; } = string.Empty;

    // Analiza Claude (tekst)
    public string? ClaudeAnalysis { get; set; }

    // Czy użyto zewnętrznych danych
    public bool UsedFootballData { get; set; }
    public bool UsedTavily { get; set; }
    public bool UsedRag { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
