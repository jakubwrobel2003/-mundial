namespace MundialPrediction.Core.Models;

public class PlayerPrediction
{
    public string PlayerId { get; set; } = string.Empty;
    public string PlayerName { get; set; } = string.Empty;
    public string Club { get; set; } = string.Empty;
    public Position Position { get; set; }
    public bool IsStar { get; set; }

    public double ExpectedGoals { get; set; }
    public double ExpectedAssists { get; set; }
    public double ExpectedShots { get; set; }
    public double ExpectedShotsOnTarget { get; set; }
    public double ExpectedFoulsCommitted { get; set; }
    public double ExpectedFoulsDrawn { get; set; }
    public double YellowCardProbability { get; set; }
    public double RedCardProbability { get; set; }
    public double ExpectedKeyPasses { get; set; }
    public double ExpectedDribbles { get; set; }

    // Betting lines
    public double ShotsOverUnderLine { get; set; }
    public double ShotsOverProbability { get; set; }
    public double FoulsOverUnderLine { get; set; }
    public double FoulsOverProbability { get; set; }

    public string KeyInsight { get; set; } = string.Empty;
    public string ThreatLevel { get; set; } = string.Empty;
}
