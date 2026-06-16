using MundialPrediction.Core.Models;

namespace MundialPrediction.Infrastructure.Services;

public class PredictionEngine
{
    private static readonly Random Rng = new(42);

    public MatchPrediction Analyze(Team home, Team away, string stage = "Group")
    {
        double homeStrength = ComputeTeamStrength(home);
        double awayStrength = ComputeTeamStrength(away);

        double homeAdvantage = stage == "Group" ? 0.08 : 0.04;
        double homeXg = ComputeExpectedGoals(home, away) * (1 + homeAdvantage);
        double awayXg = ComputeExpectedGoals(away, home);

        var topScores = ComputeTopScores(homeXg, awayXg);
        var best = topScores[0];

        (double homeWin, double draw, double awayWin) = ComputeWinProbabilities(homeXg, awayXg);

        double styleFoulMultiplier = StyleFoulMultiplier(home.PrimaryStyle, away.PrimaryStyle);
        double totalFouls = (home.FoulsPerGame + away.FoulsPerGame) * styleFoulMultiplier;
        double totalCards = (home.YellowCardsPerGame + away.YellowCardsPerGame) * styleFoulMultiplier
                            + (home.RedCardsPerGame + away.RedCardsPerGame);
        double totalShots = home.ShotsPerGame + away.ShotsPerGame;
        double totalCorners = ((home.ShotsPerGame + away.ShotsPerGame) / 3.5) * 1.2;

        var homePlayerPreds = BuildPlayerPredictions(home.Players, away, homeXg / Math.Max(home.Players.Count(p => p.Position == Position.Forward || p.Position == Position.Midfielder), 1));
        var awayPlayerPreds = BuildPlayerPredictions(away.Players, home, awayXg / Math.Max(away.Players.Count(p => p.Position == Position.Forward || p.Position == Position.Midfielder), 1));

        var betting = BuildBettingAngles(home, away, homeXg, awayXg, totalFouls, totalCards, homeWin, draw, awayWin);

        return new MatchPrediction
        {
            MatchId = $"{home.Id}-vs-{away.Id}",
            HomeTeam = home,
            AwayTeam = away,
            HomeWinProbability = homeWin,
            DrawProbability = draw,
            AwayWinProbability = awayWin,
            HomeExpectedGoals = Math.Round(homeXg, 2),
            AwayExpectedGoals = Math.Round(awayXg, 2),
            PredictedHomeGoals = best.HomeGoals,
            PredictedAwayGoals = best.AwayGoals,
            PredictedScoreProbability = best.Probability,
            TopScores = topScores,
            ExpectedTotalCards = Math.Round(totalCards, 1),
            ExpectedTotalFouls = Math.Round(totalFouls, 1),
            ExpectedTotalShots = Math.Round(totalShots, 1),
            ExpectedCorners = Math.Round(totalCorners, 1),
            MatchSummary = BuildMatchSummary(home, away, homeXg, awayXg, homeWin, draw, awayWin),
            KeyFactors = BuildKeyFactors(home, away, homeXg, awayXg),
            TacticalInsights = BuildTacticalInsights(home, away),
            DangerZones = BuildDangerZones(home, away),
            AnalysisConfidence = ComputeConfidence(home, away),
            Verdict = BuildVerdict(home, away, homeWin, draw, awayWin, homeXg, awayXg),
            HomePlayerPredictions = homePlayerPreds,
            AwayPlayerPredictions = awayPlayerPreds,
            BettingAngles = betting,
        };
    }

    private static double ComputeTeamStrength(Team t)
    {
        double rankingScore = Math.Max(0, (200 - t.FifaRanking) / 200.0) * 30;
        double squadScore = t.SquadStrength / 100.0 * 35;
        double formScore = t.CurrentForm / 10.0 * 20;
        double attackScore = Math.Min(t.GoalsPerGame / 3.5, 1.0) * 10;
        double defenseScore = Math.Max(0, (2.5 - t.GoalsConcededPerGame) / 2.5) * 5;
        return rankingScore + squadScore + formScore + attackScore + defenseScore;
    }

