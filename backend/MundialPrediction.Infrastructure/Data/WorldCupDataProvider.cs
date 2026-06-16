using MundialPrediction.Core.Models;

namespace MundialPrediction.Infrastructure.Data;

public static class WorldCupDataProvider
{
    public static List<Team> GetTeams() => new()
    {
        BuildFrance(),
        BuildArgentina(),
        BuildEngland(),
        BuildBrazil(),
        BuildSpain(),
        BuildGermany(),
        BuildPortugal(),
        BuildNetherlands(),
        BuildBelgium(),
        BuildUruguay(),
        BuildMorocco(),
        BuildUSA(),
        BuildMexico(),
        BuildCanada(),
        BuildJapan(),
        BuildSouthKorea(),
        BuildSenegal(),
        BuildNigeria(),
        BuildColombia(),
        BuildCroatia()
    };

    private static Team BuildFrance() => new()
    {
        Id = "fra",
        Name = "France",
        ShortName = "FRA",
        FlagEmoji = "🇫🇷",
        Confederation = "UEFA",
        Group = "A",
        FifaRanking = 2,
        PrimaryStyle = PlayingStyle.CounterAttack,
        SecondaryStyle = PlayingStyle.HighPress,
        GoalsPerGame = 2.4,
        GoalsConcededPerGame = 0.8,
        ShotsPerGame = 16.2,
        ShotsOnTargetPerGame = 6.1,
        FoulsPerGame = 11.3,
        YellowCardsPerGame = 1.8,
        RedCardsPerGame = 0.1,
        Possession = 55.2,
        PressureIntensity = 74,
        DefensiveLineHeight = 68,
        CurrentForm = 8.2,
        RecentResults = "W W W D W",
        SquadStrength = 93,
        ManagerName = "Didier Deschamps",
        TacticalSetup = "4-3-3",
        Description = "World-class squad depth with the best player transition in the world. France play with explosive directness, using Mbappe's speed as their primary weapon. Defensively compact with aggressive pressing in midfield.",
        Players = new List<Player>
        {
            new() { Id = "mbappe", Name = "Kylian Mbappe", Age = 25, Club = "Real Madrid", Position = Position.Forward, ShirtNumber = 10, IsCaptain = true, IsKeyStar = true, OverallRating = 96, CurrentForm = 9.5, GoalsPer90 = 0.82, AssistsPer90 = 0.41, ShotsPer90 = 4.8, ShotsOnTargetPer90 = 2.2, KeyPassesPer90 = 2.1, DribblesPer90 = 3.4, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 3.1, YellowCardsPer90 = 0.12, RedCardsPer90 = 0.01, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 0.8, Description = "The fastest player in the world. Elite finisher with telepathic movement. Draws an extraordinary number of fouls due to speed.", PlayingCharacteristics = "Explosive pace, clinical finishing, elite ball control at speed, outstanding dribbling" },
            new() { Id = "griezmann", Name = "Antoine Griezmann", Age = 33, Club = "Atletico Madrid", Position = Position.Forward, ShirtNumber = 7, IsKeyStar = true, OverallRating = 88, CurrentForm = 8.1, GoalsPer90 = 0.44, AssistsPer90 = 0.58, ShotsPer90 = 2.9, ShotsOnTargetPer90 = 1.4, KeyPassesPer90 = 3.2, DribblesPer90 = 1.8, FoulsCommittedPer90 = 1.1, FoulsDrawnPer90 = 2.2, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.01, TacklesPer90 = 1.2, InterceptionsPer90 = 0.9, AerialDuelsPer90 = 1.2, Description = "Master of the half-space, excellent link-up player, big tournament performer.", PlayingCharacteristics = "Positional intelligence, creative passing, penalty box movement, big-game mentality" },
            new() { Id = "tchouameni", Name = "Aurelien Tchouameni", Age = 24, Club = "Real Madrid", Position = Position.Midfielder, ShirtNumber = 8, OverallRating = 87, CurrentForm = 8.3, GoalsPer90 = 0.11, AssistsPer90 = 0.14, ShotsPer90 = 1.2, ShotsOnTargetPer90 = 0.4, KeyPassesPer90 = 1.4, DribblesPer90 = 1.2, FoulsCommittedPer90 = 2.1, FoulsDrawnPer90 = 0.8, YellowCardsPer90 = 0.38, RedCardsPer90 = 0.02, TacklesPer90 = 3.8, InterceptionsPer90 = 2.2, AerialDuelsPer90 = 3.1, Description = "Dominant defensive midfielder with physical superiority. High foul rate reflects aggressive style.", PlayingCharacteristics = "Ball-winning, physical duels, progressive passing, positional discipline" },
            new() { Id = "upamecano", Name = "Dayot Upamecano", Age = 26, Club = "Bayern Munich", Position = Position.Defender, ShirtNumber = 4, OverallRating = 85, CurrentForm = 7.8, GoalsPer90 = 0.08, AssistsPer90 = 0.05, ShotsPer90 = 0.5, ShotsOnTargetPer90 = 0.2, KeyPassesPer90 = 0.8, DribblesPer90 = 0.3, FoulsCommittedPer90 = 1.8, FoulsDrawnPer90 = 0.2, YellowCardsPer90 = 0.41, RedCardsPer90 = 0.03, TacklesPer90 = 2.8, InterceptionsPer90 = 1.9, AerialDuelsPer90 = 4.8, Description = "Explosive centre-back prone to concentration lapses. Excellent in the air.", PlayingCharacteristics = "Pace, aerial dominance, aggressive defending, can be rash" },
            new() { Id = "maignan", Name = "Mike Maignan", Age = 28, Club = "AC Milan", Position = Position.Goalkeeper, ShirtNumber = 1, IsKeyStar = true, OverallRating = 89, CurrentForm = 9.0, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 0.8, DribblesPer90 = 0, FoulsCommittedPer90 = 0.1, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.05, RedCardsPer90 = 0, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.2, Description = "One of the best goalkeepers in the world. Elite reflexes and commanding presence.", PlayingCharacteristics = "Shot-stopping, sweeper-keeper, fast distribution, commanding in the box" },
        }
    };

