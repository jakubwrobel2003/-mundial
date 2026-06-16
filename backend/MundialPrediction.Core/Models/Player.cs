namespace MundialPrediction.Core.Models;

public class Player
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Club { get; set; } = string.Empty;
    public Position Position { get; set; }
    public int ShirtNumber { get; set; }
    public bool IsCaptain { get; set; }
    public bool IsKeyStar { get; set; }
    public int OverallRating { get; set; }
    public double CurrentForm { get; set; }

    // Per-90-minute stats
    public double GoalsPer90 { get; set; }
    public double AssistsPer90 { get; set; }
    public double ShotsPer90 { get; set; }
    public double ShotsOnTargetPer90 { get; set; }
    public double KeyPassesPer90 { get; set; }
    public double DribblesPer90 { get; set; }
    public double FoulsCommittedPer90 { get; set; }
    public double FoulsDrawnPer90 { get; set; }
    public double YellowCardsPer90 { get; set; }
    public double RedCardsPer90 { get; set; }
    public double TacklesPer90 { get; set; }
    public double InterceptionsPer90 { get; set; }
    public double AerialDuelsPer90 { get; set; }

    public string Description { get; set; } = string.Empty;
    public string PlayingCharacteristics { get; set; } = string.Empty;
}