    private static double ComputeExpectedGoals(Team attacker, Team defender)
    {
        double baseXg = attacker.GoalsPerGame;
        double defReduction = (1 - (defender.GoalsConcededPerGame / 2.5));
        double styleBonus = attacker.PrimaryStyle switch
        {
            PlayingStyle.HighPress => 1.08,
            PlayingStyle.TikiTaka => 1.06,
            PlayingStyle.Possession => 1.04,
            PlayingStyle.CounterAttack => 1.02,
            PlayingStyle.Direct => 1.0,
            PlayingStyle.Defensive => 0.88,
            PlayingStyle.Physical => 0.95,
            _ => 1.0
        };
        double formBonus = attacker.CurrentForm / 8.0;
        return Math.Max(0.3, baseXg * defReduction * styleBonus * formBonus);
    }

    private static List<ScorePrediction> ComputeTopScores(double homeXg, double awayXg)
    {
        var scores = new List<(int h, int a, double prob)>();
        for (int h = 0; h <= 5; h++)
        {
            for (int a = 0; a <= 5; a++)
            {
                double p = Poisson(h, homeXg) * Poisson(a, awayXg);
                scores.Add((h, a, p));
            }
        }
        return scores
            .OrderByDescending(s => s.prob)
            .Take(5)
            .Select(s => new ScorePrediction
            {
                HomeGoals = s.h,
                AwayGoals = s.a,
                Probability = Math.Round(s.prob * 100, 1),
                Label = $"{s.h} - {s.a}"
            })
            .ToList();
    }

    private static (double home, double draw, double away) ComputeWinProbabilities(double homeXg, double awayXg)
    {
        double homeWin = 0, draw = 0, awayWin = 0;
        for (int h = 0; h <= 8; h++)
        {
            for (int a = 0; a <= 8; a++)
            {
                double p = Poisson(h, homeXg) * Poisson(a, awayXg);
                if (h > a) homeWin += p;
                else if (h == a) draw += p;
                else awayWin += p;
            }
        }
        double total = homeWin + draw + awayWin;
        return (Math.Round(homeWin / total * 100, 1), Math.Round(draw / total * 100, 1), Math.Round(awayWin / total * 100, 1));
    }

    private static double Poisson(int k, double lambda)
    {
        if (lambda <= 0) return k == 0 ? 1.0 : 0.0;
        double result = Math.Exp(-lambda);
        for (int i = 1; i <= k; i++) result *= lambda / i;
        return result;
    }

    private static double StyleFoulMultiplier(PlayingStyle home, PlayingStyle away)
    {
        double base_ = 1.0;
        if (home == PlayingStyle.Physical || away == PlayingStyle.Physical) base_ += 0.18;
        if (home == PlayingStyle.HighPress || away == PlayingStyle.HighPress) base_ += 0.10;
        if (home == PlayingStyle.Defensive || away == PlayingStyle.Defensive) base_ += 0.08;
        if (home == PlayingStyle.TikiTaka && away == PlayingStyle.HighPress) base_ += 0.12;
        return base_;
    }

