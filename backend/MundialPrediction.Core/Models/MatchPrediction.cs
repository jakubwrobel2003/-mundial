namespace MundialPrediction.Core.Models;

public class MatchPrediction
{
    public string MatchId { get; set; } = string.Empty;
    public Team HomeTeam { get; set; } = null!;
    public Team AwayTeam { get; set; } = null!;

    // Win probabilities
    public double HomeWinProbability { get; set; }
    public double DrawProbability { get; set; }
    public double AwayWinProbability { get; set; }

    // Expected goals
    public double HomeExpectedGoals { get; set; }
    public double AwayExpectedGoals { get; set; }

    // Most likely score
    public int PredictedHomeGoals { get; set; }
    public int PredictedAwayGoals { get; set; }
    public double PredictedScoreProbability { get; set; }

    // Top 5 most probable scores
    public List<ScorePrediction> TopScores { get; set; } = new();

    // Expected match stats
    public double ExpectedTotalCards { get; set; }
    public double ExpectedTotalFouls { get; set; }
    public double ExpectedTotalShots { get; set; }
    public double ExpectedCorners { get; set; }

    // Analysis
    public string MatchSummary { get; set; } = string.Empty;
    public List<string> KeyFactors { get; set; } = new();
    public List<string> TacticalInsights { get; set; } = new();
    public List<string> DangerZones { get; set; } = new();

    // Confidence
    public int AnalysisConfidence { get; set; }
    public string Verdict { get; set; } = string.Empty;

    // Player predictions
    public List<PlayerPrediction> HomePlayerPredictions { get; set; } = new();
    public List<PlayerPrediction> AwayPlayerPredictions { get; set; } = new();

    // Betting
    public BettingAngles BettingAngles { get; set; } = new();

    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}

public class ScorePrediction
{
    public int HomeGoals { get; set; }
    public int AwayGoals { get; set; }
    public double Probability { get; set; }
    public string Label { get; set; } = string.Empty;
}