    private static Team BuildArgentina() => new()
    {
        Id = "arg",
        Name = "Argentina",
        ShortName = "ARG",
        FlagEmoji = "🇦🇷",
        Confederation = "CONMEBOL",
        Group = "A",
        FifaRanking = 1,
        PrimaryStyle = PlayingStyle.Possession,
        SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 2.6,
        GoalsConcededPerGame = 0.7,
        ShotsPerGame = 15.8,
        ShotsOnTargetPerGame = 5.9,
        FoulsPerGame = 13.8,
        YellowCardsPerGame = 2.4,
        RedCardsPerGame = 0.15,
        Possession = 57.1,
        PressureIntensity = 71,
        DefensiveLineHeight = 62,
        CurrentForm = 9.1,
        RecentResults = "W W W W W",
        SquadStrength = 94,
        ManagerName = "Lionel Scaloni",
        TacticalSetup = "4-4-2 / 3-5-2",
        Description = "World Champions with exceptional team cohesion. Messi orchestrates from deep, with De Paul providing relentless pressing. Argentina are masters of transition play and set-pieces. Known for physicality — highest foul rate in top squads.",
        Players = new List<Player>
        {
            new() { Id = "messi", Name = "Lionel Messi", Age = 37, Club = "Inter Miami", Position = Position.Forward, ShirtNumber = 10, IsCaptain = true, IsKeyStar = true, OverallRating = 95, CurrentForm = 8.9, GoalsPer90 = 0.61, AssistsPer90 = 0.79, ShotsPer90 = 3.9, ShotsOnTargetPer90 = 1.8, KeyPassesPer90 = 4.8, DribblesPer90 = 2.9, FoulsCommittedPer90 = 0.6, FoulsDrawnPer90 = 4.2, YellowCardsPer90 = 0.08, RedCardsPer90 = 0.0, TacklesPer90 = 0.3, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 0.5, Description = "Greatest player of all time. Deep-lying playmaker role now, but still creates magic. Draws fouls constantly due to extraordinary dribbling.", PlayingCharacteristics = "Vision, dribbling, free kicks, penalty taking, creative passing, leadership" },
            new() { Id = "martinez_l", Name = "Lautaro Martinez", Age = 26, Club = "Inter Milan", Position = Position.Forward, ShirtNumber = 22, IsKeyStar = true, OverallRating = 91, CurrentForm = 9.2, GoalsPer90 = 0.78, AssistsPer90 = 0.31, ShotsPer90 = 4.1, ShotsOnTargetPer90 = 1.9, KeyPassesPer90 = 1.4, DribblesPer90 = 1.8, FoulsCommittedPer90 = 1.4, FoulsDrawnPer90 = 2.8, YellowCardsPer90 = 0.25, RedCardsPer90 = 0.02, TacklesPer90 = 0.8, InterceptionsPer90 = 0.6, AerialDuelsPer90 = 2.4, Description = "Clinical striker with relentless pressing. Excellent in tight spaces.", PlayingCharacteristics = "Aerial ability, movement, pressing, hold-up play, clinical finishing" },
            new() { Id = "depaul", Name = "Rodrigo De Paul", Age = 30, Club = "Atletico Madrid", Position = Position.Midfielder, ShirtNumber = 7, OverallRating = 87, CurrentForm = 8.4, GoalsPer90 = 0.14, AssistsPer90 = 0.28, ShotsPer90 = 1.8, ShotsOnTargetPer90 = 0.7, KeyPassesPer90 = 2.1, DribblesPer90 = 2.4, FoulsCommittedPer90 = 2.8, FoulsDrawnPer90 = 1.9, YellowCardsPer90 = 0.52, RedCardsPer90 = 0.04, TacklesPer90 = 3.1, InterceptionsPer90 = 1.8, AerialDuelsPer90 = 2.1, Description = "Argentina's engine. Most fouls and cards of any midfielder in the squad — a necessary evil.", PlayingCharacteristics = "Ball-winning, pressing, driving runs, physicality, high card risk" },
            new() { Id = "dimarco", Name = "Federico Dimarco", Age = 26, Club = "Inter Milan", Position = Position.Defender, ShirtNumber = 3, OverallRating = 83, CurrentForm = 8.1, GoalsPer90 = 0.12, AssistsPer90 = 0.38, ShotsPer90 = 1.4, ShotsOnTargetPer90 = 0.6, KeyPassesPer90 = 2.8, DribblesPer90 = 1.4, FoulsCommittedPer90 = 1.4, FoulsDrawnPer90 = 0.9, YellowCardsPer90 = 0.28, RedCardsPer90 = 0.01, TacklesPer90 = 2.1, InterceptionsPer90 = 1.4, AerialDuelsPer90 = 1.8, Description = "Dangerous from set-pieces and open play. Creative left-back.", PlayingCharacteristics = "Dead ball delivery, overlapping runs, crossing, defensive work rate" },
            new() { Id = "martinez_e", Name = "Emiliano Martinez", Age = 31, Club = "Aston Villa", Position = Position.Goalkeeper, ShirtNumber = 23, IsKeyStar = true, OverallRating = 88, CurrentForm = 8.8, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 0.4, DribblesPer90 = 0, FoulsCommittedPer90 = 0.1, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.08, RedCardsPer90 = 0.01, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.8, Description = "World Cup winning goalkeeper. Exceptional penalty saver and psychological warrior.", PlayingCharacteristics = "Penalty saving, shot-stopping, sweeping, intimidation tactics" },
        }
    };

    private static Team BuildEngland() => new()
    {
        Id = "eng",
        Name = "England",
        ShortName = "ENG",
        FlagEmoji = "🏴󠁧󠁢󠁥󠁮󠁧󠁿",
        Confederation = "UEFA",
        Group = "B",
        FifaRanking = 5,
        PrimaryStyle = PlayingStyle.Direct,
        SecondaryStyle = PlayingStyle.HighPress,
        GoalsPerGame = 2.1,
        GoalsConcededPerGame = 0.9,
        ShotsPerGame = 17.1,
        ShotsOnTargetPerGame = 5.8,
        FoulsPerGame = 10.8,
        YellowCardsPerGame = 1.6,
        RedCardsPerGame = 0.08,
        Possession = 58.4,
        PressureIntensity = 76,
        DefensiveLineHeight = 70,
        CurrentForm = 7.9,
        RecentResults = "W D W W L",
        SquadStrength = 90,
        ManagerName = "Gareth Southgate",
        TacticalSetup = "4-3-3 / 4-2-3-1",
        Description = "Generational talent finally maturing. Bellingham is the heartbeat, with Saka and Kane providing deadly end product. England's set-piece delivery is among the best in the world.",
        Players = new List<Player>
        {
            new() { Id = "bellingham", Name = "Jude Bellingham", Age = 21, Club = "Real Madrid", Position = Position.Midfielder, ShirtNumber = 10, IsCaptain = false, IsKeyStar = true, OverallRating = 93, CurrentForm = 9.4, GoalsPer90 = 0.48, AssistsPer90 = 0.42, ShotsPer90 = 3.1, ShotsOnTargetPer90 = 1.4, KeyPassesPer90 = 3.2, DribblesPer90 = 2.8, FoulsCommittedPer90 = 1.4, FoulsDrawnPer90 = 2.1, YellowCardsPer90 = 0.28, RedCardsPer90 = 0.01, TacklesPer90 = 2.4, InterceptionsPer90 = 1.8, AerialDuelsPer90 = 2.2, Description = "Complete midfielder of his generation. Box-to-box brilliance with elite goal scoring for a midfielder.", PlayingCharacteristics = "Box-to-box dynamism, goalscoring, physicality, leadership, late runs into box" },
            new() { Id = "kane", Name = "Harry Kane", Age = 30, Club = "Bayern Munich", Position = Position.Forward, ShirtNumber = 9, IsCaptain = true, IsKeyStar = true, OverallRating = 92, CurrentForm = 8.8, GoalsPer90 = 0.91, AssistsPer90 = 0.38, ShotsPer90 = 4.4, ShotsOnTargetPer90 = 2.1, KeyPassesPer90 = 2.4, DribblesPer90 = 0.8, FoulsCommittedPer90 = 0.9, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 4.1, Description = "Clinical striker and penalty specialist. Drops deep to create, lethal in the box.", PlayingCharacteristics = "Hold-up play, penalty taking, aerial threat, link-up play, movement" },
            new() { Id = "saka", Name = "Bukayo Saka", Age = 22, Club = "Arsenal", Position = Position.Forward, ShirtNumber = 7, IsKeyStar = true, OverallRating = 88, CurrentForm = 8.9, GoalsPer90 = 0.42, AssistsPer90 = 0.48, ShotsPer90 = 3.2, ShotsOnTargetPer90 = 1.4, KeyPassesPer90 = 3.1, DribblesPer90 = 3.8, FoulsCommittedPer90 = 0.6, FoulsDrawnPer90 = 3.4, YellowCardsPer90 = 0.08, RedCardsPer90 = 0.0, TacklesPer90 = 0.8, InterceptionsPer90 = 0.6, AerialDuelsPer90 = 0.4, Description = "Draws the most fouls of any England player. Consistent, high-quality performer.", PlayingCharacteristics = "Dribbling, consistency, fouls drawn, crossing, penalty taking" },
            new() { Id = "rice", Name = "Declan Rice", Age = 25, Club = "Arsenal", Position = Position.Midfielder, ShirtNumber = 4, OverallRating = 87, CurrentForm = 8.7, GoalsPer90 = 0.21, AssistsPer90 = 0.18, ShotsPer90 = 1.4, ShotsOnTargetPer90 = 0.5, KeyPassesPer90 = 2.1, DribblesPer90 = 1.4, FoulsCommittedPer90 = 1.8, FoulsDrawnPer90 = 0.8, YellowCardsPer90 = 0.32, RedCardsPer90 = 0.01, TacklesPer90 = 3.8, InterceptionsPer90 = 2.4, AerialDuelsPer90 = 2.8, Description = "England's defensive anchor and set-piece taker. Underrated going forward.", PlayingCharacteristics = "Ball recovery, progressive carrying, set-piece delivery, physicality" },
            new() { Id = "pickford", Name = "Jordan Pickford", Age = 30, Club = "Everton", Position = Position.Goalkeeper, ShirtNumber = 1, OverallRating = 83, CurrentForm = 7.9, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 0.3, DribblesPer90 = 0, FoulsCommittedPer90 = 0.1, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.06, RedCardsPer90 = 0, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.4, Description = "Experienced tournament goalkeeper. Good penalty saver.", PlayingCharacteristics = "Reflexes, distribution, penalty saves, commanding" },
        }
    };