    private static List<PlayerPrediction> BuildPlayerPredictions(List<Player> players, Team opponent, double teamXgShare)
    {
        var result = new List<PlayerPrediction>();
        foreach (var p in players)
        {
            double defensiveMultiplier = Math.Max(0.6, 1.2 - opponent.GoalsConcededPerGame / 3.0);
            double cardMultiplier = StyleFoulMultiplier(PlayingStyle.Physical, opponent.PrimaryStyle);

            double xg = p.Position == Position.Forward
                ? p.GoalsPer90 * defensiveMultiplier
                : p.Position == Position.Midfielder
                    ? p.GoalsPer90 * defensiveMultiplier * 0.7
                    : p.GoalsPer90 * 0.3;

            double xFouls = p.FoulsCommittedPer90 * cardMultiplier;
            double ycProb = Math.Min(0.95, p.YellowCardsPer90 * cardMultiplier * 1.5);

            result.Add(new PlayerPrediction
            {
                PlayerId = p.Id,
                PlayerName = p.Name,
                Club = p.Club,
                Position = p.Position,
                IsStar = p.IsKeyStar,
                ExpectedGoals = Math.Round(xg, 2),
                ExpectedAssists = Math.Round(p.AssistsPer90 * defensiveMultiplier, 2),
                ExpectedShots = Math.Round(p.ShotsPer90, 1),
                ExpectedShotsOnTarget = Math.Round(p.ShotsOnTargetPer90 * defensiveMultiplier, 1),
                ExpectedFoulsCommitted = Math.Round(xFouls, 1),
                ExpectedFoulsDrawn = Math.Round(p.FoulsDrawnPer90, 1),
                YellowCardProbability = Math.Round(ycProb * 100, 1),
                RedCardProbability = Math.Round(p.RedCardsPer90 * 100, 1),
                ExpectedKeyPasses = Math.Round(p.KeyPassesPer90, 1),
                ExpectedDribbles = Math.Round(p.DribblesPer90, 1),
                ShotsOverUnderLine = p.Position == Position.Forward ? 2.5 : 1.5,
                ShotsOverProbability = Math.Round(Math.Min(90, p.ShotsPer90 / (p.Position == Position.Forward ? 2.5 : 1.5) * 55), 1),
                FoulsOverUnderLine = 1.5,
                FoulsOverProbability = Math.Round(Math.Min(90, p.FoulsCommittedPer90 / 1.5 * 60), 1),
                KeyInsight = BuildPlayerInsight(p, opponent),
                ThreatLevel = ComputeThreatLevel(p)
            });
        }
        return result.OrderByDescending(p => p.IsStar).ThenByDescending(p => p.ExpectedGoals).ToList();
    }

    private static string BuildPlayerInsight(Player p, Team opponent)
    {
        if (p.IsKeyStar && p.GoalsPer90 > 0.5)
            return $"{p.Name} averages {p.GoalsPer90:F2} goals/90 — top scoring threat against {opponent.Name}'s {opponent.GoalsConcededPerGame:F1} conceded/game.";
        if (p.FoulsDrawnPer90 > 2.5)
            return $"{p.Name} draws {p.FoulsDrawnPer90:F1} fouls per 90 — premium foul-drawing threat. Penalty and card bets relevant.";
        if (p.FoulsCommittedPer90 > 2.0)
            return $"{p.Name} commits {p.FoulsCommittedPer90:F1} fouls per 90 — yellow card probability {p.YellowCardsPer90 * 150:F0}%.";
        if (p.Position == Position.Goalkeeper)
            return $"{p.Name} faces an opponent averaging {opponent.GoalsPerGame:F1} goals/game. Key saves expected.";
        if (p.AssistsPer90 > 0.4)
            return $"{p.Name} creates {p.KeyPassesPer90:F1} key passes/90 — primary assist threat and set-piece danger.";
        return $"{p.Name} contributes {p.GoalsPer90 + p.AssistsPer90:F2} G+A per 90. Consistent contributor.";
    }

    private static string ComputeThreatLevel(Player p)
    {
        double score = p.GoalsPer90 * 3 + p.AssistsPer90 * 2 + p.OverallRating / 100.0 + p.CurrentForm / 10.0;
        return score switch
        {
            > 4 => "Elite",
            > 2.5 => "High",
            > 1.5 => "Medium",
            _ => "Low"
        };
    }

