namespace MundialPrediction.Core.Models;

public class Team
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string FlagEmoji { get; set; } = string.Empty;
    public string Confederation { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public int FifaRanking { get; set; }
    public PlayingStyle PrimaryStyle { get; set; }
    public PlayingStyle SecondaryStyle { get; set; }

    // Team stats (season averages)
    public double GoalsPerGame { get; set; }
    public double GoalsConcededPerGame { get; set; }
    public double ShotsPerGame { get; set; }
    public double ShotsOnTargetPerGame { get; set; }
    public double FoulsPerGame { get; set; }
    public double YellowCardsPerGame { get; set; }
    public double RedCardsPerGame { get; set; }
    public double Possession { get; set; }
    public double PressureIntensity { get; set; }
    public double DefensiveLineHeight { get; set; }

    // Form
    public double CurrentForm { get; set; }
    public string RecentResults { get; set; } = string.Empty;

    public int SquadStrength { get; set; }
    public string ManagerName { get; set; } = string.Empty;
    public string TacticalSetup { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<Player> Players { get; set; } = new();
}