    private static Team BuildBrazil() => new()
    {
        Id = "bra",
        Name = "Brazil",
        ShortName = "BRA",
        FlagEmoji = "🇧🇷",
        Confederation = "CONMEBOL",
        Group = "B",
        FifaRanking = 4,
        PrimaryStyle = PlayingStyle.Possession,
        SecondaryStyle = PlayingStyle.HighPress,
        GoalsPerGame = 2.3,
        GoalsConcededPerGame = 0.9,
        ShotsPerGame = 16.8,
        ShotsOnTargetPerGame = 6.2,
        FoulsPerGame = 12.4,
        YellowCardsPerGame = 2.1,
        RedCardsPerGame = 0.12,
        Possession = 60.1,
        PressureIntensity = 78,
        DefensiveLineHeight = 65,
        CurrentForm = 8.4,
        RecentResults = "W W D W W",
        SquadStrength = 92,
        ManagerName = "Dorival Junior",
        TacticalSetup = "4-2-3-1",
        Description = "Brazil's golden generation features the electrifying Vinicius Jr alongside Rodrygo and Endrick. Play beautiful attacking football with world-class technical ability throughout the squad.",
        Players = new List<Player>
        {
            new() { Id = "vinicius", Name = "Vinicius Jr", Age = 24, Club = "Real Madrid", Position = Position.Forward, ShirtNumber = 7, IsKeyStar = true, OverallRating = 94, CurrentForm = 9.6, GoalsPer90 = 0.72, AssistsPer90 = 0.52, ShotsPer90 = 4.2, ShotsOnTargetPer90 = 1.9, KeyPassesPer90 = 2.4, DribblesPer90 = 4.8, FoulsCommittedPer90 = 0.7, FoulsDrawnPer90 = 4.1, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.01, TacklesPer90 = 0.5, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 0.6, Description = "Most dangerous winger in the world. Elite dribbler who draws constant fouls. Match-winner on any day.", PlayingCharacteristics = "Explosive dribbling, pace, finishing, fouls drawn, creativity" },
            new() { Id = "rodrygo", Name = "Rodrygo", Age = 23, Club = "Real Madrid", Position = Position.Forward, ShirtNumber = 11, IsKeyStar = true, OverallRating = 88, CurrentForm = 8.8, GoalsPer90 = 0.51, AssistsPer90 = 0.44, ShotsPer90 = 3.4, ShotsOnTargetPer90 = 1.5, KeyPassesPer90 = 2.2, DribblesPer90 = 3.1, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 2.8, YellowCardsPer90 = 0.10, RedCardsPer90 = 0.0, TacklesPer90 = 0.6, InterceptionsPer90 = 0.5, AerialDuelsPer90 = 0.9, Description = "Big-game performer with technical brilliance. Complements Vinicius perfectly.", PlayingCharacteristics = "Technical skill, big-game temperament, dribbling, intelligent movement" },
            new() { Id = "casemiro", Name = "Casemiro", Age = 32, Club = "Manchester United", Position = Position.Midfielder, ShirtNumber = 5, OverallRating = 87, CurrentForm = 7.8, GoalsPer90 = 0.12, AssistsPer90 = 0.08, ShotsPer90 = 0.9, ShotsOnTargetPer90 = 0.3, KeyPassesPer90 = 1.1, DribblesPer90 = 0.8, FoulsCommittedPer90 = 2.4, FoulsDrawnPer90 = 0.6, YellowCardsPer90 = 0.48, RedCardsPer90 = 0.04, TacklesPer90 = 3.4, InterceptionsPer90 = 2.1, AerialDuelsPer90 = 3.2, Description = "Veteran destroyer. High card risk but essential for defensive stability.", PlayingCharacteristics = "Physicality, ball-winning, card risk, leadership, positional reading" },
            new() { Id = "alisson", Name = "Alisson Becker", Age = 31, Club = "Liverpool", Position = Position.Goalkeeper, ShirtNumber = 1, IsKeyStar = true, OverallRating = 92, CurrentForm = 9.1, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 0.9, DribblesPer90 = 0, FoulsCommittedPer90 = 0.05, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.02, RedCardsPer90 = 0, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.9, Description = "Best goalkeeper in the world. Elite shot-stopper with exceptional distribution.", PlayingCharacteristics = "World-class shot-stopping, distribution, commanding, composure" },
        }
    };

    private static Team BuildSpain() => new()
    {
        Id = "esp",
        Name = "Spain",
        ShortName = "ESP",
        FlagEmoji = "🇪🇸",
        Confederation = "UEFA",
        Group = "C",
        FifaRanking = 7,
        PrimaryStyle = PlayingStyle.TikiTaka,
        SecondaryStyle = PlayingStyle.HighPress,
        GoalsPerGame = 2.2,
        GoalsConcededPerGame = 0.7,
        ShotsPerGame = 18.4,
        ShotsOnTargetPerGame = 6.8,
        FoulsPerGame = 9.8,
        YellowCardsPerGame = 1.4,
        RedCardsPerGame = 0.06,
        Possession = 64.8,
        PressureIntensity = 82,
        DefensiveLineHeight = 72,
        CurrentForm = 8.8,
        RecentResults = "W W W W D",
        SquadStrength = 91,
        ManagerName = "Luis de la Fuente",
        TacticalSetup = "4-3-3 / 4-1-4-1",
        Description = "Euro 2024 Champions with the youngest and most exciting squad in Europe. Lamine Yamal at 17 is the next Messi-level talent. Spain dominate possession and press intensely — least fouls of any top team but most ball retention.",
        Players = new List<Player>
        {
            new() { Id = "yamal", Name = "Lamine Yamal", Age = 17, Club = "Barcelona", Position = Position.Forward, ShirtNumber = 19, IsKeyStar = true, OverallRating = 88, CurrentForm = 9.3, GoalsPer90 = 0.48, AssistsPer90 = 0.61, ShotsPer90 = 3.8, ShotsOnTargetPer90 = 1.6, KeyPassesPer90 = 3.8, DribblesPer90 = 4.2, FoulsCommittedPer90 = 0.4, FoulsDrawnPer90 = 3.8, YellowCardsPer90 = 0.06, RedCardsPer90 = 0.0, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 0.3, Description = "Generational talent. At 17, already a Euro 2024 winner. The next great. Extraordinary dribbling and vision for his age.", PlayingCharacteristics = "Elite dribbling, vision, creativity, maturity beyond years, fouls drawn" },
            new() { Id = "pedri", Name = "Pedri", Age = 22, Club = "Barcelona", Position = Position.Midfielder, ShirtNumber = 8, IsKeyStar = true, OverallRating = 89, CurrentForm = 8.6, GoalsPer90 = 0.21, AssistsPer90 = 0.38, ShotsPer90 = 1.8, ShotsOnTargetPer90 = 0.7, KeyPassesPer90 = 4.1, DribblesPer90 = 2.8, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 2.4, YellowCardsPer90 = 0.12, RedCardsPer90 = 0.0, TacklesPer90 = 1.8, InterceptionsPer90 = 1.4, AerialDuelsPer90 = 0.6, Description = "Modern Iniesta. Controls tempo with extraordinary vision. The metronome of Spain's possession game.", PlayingCharacteristics = "Ball retention, vision, tempo control, dribbling in tight spaces" },
            new() { Id = "morata", Name = "Alvaro Morata", Age = 31, Club = "AC Milan", Position = Position.Forward, ShirtNumber = 9, IsCaptain = true, OverallRating = 83, CurrentForm = 7.9, GoalsPer90 = 0.38, AssistsPer90 = 0.28, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.1, KeyPassesPer90 = 1.4, DribblesPer90 = 0.8, FoulsCommittedPer90 = 1.2, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.01, TacklesPer90 = 0.6, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 3.4, Description = "Target man with good aerial ability. Leadership and work rate compensate for inconsistency.", PlayingCharacteristics = "Aerial ability, work rate, hold-up play, leadership" },
            new() { Id = "carvajal", Name = "Dani Carvajal", Age = 32, Club = "Real Madrid", Position = Position.Defender, ShirtNumber = 2, OverallRating = 84, CurrentForm = 8.4, GoalsPer90 = 0.08, AssistsPer90 = 0.22, ShotsPer90 = 0.8, ShotsOnTargetPer90 = 0.3, KeyPassesPer90 = 1.4, DribblesPer90 = 1.1, FoulsCommittedPer90 = 1.4, FoulsDrawnPer90 = 0.6, YellowCardsPer90 = 0.28, RedCardsPer90 = 0.02, TacklesPer90 = 2.4, InterceptionsPer90 = 1.8, AerialDuelsPer90 = 1.8, Description = "Experienced, reliable right-back. Champions League pedigree.", PlayingCharacteristics = "Positioning, delivery, experience, defensive reliability" },
        }
    };