    private static BettingAngles BuildBettingAngles(Team home, Team away, double homeXg, double awayXg,
        double totalFouls, double totalCards, double homeWin, double draw, double awayWin)
    {
        double totalXg = homeXg + awayXg;

        double over15 = 1 - Poisson(0, homeXg) * Poisson(0, awayXg) - Poisson(1, homeXg) * Poisson(0, awayXg) - Poisson(0, homeXg) * Poisson(1, awayXg);
        double over25 = ComputeOverProbability(homeXg, awayXg, 2);
        double over35 = ComputeOverProbability(homeXg, awayXg, 3);
        double btts = (1 - PoissonZero(homeXg)) * (1 - PoissonZero(awayXg));
        double homeCS = PoissonZero(awayXg);
        double awayCS = PoissonZero(homeXg);

        bool homeMoreFouls = home.FoulsPerGame > away.FoulsPerGame;
        bool homeMoreCards = (home.YellowCardsPerGame + home.RedCardsPerGame) > (away.YellowCardsPerGame + away.RedCardsPerGame);

        return new BettingAngles
        {
            Over15GoalsProbability = Math.Round(over15 * 100, 1),
            Over25GoalsProbability = Math.Round(over25 * 100, 1),
            Over35GoalsProbability = Math.Round(over35 * 100, 1),
            BothTeamsToScoreProbability = Math.Round(btts * 100, 1),
            HomeCleanSheetProbability = Math.Round(homeCS * 100, 1),
            AwayCleanSheetProbability = Math.Round(awayCS * 100, 1),
            Over35CardsProbability = Math.Round(Math.Min(90, totalCards / 3.5 * 55), 1),
            Over45CardsProbability = Math.Round(Math.Min(85, totalCards / 4.5 * 50), 1),
            Over55CardsProbability = Math.Round(Math.Min(75, totalCards / 5.5 * 45), 1),
            Over85FoulsProbability = Math.Round(Math.Min(92, totalFouls / 8.5 * 60), 1),
            Over105FoulsProbability = Math.Round(Math.Min(88, totalFouls / 10.5 * 58), 1),
            MoreFoulsTeam = homeMoreFouls ? home.Name : away.Name,
            MoreFoulsProbability = Math.Round(Math.Abs(home.FoulsPerGame - away.FoulsPerGame) / (home.FoulsPerGame + away.FoulsPerGame) * 100 + 50, 1),
            MoreCardsTeam = homeMoreCards ? home.Name : away.Name,
            MoreCardsProbability = Math.Round(62.5 + Math.Abs(home.YellowCardsPerGame - away.YellowCardsPerGame) * 5, 1),
            FirstGoalTeam = homeXg > awayXg ? home.Name : away.Name,
            FirstGoalProbability = Math.Round(homeXg / (homeXg + awayXg) * 100, 1),
            ExpectedCorners = Math.Round((home.ShotsPerGame + away.ShotsPerGame) / 3.5, 1),
            Over9CornersProbability = Math.Round(Math.Min(85, (home.ShotsPerGame + away.ShotsPerGame) / 30.0 * 80), 1),
            HalfTimeResult = homeWin > 45 ? $"{home.ShortName} Win" : draw > 35 ? "Draw" : $"{away.ShortName} Win",
            HalfTimeResultProbability = Math.Round(Math.Max(homeWin, Math.Max(draw, awayWin)) * 0.7, 1),
            ValueBets = BuildValueBets(home, away, homeXg, awayXg, totalCards, totalFouls),
            AvoidBets = BuildAvoidBets(home, away, homeXg, awayXg)
        };
    }

    private static double ComputeOverProbability(double homeXg, double awayXg, int threshold)
    {
        double under = 0;
        for (int total = 0; total <= threshold; total++)
        {
            for (int h = 0; h <= total; h++)
            {
                int a = total - h;
                under += Poisson(h, homeXg) * Poisson(a, awayXg);
            }
        }
        return Math.Max(0, 1 - under);
    }

    private static double PoissonZero(double lambda) => Math.Exp(-lambda);

    private static List<string> BuildValueBets(Team home, Team away, double homeXg, double awayXg, double totalCards, double totalFouls)
    {
        var bets = new List<string>();

        if (homeXg + awayXg > 2.8)
            bets.Add($"Over 2.5 Goals — xG total {homeXg + awayXg:F2} strongly supports this");

        if (totalCards > 4.0)
            bets.Add($"Over 3.5 Cards — both teams have high card rates ({home.YellowCardsPerGame + away.YellowCardsPerGame:F1} avg/game combined)");

        if (totalFouls > 24)
            bets.Add($"Over 22.5 Fouls — physical styles guarantee high foul count ({totalFouls:F1} expected)");

        if (home.FoulsPerGame > away.FoulsPerGame * 1.4)
            bets.Add($"{home.Name} to receive more cards — commits {home.FoulsPerGame:F1} fouls/game vs {away.FoulsPerGame:F1}");
        else if (away.FoulsPerGame > home.FoulsPerGame * 1.4)
            bets.Add($"{away.Name} to receive more cards — commits {away.FoulsPerGame:F1} fouls/game vs {home.FoulsPerGame:F1}");

        var starForward = home.Players.Where(p => p.IsKeyStar && p.Position == Position.Forward).MaxBy(p => p.GoalsPer90);
        if (starForward != null && starForward.GoalsPer90 > 0.6)
            bets.Add($"{starForward.Name} anytime scorer — {starForward.GoalsPer90:F2} goals/90 against {away.Name}");

        return bets;
    }

