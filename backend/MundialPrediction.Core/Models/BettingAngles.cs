namespace MundialPrediction.Core.Models;

public class BettingAngles
{
    public double Over15GoalsProbability { get; set; }
    public double Over25GoalsProbability { get; set; }
    public double Over35GoalsProbability { get; set; }
    public double BothTeamsToScoreProbability { get; set; }
    public double HomeCleanSheetProbability { get; set; }
    public double AwayCleanSheetProbability { get; set; }

    public double Over35CardsProbability { get; set; }
    public double Over45CardsProbability { get; set; }
    public double Over55CardsProbability { get; set; }

    public double Over85FoulsProbability { get; set; }
    public double Over105FoulsProbability { get; set; }

    // Which team fouls more
    public string MoreFoulsTeam { get; set; } = string.Empty;
    public double MoreFoulsProbability { get; set; }

    // Which team gets more cards
    public string MoreCardsTeam { get; set; } = string.Empty;
    public double MoreCardsProbability { get; set; }

    // First goal
    public string FirstGoalTeam { get; set; } = string.Empty;
    public double FirstGoalProbability { get; set; }

    // Corners
    public double ExpectedCorners { get; set; }
    public double Over9CornersProbability { get; set; }

    // Half time
    public string HalfTimeResult { get; set; } = string.Empty;
    public double HalfTimeResultProbability { get; set; }

    public List<string> ValueBets { get; set; } = new();
    public List<string> AvoidBets { get; set; } = new();
}