    private static Team BuildGermany() => new()
    {
        Id = "ger",
        Name = "Germany",
        ShortName = "GER",
        FlagEmoji = "🇩🇪",
        Confederation = "UEFA",
        Group = "D",
        FifaRanking = 16,
        PrimaryStyle = PlayingStyle.HighPress,
        SecondaryStyle = PlayingStyle.Possession,
        GoalsPerGame = 2.1,
        GoalsConcededPerGame = 1.1,
        ShotsPerGame = 17.8,
        ShotsOnTargetPerGame = 6.4,
        FoulsPerGame = 10.4,
        YellowCardsPerGame = 1.5,
        RedCardsPerGame = 0.07,
        Possession = 59.8,
        PressureIntensity = 81,
        DefensiveLineHeight = 71,
        CurrentForm = 8.1,
        RecentResults = "W L W W W",
        SquadStrength = 89,
        ManagerName = "Julian Nagelsmann",
        TacticalSetup = "4-2-3-1 / 3-4-3",
        Description = "Rebuilding powerhouse with Musiala and Wirtz as the new generation. Germany's pressing intensity is world-class under Nagelsmann, but defensive fragility remains a concern.",
        Players = new List<Player>
        {
            new() { Id = "musiala", Name = "Jamal Musiala", Age = 21, Club = "Bayern Munich", Position = Position.Midfielder, ShirtNumber = 10, IsKeyStar = true, OverallRating = 90, CurrentForm = 9.1, GoalsPer90 = 0.44, AssistsPer90 = 0.48, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.3, KeyPassesPer90 = 3.4, DribblesPer90 = 4.1, FoulsCommittedPer90 = 0.6, FoulsDrawnPer90 = 3.2, YellowCardsPer90 = 0.08, RedCardsPer90 = 0.0, TacklesPer90 = 1.2, InterceptionsPer90 = 0.9, AerialDuelsPer90 = 0.4, Description = "Magically gifted attacking midfielder. Elite dribbler in tight spaces with great goal sense.", PlayingCharacteristics = "Elite dribbling, creativity, fouls drawn, technical brilliance, goal sense" },
            new() { Id = "wirtz", Name = "Florian Wirtz", Age = 21, Club = "Bayer Leverkusen", Position = Position.Midfielder, ShirtNumber = 13, IsKeyStar = true, OverallRating = 89, CurrentForm = 9.0, GoalsPer90 = 0.41, AssistsPer90 = 0.52, ShotsPer90 = 2.4, ShotsOnTargetPer90 = 1.1, KeyPassesPer90 = 3.8, DribblesPer90 = 3.4, FoulsCommittedPer90 = 0.5, FoulsDrawnPer90 = 2.8, YellowCardsPer90 = 0.06, RedCardsPer90 = 0.0, TacklesPer90 = 0.8, InterceptionsPer90 = 0.6, AerialDuelsPer90 = 0.5, Description = "Bundesliga's best. Incredibly creative with an eye for the spectacular.", PlayingCharacteristics = "Creativity, vision, dribbling, through balls, intelligent movement" },
            new() { Id = "kroos", Name = "Toni Kroos", Age = 34, Club = "Real Madrid", Position = Position.Midfielder, ShirtNumber = 8, OverallRating = 88, CurrentForm = 8.4, GoalsPer90 = 0.09, AssistsPer90 = 0.31, ShotsPer90 = 1.2, ShotsOnTargetPer90 = 0.4, KeyPassesPer90 = 5.2, DribblesPer90 = 0.8, FoulsCommittedPer90 = 0.9, FoulsDrawnPer90 = 0.8, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 1.4, InterceptionsPer90 = 1.2, AerialDuelsPer90 = 0.8, Description = "Passing master. Controls matches with supreme intelligence. Set-piece specialist.", PlayingCharacteristics = "Passing precision, set pieces, game control, long-range shooting" },
            new() { Id = "neuer", Name = "Manuel Neuer", Age = 38, Club = "Bayern Munich", Position = Position.Goalkeeper, ShirtNumber = 1, OverallRating = 85, CurrentForm = 7.8, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 1.1, DribblesPer90 = 0, FoulsCommittedPer90 = 0.1, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.04, RedCardsPer90 = 0, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.6, Description = "Legend of the game, aging but still elite. Pioneer of the sweeper-keeper role.", PlayingCharacteristics = "Sweeper-keeper, distribution, experience, leadership" },
        }
    };

    private static Team BuildPortugal() => new()
    {
        Id = "por",
        Name = "Portugal",
        ShortName = "POR",
        FlagEmoji = "🇵🇹",
        Confederation = "UEFA",
        Group = "E",
        FifaRanking = 6,
        PrimaryStyle = PlayingStyle.CounterAttack,
        SecondaryStyle = PlayingStyle.Possession,
        GoalsPerGame = 2.8,
        GoalsConcededPerGame = 0.9,
        ShotsPerGame = 16.4,
        ShotsOnTargetPerGame = 5.8,
        FoulsPerGame = 12.1,
        YellowCardsPerGame = 2.0,
        RedCardsPerGame = 0.11,
        Possession = 54.8,
        PressureIntensity = 69,
        DefensiveLineHeight = 60,
        CurrentForm = 8.6,
        RecentResults = "W W W W D",
        SquadStrength = 90,
        ManagerName = "Roberto Martinez",
        TacticalSetup = "4-3-3 / 4-4-2",
        Description = "Golden generation peaking at the right time. Ronaldo's influence as motivator remains, but Bruno Fernandes and Joao Felix now lead the technical quality. Portugal score freely but can be vulnerable on the break.",
        Players = new List<Player>
        {
            new() { Id = "ronaldo", Name = "Cristiano Ronaldo", Age = 39, Club = "Al-Nassr", Position = Position.Forward, ShirtNumber = 7, IsCaptain = true, IsKeyStar = true, OverallRating = 87, CurrentForm = 7.8, GoalsPer90 = 0.88, AssistsPer90 = 0.18, ShotsPer90 = 5.8, ShotsOnTargetPer90 = 2.4, KeyPassesPer90 = 0.8, DribblesPer90 = 1.4, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 1.4, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.01, TacklesPer90 = 0.2, InterceptionsPer90 = 0.2, AerialDuelsPer90 = 4.8, Description = "GOAT debate. At 39, still an extraordinary goal-scorer but pace has diminished. His aerial ability and set-piece threat remain elite. High shot volume.", PlayingCharacteristics = "Free kicks, aerial ability, penalty taking, goal instinct, experience" },
            new() { Id = "brunofernandes", Name = "Bruno Fernandes", Age = 29, Club = "Manchester United", Position = Position.Midfielder, ShirtNumber = 8, IsKeyStar = true, OverallRating = 88, CurrentForm = 8.8, GoalsPer90 = 0.38, AssistsPer90 = 0.48, ShotsPer90 = 3.1, ShotsOnTargetPer90 = 1.4, KeyPassesPer90 = 4.2, DribblesPer90 = 2.1, FoulsCommittedPer90 = 1.4, FoulsDrawnPer90 = 2.4, YellowCardsPer90 = 0.28, RedCardsPer90 = 0.02, TacklesPer90 = 1.8, InterceptionsPer90 = 1.2, AerialDuelsPer90 = 1.2, Description = "Portugal's real engine. Dictates tempo, takes set-pieces, scores and assists in big games.", PlayingCharacteristics = "Creative passing, set pieces, goal contribution, drive, leadership" },
            new() { Id = "joaofelix", Name = "Joao Felix", Age = 24, Club = "Chelsea", Position = Position.Forward, ShirtNumber = 11, IsKeyStar = true, OverallRating = 86, CurrentForm = 8.2, GoalsPer90 = 0.42, AssistsPer90 = 0.38, ShotsPer90 = 3.4, ShotsOnTargetPer90 = 1.4, KeyPassesPer90 = 2.8, DribblesPer90 = 3.1, FoulsCommittedPer90 = 0.9, FoulsDrawnPer90 = 2.8, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 0.5, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 0.8, Description = "Technically gifted with exceptional dribbling. Finding consistency at international level.", PlayingCharacteristics = "Dribbling, technical ability, creativity, agility, fouls drawn" },
            new() { Id = "costa_d", Name = "Diogo Costa", Age = 24, Club = "Porto", Position = Position.Goalkeeper, ShirtNumber = 1, OverallRating = 84, CurrentForm = 8.4, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 0.8, DribblesPer90 = 0, FoulsCommittedPer90 = 0.1, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.04, RedCardsPer90 = 0, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.4, Description = "Young elite keeper. Saved 3 penalties in one shootout at Euro 2024.", PlayingCharacteristics = "Penalty saving, reflexes, distribution, composure" },
        }
    };