    private static List<string> BuildAvoidBets(Team home, Team away, double homeXg, double awayXg)
    {
        var avoid = new List<string>();

        if (Math.Abs(homeXg - awayXg) < 0.4)
            avoid.Add("Outright winner — evenly matched teams, draw is just as likely");

        if (homeXg + awayXg < 2.0)
            avoid.Add("Over 2.5 Goals — both defenses are strong, low-scoring game likely");

        if (home.PrimaryStyle == PlayingStyle.Defensive || away.PrimaryStyle == PlayingStyle.Defensive)
            avoid.Add("BTTS — at least one defensive team likely to keep a clean sheet");

        return avoid;
    }

    private static string BuildMatchSummary(Team home, Team away, double homeXg, double awayXg, double hw, double d, double aw)
    {
        string favorite = hw > aw + 15 ? home.Name : aw > hw + 15 ? away.Name : "Neither team";
        return $"Based on my analysis: {home.Name} (FIFA #{home.FifaRanking}, form {home.CurrentForm:F1}/10) vs {away.Name} (FIFA #{away.FifaRanking}, form {away.CurrentForm:F1}/10). " +
               $"Expected goals: {homeXg:F2} — {awayXg:F2}. {favorite} are favored. " +
               $"Win probabilities: {home.ShortName} {hw:F1}% | Draw {d:F1}% | {away.ShortName} {aw:F1}%.";
    }

    private static List<string> BuildKeyFactors(Team home, Team away, double homeXg, double awayXg)
    {
        var factors = new List<string>();

        int rankDiff = Math.Abs(home.FifaRanking - away.FifaRanking);
        if (rankDiff > 30)
        {
            var stronger = home.FifaRanking < away.FifaRanking ? home : away;
            factors.Add($"FIFA ranking gap of {rankDiff} places heavily favors {stronger.Name}");
        }

        factors.Add($"Possession battle: {home.ShortName} {home.Possession:F0}% vs {away.ShortName} {away.Possession:F0}% — {(home.Possession > away.Possession ? home.Name : away.Name)} expected to dominate the ball");
        factors.Add($"Pressing intensity: {home.ShortName} {home.PressureIntensity}/100 vs {away.ShortName} {away.PressureIntensity}/100");

        if (home.GoalsConcededPerGame < 0.8 || away.GoalsConcededPerGame < 0.8)
        {
            var strongDef = home.GoalsConcededPerGame < away.GoalsConcededPerGame ? home : away;
            factors.Add($"{strongDef.Name}'s defense concedes only {strongDef.GoalsConcededPerGame:F1} per game — clean sheet probability is significant");
        }

        var homeStars = home.Players.Where(p => p.IsKeyStar).Select(p => p.Name).ToList();
        var awayStars = away.Players.Where(p => p.IsKeyStar).Select(p => p.Name).ToList();
        if (homeStars.Any()) factors.Add($"{home.Name} key threats: {string.Join(", ", homeStars)}");
        if (awayStars.Any()) factors.Add($"{away.Name} key threats: {string.Join(", ", awayStars)}");

        return factors;
    }