    private static Team BuildNetherlands() => new()
    {
        Id = "ned",
        Name = "Netherlands",
        ShortName = "NED",
        FlagEmoji = "🇳🇱",
        Confederation = "UEFA",
        Group = "F",
        FifaRanking = 8,
        PrimaryStyle = PlayingStyle.HighPress,
        SecondaryStyle = PlayingStyle.Possession,
        GoalsPerGame = 2.0,
        GoalsConcededPerGame = 1.0,
        ShotsPerGame = 15.4,
        ShotsOnTargetPerGame = 5.2,
        FoulsPerGame = 11.8,
        YellowCardsPerGame = 1.9,
        RedCardsPerGame = 0.09,
        Possession = 57.4,
        PressureIntensity = 77,
        DefensiveLineHeight = 68,
        CurrentForm = 7.8,
        RecentResults = "W D W L W",
        SquadStrength = 87,
        ManagerName = "Ronald Koeman",
        TacticalSetup = "4-3-3",
        Description = "Holland play classic Dutch total football with modern pressing. Van Dijk anchors a solid defense, while Gakpo and Dumfries provide width and energy.",
        Players = new List<Player>
        {
            new() { Id = "vandijk", Name = "Virgil van Dijk", Age = 32, Club = "Liverpool", Position = Position.Defender, ShirtNumber = 4, IsCaptain = true, IsKeyStar = true, OverallRating = 90, CurrentForm = 8.8, GoalsPer90 = 0.14, AssistsPer90 = 0.08, ShotsPer90 = 0.9, ShotsOnTargetPer90 = 0.4, KeyPassesPer90 = 0.9, DribblesPer90 = 0.2, FoulsCommittedPer90 = 1.1, FoulsDrawnPer90 = 0.3, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 1.8, InterceptionsPer90 = 1.4, AerialDuelsPer90 = 6.2, Description = "Best defender in the world. Commanding, vocal, aerially dominant. Rarely makes mistakes.", PlayingCharacteristics = "Aerial dominance, positioning, leadership, composure, set-piece threat" },
            new() { Id = "gakpo", Name = "Cody Gakpo", Age = 25, Club = "Liverpool", Position = Position.Forward, ShirtNumber = 11, IsKeyStar = true, OverallRating = 85, CurrentForm = 8.4, GoalsPer90 = 0.58, AssistsPer90 = 0.34, ShotsPer90 = 3.4, ShotsOnTargetPer90 = 1.4, KeyPassesPer90 = 2.1, DribblesPer90 = 2.4, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 2.1, YellowCardsPer90 = 0.12, RedCardsPer90 = 0.0, TacklesPer90 = 0.6, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 1.8, Description = "Physical, direct forward with excellent goal-scoring record at international level.", PlayingCharacteristics = "Pace, power, goal-scoring, direct running, aerial ability" },
            new() { Id = "dumfries", Name = "Denzel Dumfries", Age = 28, Club = "Inter Milan", Position = Position.Defender, ShirtNumber = 2, OverallRating = 83, CurrentForm = 8.1, GoalsPer90 = 0.12, AssistsPer90 = 0.31, ShotsPer90 = 1.4, ShotsOnTargetPer90 = 0.5, KeyPassesPer90 = 1.8, DribblesPer90 = 1.8, FoulsCommittedPer90 = 1.8, FoulsDrawnPer90 = 0.9, YellowCardsPer90 = 0.31, RedCardsPer90 = 0.02, TacklesPer90 = 2.8, InterceptionsPer90 = 1.6, AerialDuelsPer90 = 2.1, Description = "Explosive right-back/wing-back. Energetic overlapping runs, defensive fouls.", PlayingCharacteristics = "Overlapping runs, physicality, crossing, defensive work" },
        }
    };

    private static Team BuildBelgium() => new()
    {
        Id = "bel",
        Name = "Belgium",
        ShortName = "BEL",
        FlagEmoji = "🇧🇪",
        Confederation = "UEFA",
        Group = "G",
        FifaRanking = 3,
        PrimaryStyle = PlayingStyle.CounterAttack,
        SecondaryStyle = PlayingStyle.Physical,
        GoalsPerGame = 2.2,
        GoalsConcededPerGame = 1.0,
        ShotsPerGame = 15.8,
        ShotsOnTargetPerGame = 5.4,
        FoulsPerGame = 12.8,
        YellowCardsPerGame = 2.1,
        RedCardsPerGame = 0.12,
        Possession = 53.4,
        PressureIntensity = 68,
        DefensiveLineHeight = 58,
        CurrentForm = 7.4,
        RecentResults = "W D W D W",
        SquadStrength = 88,
        ManagerName = "Domenico Tedesco",
        TacticalSetup = "4-3-3",
        Description = "Last tournament for Belgium's golden generation. De Bruyne remains elite, Lukaku is a physical beast. More experienced than exciting but dangerous on transition.",
        Players = new List<Player>
        {
            new() { Id = "debruyne", Name = "Kevin De Bruyne", Age = 33, Club = "Manchester City", Position = Position.Midfielder, ShirtNumber = 7, IsCaptain = true, IsKeyStar = true, OverallRating = 92, CurrentForm = 8.6, GoalsPer90 = 0.28, AssistsPer90 = 0.68, ShotsPer90 = 2.4, ShotsOnTargetPer90 = 1.0, KeyPassesPer90 = 5.8, DribblesPer90 = 1.8, FoulsCommittedPer90 = 1.1, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.0, TacklesPer90 = 1.4, InterceptionsPer90 = 1.0, AerialDuelsPer90 = 0.8, Description = "Greatest assist provider in Premier League history. Long-range shooting, set-piece delivery and through balls are all world-class.", PlayingCharacteristics = "Long-range passing, set pieces, long shots, vision, assists" },
            new() { Id = "lukaku", Name = "Romelu Lukaku", Age = 31, Club = "AS Roma", Position = Position.Forward, ShirtNumber = 9, IsKeyStar = true, OverallRating = 86, CurrentForm = 7.8, GoalsPer90 = 0.64, AssistsPer90 = 0.18, ShotsPer90 = 3.8, ShotsOnTargetPer90 = 1.6, KeyPassesPer90 = 0.8, DribblesPer90 = 0.9, FoulsCommittedPer90 = 1.8, FoulsDrawnPer90 = 2.8, YellowCardsPer90 = 0.21, RedCardsPer90 = 0.01, TacklesPer90 = 0.3, InterceptionsPer90 = 0.2, AerialDuelsPer90 = 4.8, Description = "Biggest and most physical striker in the tournament. Draws fouls with his power. Clinical at international level.", PlayingCharacteristics = "Physical power, aerial ability, fouls drawn, hold-up play, clinical finishing" },
        }
    };

    private static Team BuildUruguay() => new()
    {
        Id = "uru",
        Name = "Uruguay",
        ShortName = "URU",
        FlagEmoji = "🇺🇾",
        Confederation = "CONMEBOL",
        Group = "C",
        FifaRanking = 14,
        PrimaryStyle = PlayingStyle.Physical,
        SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.8,
        GoalsConcededPerGame = 0.8,
        ShotsPerGame = 13.2,
        ShotsOnTargetPerGame = 4.4,
        FoulsPerGame = 15.8,
        YellowCardsPerGame = 2.8,
        RedCardsPerGame = 0.22,
        Possession = 48.4,
        PressureIntensity = 65,
        DefensiveLineHeight = 52,
        CurrentForm = 7.6,
        RecentResults = "W W D W L",
        SquadStrength = 84,
        ManagerName = "Marcelo Bielsa",
        TacticalSetup = "4-3-1-2",
        Description = "Uruguay are the dirtiest team statistically — highest fouls and cards per game. Bielsa's intense pressing and South American physicality make this a team that opponents hate playing. Valverde and Darwin Nunez are exceptional.",
        Players = new List<Player>
        {
            new() { Id = "valverde", Name = "Federico Valverde", Age = 25, Club = "Real Madrid", Position = Position.Midfielder, ShirtNumber = 8, IsKeyStar = true, OverallRating = 89, CurrentForm = 8.9, GoalsPer90 = 0.31, AssistsPer90 = 0.28, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.1, KeyPassesPer90 = 2.1, DribblesPer90 = 2.8, FoulsCommittedPer90 = 2.8, FoulsDrawnPer90 = 1.4, YellowCardsPer90 = 0.48, RedCardsPer90 = 0.03, TacklesPer90 = 3.8, InterceptionsPer90 = 2.4, AerialDuelsPer90 = 1.8, Description = "Box-to-box powerhouse with exceptional engine. High foul rate reflects Bielsa's aggressive system. Dangerous from distance.", PlayingCharacteristics = "Work rate, box-to-box, long shots, physicality, pressing, foul risk" },
            new() { Id = "darwinnunez", Name = "Darwin Nunez", Age = 25, Club = "Liverpool", Position = Position.Forward, ShirtNumber = 19, IsKeyStar = true, OverallRating = 86, CurrentForm = 8.1, GoalsPer90 = 0.62, AssistsPer90 = 0.28, ShotsPer90 = 4.8, ShotsOnTargetPer90 = 1.8, KeyPassesPer90 = 0.9, DribblesPer90 = 1.8, FoulsCommittedPer90 = 1.4, FoulsDrawnPer90 = 2.4, YellowCardsPer90 = 0.21, RedCardsPer90 = 0.02, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 3.8, Description = "Explosive, chaotic, brilliant. Highest shot count per 90 in Uruguay squad. Impulsive but devastating.", PlayingCharacteristics = "Explosive pace, aerial ability, high shots volume, raw power, inconsistency" },
        }
    };

    private static Team BuildMorocco() => new()
    {
        Id = "mar",
        Name = "Morocco",
        ShortName = "MAR",
        FlagEmoji = "🇲🇦",
        Confederation = "CAF",
        Group = "H",
        FifaRanking = 14,
        PrimaryStyle = PlayingStyle.Defensive,
        SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.6,
        GoalsConcededPerGame = 0.6,
        ShotsPerGame = 12.8,
        ShotsOnTargetPerGame = 4.2,
        FoulsPerGame = 13.4,
        YellowCardsPerGame = 2.2,
        RedCardsPerGame = 0.14,
        Possession = 49.2,
        PressureIntensity = 72,
        DefensiveLineHeight = 55,
        CurrentForm = 7.9,
        RecentResults = "W W D W W",
        SquadStrength = 84,
        ManagerName = "Walid Regragui",
        TacticalSetup = "4-1-4-1",
        Description = "2022 World Cup semi-finalists who shocked the world. Hakimi is one of the best right-backs alive. Morocco's defensive organization under Regragui is extraordinary. They grind opponents down and hit with lethal counter-attacks.",
        Players = new List<Player>
        {
            new() { Id = "hakimi", Name = "Achraf Hakimi", Age = 25, Club = "PSG", Position = Position.Defender, ShirtNumber = 2, IsCaptain = true, IsKeyStar = true, OverallRating = 87, CurrentForm = 8.8, GoalsPer90 = 0.21, AssistsPer90 = 0.44, ShotsPer90 = 1.8, ShotsOnTargetPer90 = 0.7, KeyPassesPer90 = 2.8, DribblesPer90 = 2.8, FoulsCommittedPer90 = 1.4, FoulsDrawnPer90 = 1.4, YellowCardsPer90 = 0.24, RedCardsPer90 = 0.01, TacklesPer90 = 2.8, InterceptionsPer90 = 1.8, AerialDuelsPer90 = 1.8, Description = "Best right-back in the world. Explosive pace with elite dribbling for a defender. Sets the tempo for Morocco attacks.", PlayingCharacteristics = "Pace, overlapping runs, dribbling, crossing, defensive intensity" },
            new() { Id = "ziyech", Name = "Hakim Ziyech", Age = 31, Club = "Galatasaray", Position = Position.Forward, ShirtNumber = 22, OverallRating = 83, CurrentForm = 8.2, GoalsPer90 = 0.38, AssistsPer90 = 0.44, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.1, KeyPassesPer90 = 3.1, DribblesPer90 = 2.4, FoulsCommittedPer90 = 0.9, FoulsDrawnPer90 = 2.1, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 0.6, InterceptionsPer90 = 0.5, AerialDuelsPer90 = 0.4, Description = "Creative winger with exceptional left foot. Set-piece specialist and Morocco's creative hub.", PlayingCharacteristics = "Left foot, set pieces, creativity, dribbling, assists" },
        }
    };

    private static Team BuildUSA() => new()
    {
        Id = "usa",
        Name = "USA",
        ShortName = "USA",
        FlagEmoji = "🇺🇸",
        Confederation = "CONCACAF",
        Group = "E",
        FifaRanking = 13,
        PrimaryStyle = PlayingStyle.HighPress,
        SecondaryStyle = PlayingStyle.Direct,
        GoalsPerGame = 1.9,
        GoalsConcededPerGame = 1.1,
        ShotsPerGame = 14.4,
        ShotsOnTargetPerGame = 4.8,
        FoulsPerGame = 11.2,
        YellowCardsPerGame = 1.6,
        RedCardsPerGame = 0.08,
        Possession = 52.1,
        PressureIntensity = 79,
        DefensiveLineHeight = 65,
        CurrentForm = 7.8,
        RecentResults = "W W D W W",
        SquadStrength = 82,
        ManagerName = "Mauricio Pochettino",
        TacticalSetup = "4-3-3",
        Description = "Host nation with a golden generation. Pulisic leads a young, athletic squad capable of beating anyone on form. Playing at home is massive advantage. Weiss, McKennie, and Adams bring Premier League quality.",
        Players = new List<Player>
        {
            new() { Id = "pulisic", Name = "Christian Pulisic", Age = 26, Club = "AC Milan", Position = Position.Forward, ShirtNumber = 10, IsCaptain = true, IsKeyStar = true, OverallRating = 85, CurrentForm = 8.6, GoalsPer90 = 0.48, AssistsPer90 = 0.38, ShotsPer90 = 3.4, ShotsOnTargetPer90 = 1.4, KeyPassesPer90 = 2.4, DribblesPer90 = 3.1, FoulsCommittedPer90 = 0.6, FoulsDrawnPer90 = 2.8, YellowCardsPer90 = 0.10, RedCardsPer90 = 0.0, TacklesPer90 = 0.8, InterceptionsPer90 = 0.6, AerialDuelsPer90 = 0.6, Description = "America's captain and talisman. Dribbler who draws fouls and provides end product.", PlayingCharacteristics = "Dribbling, pace, fouls drawn, leadership, big-game performer" },
            new() { Id = "mckennie", Name = "Weston McKennie", Age = 26, Club = "Juventus", Position = Position.Midfielder, ShirtNumber = 8, OverallRating = 82, CurrentForm = 7.9, GoalsPer90 = 0.24, AssistsPer90 = 0.18, ShotsPer90 = 1.8, ShotsOnTargetPer90 = 0.7, KeyPassesPer90 = 1.4, DribblesPer90 = 1.4, FoulsCommittedPer90 = 2.1, FoulsDrawnPer90 = 0.8, YellowCardsPer90 = 0.38, RedCardsPer90 = 0.02, TacklesPer90 = 2.8, InterceptionsPer90 = 1.8, AerialDuelsPer90 = 2.8, Description = "Box-to-box midfielder with aerial threat and high energy. Card risk.", PlayingCharacteristics = "Aerial ability, work rate, set-piece threat, physicality, card risk" },
        }
    };

    private static Team BuildMexico() => new()
    {
        Id = "mex",
        Name = "Mexico",
        ShortName = "MEX",
        FlagEmoji = "🇲🇽",
        Confederation = "CONCACAF",
        Group = "F",
        FifaRanking = 15,
        PrimaryStyle = PlayingStyle.Possession,
        SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.7,
        GoalsConcededPerGame = 1.1,
        ShotsPerGame = 13.8,
        ShotsOnTargetPerGame = 4.4,
        FoulsPerGame = 12.8,
        YellowCardsPerGame = 2.0,
        RedCardsPerGame = 0.13,
        Possession = 55.4,
        PressureIntensity = 68,
        DefensiveLineHeight = 58,
        CurrentForm = 7.2,
        RecentResults = "D W L W D",
        SquadStrength = 80,
        ManagerName = "Jaime Lozano",
        TacticalSetup = "4-4-2 / 4-3-3",
        Description = "Experienced squad in transition. Mexico fans guarantee electric atmosphere at home games. Jimenez provides physical presence, Lozano speed on the counter.",
        Players = new List<Player>
        {
            new() { Id = "jimenez", Name = "Raul Jimenez", Age = 33, Club = "Fulham", Position = Position.Forward, ShirtNumber = 9, IsKeyStar = true, OverallRating = 81, CurrentForm = 7.8, GoalsPer90 = 0.48, AssistsPer90 = 0.21, ShotsPer90 = 3.1, ShotsOnTargetPer90 = 1.2, KeyPassesPer90 = 1.1, DribblesPer90 = 0.8, FoulsCommittedPer90 = 1.1, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.01, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 3.8, Description = "Target man and leader. Remarkable recovery from career-threatening skull fracture.", PlayingCharacteristics = "Aerial ability, hold-up play, leadership, clinical" },
            new() { Id = "lozano", Name = "Hirving Lozano", Age = 29, Club = "PSV Eindhoven", Position = Position.Forward, ShirtNumber = 22, OverallRating = 82, CurrentForm = 8.1, GoalsPer90 = 0.42, AssistsPer90 = 0.34, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.1, KeyPassesPer90 = 1.8, DribblesPer90 = 3.4, FoulsCommittedPer90 = 0.6, FoulsDrawnPer90 = 2.4, YellowCardsPer90 = 0.10, RedCardsPer90 = 0.0, TacklesPer90 = 0.6, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 0.4, Description = "El Chucky. Explosive winger with pace to beat any defender.", PlayingCharacteristics = "Explosive pace, dribbling, direct running, fouls drawn" },
        }
    };