    private static List<string> BuildTacticalInsights(Team home, Team away)
    {
        var insights = new List<string>();

        insights.Add($"{home.Name} ({home.TacticalSetup}) vs {away.Name} ({away.TacticalSetup}) — {DescribeStyleMatchup(home.PrimaryStyle, away.PrimaryStyle)}");

        if (home.PrimaryStyle == PlayingStyle.HighPress)
            insights.Add($"{home.Name}'s high press (intensity {home.PressureIntensity}/100) will target {away.Name}'s defensive line, forcing long balls");

        if (away.PrimaryStyle == PlayingStyle.CounterAttack)
            insights.Add($"{away.Name} will invite pressure and exploit {home.Name}'s defensive line height of {home.DefensiveLineHeight}% — pace behind the defense is a threat");

        if (home.PrimaryStyle == PlayingStyle.TikiTaka || home.PrimaryStyle == PlayingStyle.Possession)
            insights.Add($"{home.Name}'s possession game ({home.Possession:F0}% average) will demand patience from {away.Name} — defensive organization is critical");

        return insights;
    }

    private static string DescribeStyleMatchup(PlayingStyle home, PlayingStyle away)
    {
        return (home, away) switch
        {
            (PlayingStyle.HighPress, PlayingStyle.Possession) => "clash of pressing vs patient build-up. High tempo, high foul count expected.",
            (PlayingStyle.TikiTaka, PlayingStyle.Defensive) => "technical dominance vs organized block. Expect a tight match with few clear chances.",
            (PlayingStyle.CounterAttack, PlayingStyle.Possession) => "possession dominance vs lethal counters. Open-space transitions will be key.",
            (PlayingStyle.Physical, PlayingStyle.TikiTaka) => "brute force vs technical elegance. Physicality could disrupt rhythm. High cards.",
            _ => $"{home} vs {away} — tactical battle expected across the midfield."
        };
    }

    private static List<string> BuildDangerZones(Team home, Team away)
    {
        var zones = new List<string>();

        var topScorer = home.Players.Where(p => p.Position == Position.Forward).MaxBy(p => p.GoalsPer90);
        if (topScorer != null)
            zones.Add($"{home.Name}: {topScorer.Name} ({topScorer.GoalsPer90:F2} G/90) — primary scoring threat from open play");

        var topDribbler = home.Players.MaxBy(p => p.DribblesPer90);
        if (topDribbler != null && topDribbler.DribblesPer90 > 2.5)
            zones.Add($"{home.Name}: {topDribbler.Name} ({topDribbler.DribblesPer90:F1} dribbles/90) — danger with ball at feet, draws fouls in dangerous areas");

        var awayTopScorer = away.Players.Where(p => p.Position == Position.Forward).MaxBy(p => p.GoalsPer90);
        if (awayTopScorer != null)
            zones.Add($"{away.Name}: {awayTopScorer.Name} ({awayTopScorer.GoalsPer90:F2} G/90) — primary scoring threat");

        if (home.FoulsPerGame > 13)
            zones.Add($"{home.Name}'s high foul rate ({home.FoulsPerGame:F1}/game) creates dangerous free-kick positions for {away.Name} set-pieces");

        return zones;
    }

    private static int ComputeConfidence(Team home, Team away)
    {
        int confidence = 70;
        if (Math.Abs(home.FifaRanking - away.FifaRanking) > 40) confidence += 12;
        if (Math.Abs(home.SquadStrength - away.SquadStrength) > 15) confidence += 8;
        if (Math.Abs(home.CurrentForm - away.CurrentForm) > 2) confidence += 5;
        return Math.Min(95, confidence);
    }

    private static string BuildVerdict(Team home, Team away, double hw, double d, double aw, double homeXg, double awayXg)
    {
        if (hw > 55) return $"Strong confidence: {home.Name} win. xG advantage ({homeXg:F2} vs {awayXg:F2}) and form support this pick.";
        if (aw > 55) return $"Strong confidence: {away.Name} win. Quality edge and form clearly favor the away side.";
        if (hw > 42) return $"Lean: {home.Name} win or draw. Evenly matched but home advantage tips the balance.";
        if (aw > 42) return $"Lean: {away.Name} win. Superior FIFA ranking and squad quality should prevail.";
        return $"Genuinely unpredictable — both teams evenly matched. Draw most likely at {d:F0}%. Look for value in goals and cards markets instead.";
    }
}