    private static Team BuildCanada() => new()
    {
        Id = "can",
        Name = "Canada",
        ShortName = "CAN",
        FlagEmoji = "🇨🇦",
        Confederation = "CONCACAF",
        Group = "G",
        FifaRanking = 48,
        PrimaryStyle = PlayingStyle.HighPress,
        SecondaryStyle = PlayingStyle.Direct,
        GoalsPerGame = 1.6,
        GoalsConcededPerGame = 1.3,
        ShotsPerGame = 13.4,
        ShotsOnTargetPerGame = 4.2,
        FoulsPerGame = 11.8,
        YellowCardsPerGame = 1.8,
        RedCardsPerGame = 0.09,
        Possession = 50.2,
        PressureIntensity = 78,
        DefensiveLineHeight = 63,
        CurrentForm = 7.4,
        RecentResults = "W W D L W",
        SquadStrength = 76,
        ManagerName = "Jesse Marsch",
        TacticalSetup = "4-3-3",
        Description = "Rising team with the extraordinary Alphonso Davies. Jonathan David is one of the most prolific strikers in Europe. Canada's biggest World Cup with home advantage.",
        Players = new List<Player>
        {
            new() { Id = "davies", Name = "Alphonso Davies", Age = 23, Club = "Bayern Munich", Position = Position.Defender, ShirtNumber = 3, IsKeyStar = true, OverallRating = 87, CurrentForm = 8.9, GoalsPer90 = 0.14, AssistsPer90 = 0.48, ShotsPer90 = 1.4, ShotsOnTargetPer90 = 0.5, KeyPassesPer90 = 2.4, DribblesPer90 = 3.8, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 2.4, InterceptionsPer90 = 1.6, AerialDuelsPer90 = 0.8, Description = "One of the fastest humans on earth. Transforms into an attacking weapon from left-back.", PlayingCharacteristics = "Explosive pace, dribbling, overlapping, energy, crossing" },
            new() { Id = "david", Name = "Jonathan David", Age = 24, Club = "Lille", Position = Position.Forward, ShirtNumber = 20, IsKeyStar = true, OverallRating = 85, CurrentForm = 8.7, GoalsPer90 = 0.82, AssistsPer90 = 0.24, ShotsPer90 = 3.8, ShotsOnTargetPer90 = 1.8, KeyPassesPer90 = 0.9, DribblesPer90 = 1.4, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.10, RedCardsPer90 = 0.0, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 1.4, Description = "Prolific Ligue 1 scorer. Clinical finisher with impressive movement.", PlayingCharacteristics = "Clinical finishing, movement, pace, consistent scoring" },
        }
    };

    private static Team BuildJapan() => new()
    {
        Id = "jpn",
        Name = "Japan",
        ShortName = "JPN",
        FlagEmoji = "🇯🇵",
        Confederation = "AFC",
        Group = "C",
        FifaRanking = 17,
        PrimaryStyle = PlayingStyle.HighPress,
        SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 2.0,
        GoalsConcededPerGame = 1.0,
        ShotsPerGame = 14.8,
        ShotsOnTargetPerGame = 5.2,
        FoulsPerGame = 9.4,
        YellowCardsPerGame = 1.2,
        RedCardsPerGame = 0.04,
        Possession = 53.8,
        PressureIntensity = 85,
        DefensiveLineHeight = 70,
        CurrentForm = 8.4,
        RecentResults = "W W W D W",
        SquadStrength = 82,
        ManagerName = "Hajime Moriyasu",
        TacticalSetup = "4-2-3-1",
        Description = "Japan's finest generation. Beat Germany and Spain in 2022. Low discipline issues — fewest cards in the tournament. Kubo is a special talent, and their collective pressing is world-class.",
        Players = new List<Player>
        {
            new() { Id = "kubo", Name = "Takefusa Kubo", Age = 23, Club = "Real Sociedad", Position = Position.Forward, ShirtNumber = 8, IsKeyStar = true, OverallRating = 84, CurrentForm = 8.8, GoalsPer90 = 0.38, AssistsPer90 = 0.44, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.2, KeyPassesPer90 = 3.1, DribblesPer90 = 3.8, FoulsCommittedPer90 = 0.4, FoulsDrawnPer90 = 2.8, YellowCardsPer90 = 0.06, RedCardsPer90 = 0.0, TacklesPer90 = 0.8, InterceptionsPer90 = 0.6, AerialDuelsPer90 = 0.4, Description = "La Liga star. Technical excellence and exceptional dribbling ability.", PlayingCharacteristics = "Dribbling, creativity, technical ability, low foul rate, fouls drawn" },
        }
    };

    private static Team BuildSouthKorea() => new()
    {
        Id = "kor",
        Name = "South Korea",
        ShortName = "KOR",
        FlagEmoji = "🇰🇷",
        Confederation = "AFC",
        Group = "D",
        FifaRanking = 23,
        PrimaryStyle = PlayingStyle.CounterAttack,
        SecondaryStyle = PlayingStyle.HighPress,
        GoalsPerGame = 1.8,
        GoalsConcededPerGame = 1.1,
        ShotsPerGame = 13.4,
        ShotsOnTargetPerGame = 4.6,
        FoulsPerGame = 11.4,
        YellowCardsPerGame = 1.6,
        RedCardsPerGame = 0.07,
        Possession = 50.8,
        PressureIntensity = 74,
        DefensiveLineHeight = 60,
        CurrentForm = 8.0,
        RecentResults = "W D W W D",
        SquadStrength = 80,
        ManagerName = "Hong Myung-bo",
        TacticalSetup = "4-2-3-1 / 3-4-3",
        Description = "Son-driven team that punches above their weight. Son Heung-min is one of the best forwards in the world. Lee Kang-in adds creativity. Kim Min-jae anchors the defense.",
        Players = new List<Player>
        {
            new() { Id = "son", Name = "Son Heung-min", Age = 32, Club = "Tottenham", Position = Position.Forward, ShirtNumber = 7, IsCaptain = true, IsKeyStar = true, OverallRating = 89, CurrentForm = 8.6, GoalsPer90 = 0.54, AssistsPer90 = 0.34, ShotsPer90 = 3.4, ShotsOnTargetPer90 = 1.5, KeyPassesPer90 = 2.1, DribblesPer90 = 2.4, FoulsCommittedPer90 = 0.5, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.06, RedCardsPer90 = 0.0, TacklesPer90 = 0.6, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 0.8, Description = "World class left-footed forward. Electrifying pace and a great left foot. One of the cleanest players in the squad.", PlayingCharacteristics = "Pace, left foot, finesse shots, movement, leadership" },
        }
    };

    private static Team BuildSenegal() => new()
    {
        Id = "sen",
        Name = "Senegal",
        ShortName = "SEN",
        FlagEmoji = "🇸🇳",
        Confederation = "CAF",
        Group = "A",
        FifaRanking = 19,
        PrimaryStyle = PlayingStyle.Physical,
        SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.7,
        GoalsConcededPerGame = 1.0,
        ShotsPerGame = 12.8,
        ShotsOnTargetPerGame = 4.2,
        FoulsPerGame = 14.2,
        YellowCardsPerGame = 2.4,
        RedCardsPerGame = 0.16,
        Possession = 47.8,
        PressureIntensity = 66,
        DefensiveLineHeight = 54,
        CurrentForm = 7.8,
        RecentResults = "W W D W L",
        SquadStrength = 82,
        ManagerName = "Aliou Cisse",
        TacticalSetup = "4-3-3",
        Description = "African champions with exceptional physical attributes. Mane when fit is devastating. High foul rate reflects their aggressive defensive approach.",
        Players = new List<Player>
        {
            new() { Id = "mane", Name = "Sadio Mane", Age = 32, Club = "Al-Nassr", Position = Position.Forward, ShirtNumber = 10, IsCaptain = true, IsKeyStar = true, OverallRating = 85, CurrentForm = 7.9, GoalsPer90 = 0.48, AssistsPer90 = 0.28, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.2, KeyPassesPer90 = 1.8, DribblesPer90 = 2.8, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 2.4, YellowCardsPer90 = 0.12, RedCardsPer90 = 0.0, TacklesPer90 = 1.1, InterceptionsPer90 = 0.8, AerialDuelsPer90 = 1.4, Description = "Africa's best player in peak years. Still dangerous despite Saudi league spell.", PlayingCharacteristics = "Pace, power, dribbling, clinical finishing, leadership" },
        }
    };

    private static Team BuildNigeria() => new()
    {
        Id = "nga",
        Name = "Nigeria",
        ShortName = "NGA",
        FlagEmoji = "🇳🇬",
        Confederation = "CAF",
        Group = "B",
        FifaRanking = 40,
        PrimaryStyle = PlayingStyle.CounterAttack,
        SecondaryStyle = PlayingStyle.Direct,
        GoalsPerGame = 1.8,
        GoalsConcededPerGame = 1.2,
        ShotsPerGame = 13.4,
        ShotsOnTargetPerGame = 4.6,
        FoulsPerGame = 13.8,
        YellowCardsPerGame = 2.2,
        RedCardsPerGame = 0.13,
        Possession = 47.4,
        PressureIntensity = 65,
        DefensiveLineHeight = 55,
        CurrentForm = 7.4,
        RecentResults = "W D L W W",
        SquadStrength = 80,
        ManagerName = "Augustine Eguavoen",
        TacticalSetup = "4-3-3 / 4-4-2",
        Description = "Super Eagles with Osimhen as their superstar. Athletic and direct, Nigeria can beat anyone on their day. High counter-attack threat.",
        Players = new List<Player>
        {
            new() { Id = "osimhen", Name = "Victor Osimhen", Age = 25, Club = "Galatasaray", Position = Position.Forward, ShirtNumber = 9, IsKeyStar = true, OverallRating = 88, CurrentForm = 8.6, GoalsPer90 = 0.78, AssistsPer90 = 0.21, ShotsPer90 = 4.2, ShotsOnTargetPer90 = 1.8, KeyPassesPer90 = 0.8, DribblesPer90 = 1.4, FoulsCommittedPer90 = 1.2, FoulsDrawnPer90 = 2.4, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.01, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 4.2, Description = "Elite goal-scorer with devastating pace and aerial ability. One of the world's best strikers.", PlayingCharacteristics = "Explosive pace, aerial ability, clinical finishing, hold-up play" },
        }
    };

    private static Team BuildColombia() => new()
    {
        Id = "col",
        Name = "Colombia",
        ShortName = "COL",
        FlagEmoji = "🇨🇴",
        Confederation = "CONMEBOL",
        Group = "D",
        FifaRanking = 11,
        PrimaryStyle = PlayingStyle.Possession,
        SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 2.1,
        GoalsConcededPerGame = 0.9,
        ShotsPerGame = 14.8,
        ShotsOnTargetPerGame = 5.1,
        FoulsPerGame = 13.4,
        YellowCardsPerGame = 2.3,
        RedCardsPerGame = 0.14,
        Possession = 56.2,
        PressureIntensity = 70,
        DefensiveLineHeight = 62,
        CurrentForm = 8.3,
        RecentResults = "W W W D W",
        SquadStrength = 85,
        ManagerName = "Nestor Lorenzo",
        TacticalSetup = "4-4-2",
        Description = "Copa America 2024 runners-up. James Rodriguez at 33 still pulls strings with magical creativity. Luis Diaz provides electric pace. Colombia are in excellent form.",
        Players = new List<Player>
        {
            new() { Id = "james", Name = "James Rodriguez", Age = 33, Club = "Rayo Vallecano", Position = Position.Midfielder, ShirtNumber = 10, IsCaptain = true, IsKeyStar = true, OverallRating = 84, CurrentForm = 8.4, GoalsPer90 = 0.21, AssistsPer90 = 0.62, ShotsPer90 = 2.1, ShotsOnTargetPer90 = 0.8, KeyPassesPer90 = 5.1, DribblesPer90 = 2.1, FoulsCommittedPer90 = 0.9, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 0.8, InterceptionsPer90 = 0.6, AerialDuelsPer90 = 0.4, Description = "2014 World Cup golden boot winner. Still the architect of everything beautiful Colombia do.", PlayingCharacteristics = "Vision, creative passing, long shots, set pieces, assists" },
            new() { Id = "luisdiaz", Name = "Luis Diaz", Age = 27, Club = "Liverpool", Position = Position.Forward, ShirtNumber = 7, IsKeyStar = true, OverallRating = 86, CurrentForm = 8.9, GoalsPer90 = 0.42, AssistsPer90 = 0.38, ShotsPer90 = 3.1, ShotsOnTargetPer90 = 1.2, KeyPassesPer90 = 2.1, DribblesPer90 = 3.8, FoulsCommittedPer90 = 0.7, FoulsDrawnPer90 = 3.1, YellowCardsPer90 = 0.10, RedCardsPer90 = 0.0, TacklesPer90 = 0.8, InterceptionsPer90 = 0.6, AerialDuelsPer90 = 0.6, Description = "Electric winger who draws constant fouls. Premier League quality at highest level.", PlayingCharacteristics = "Explosive pace, dribbling, fouls drawn, direct running, creativity" },
        }
    };

    private static Team BuildCroatia() => new()
    {
        Id = "cro",
        Name = "Croatia",
        ShortName = "CRO",
        FlagEmoji = "🇭🇷",
        Confederation = "UEFA",
        Group = "H",
        FifaRanking = 10,
        PrimaryStyle = PlayingStyle.Possession,
        SecondaryStyle = PlayingStyle.Defensive,
        GoalsPerGame = 1.7,
        GoalsConcededPerGame = 0.9,
        ShotsPerGame = 13.1,
        ShotsOnTargetPerGame = 4.4,
        FoulsPerGame = 11.4,
        YellowCardsPerGame = 1.8,
        RedCardsPerGame = 0.08,
        Possession = 58.8,
        PressureIntensity = 65,
        DefensiveLineHeight = 58,
        CurrentForm = 7.6,
        RecentResults = "D W D W W",
        SquadStrength = 84,
        ManagerName = "Zlatko Dalic",
        TacticalSetup = "4-3-3 / 3-5-2",
        Description = "2018 runners-up, 2022 third place. Modric at 38 is still the best player Croatia have had. Disciplined and experienced, they over-perform expectations in every tournament.",
        Players = new List<Player>
        {
            new() { Id = "modric", Name = "Luka Modric", Age = 38, Club = "Real Madrid", Position = Position.Midfielder, ShirtNumber = 10, IsCaptain = true, IsKeyStar = true, OverallRating = 88, CurrentForm = 8.2, GoalsPer90 = 0.14, AssistsPer90 = 0.38, ShotsPer90 = 1.8, ShotsOnTargetPer90 = 0.7, KeyPassesPer90 = 4.8, DribblesPer90 = 2.4, FoulsCommittedPer90 = 1.1, FoulsDrawnPer90 = 2.1, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.0, TacklesPer90 = 2.1, InterceptionsPer90 = 1.8, AerialDuelsPer90 = 0.8, Description = "Ballon d'Or winner. Ageless wonder who controls every match he plays. Last World Cup.", PlayingCharacteristics = "Passing mastery, dribbling, stamina, vision, leadership, free kicks" },
        }
    };

    public static List<Match> GetMatches() => new()
    {
        new() { Id = "m1", HomeTeamId = "fra", AwayTeamId = "arg", Stage = "Group", Group = "A", MatchDate = new DateTime(2026, 6, 14), Venue = "MetLife Stadium", City = "New York" },
        new() { Id = "m2", HomeTeamId = "eng", AwayTeamId = "bra", Stage = "Group", Group = "B", MatchDate = new DateTime(2026, 6, 15), Venue = "SoFi Stadium", City = "Los Angeles" },
        new() { Id = "m3", HomeTeamId = "esp", AwayTeamId = "uru", Stage = "Group", Group = "C", MatchDate = new DateTime(2026, 6, 16), Venue = "AT&T Stadium", City = "Dallas" },
        new() { Id = "m4", HomeTeamId = "ger", AwayTeamId = "col", Stage = "Group", Group = "D", MatchDate = new DateTime(2026, 6, 17), Venue = "Levi's Stadium", City = "San Francisco" },
        new() { Id = "m5", HomeTeamId = "usa", AwayTeamId = "por", Stage = "Group", Group = "E", MatchDate = new DateTime(2026, 6, 18), Venue = "Rose Bowl", City = "Los Angeles" },
        new() { Id = "m6", HomeTeamId = "ned", AwayTeamId = "mex", Stage = "Group", Group = "F", MatchDate = new DateTime(2026, 6, 19), Venue = "Estadio Azteca", City = "Mexico City" },
        new() { Id = "m7", HomeTeamId = "bel", AwayTeamId = "can", Stage = "Group", Group = "G", MatchDate = new DateTime(2026, 6, 20), Venue = "BC Place", City = "Vancouver" },
        new() { Id = "m8", HomeTeamId = "mar", AwayTeamId = "cro", Stage = "Group", Group = "H", MatchDate = new DateTime(2026, 6, 21), Venue = "BMO Field", City = "Toronto" },
    };
}
