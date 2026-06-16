using MundialPrediction.Core.Models;

namespace MundialPrediction.Infrastructure.Data;

public static class WorldCupDataProvider
{
    public static List<Team> GetTeams() => new()
    {
        // Oryginalne 20 drużyn
        BuildFrance(), BuildArgentina(), BuildEngland(), BuildBrazil(), BuildSpain(),
        BuildGermany(), BuildPortugal(), BuildNetherlands(), BuildBelgium(), BuildUruguay(),
        BuildMorocco(), BuildUSA(), BuildMexico(), BuildCanada(), BuildJapan(),
        BuildSouthKorea(), BuildSenegal(), BuildNigeria(), BuildColombia(), BuildCroatia(),
        // UEFA (dodatkowe 8)
        BuildItaly(), BuildSwitzerland(), BuildDenmark(), BuildAustria(),
        BuildTurkey(), BuildPoland(), BuildSerbia(), BuildUkraine(),
        // CONMEBOL (dodatkowe 2)
        BuildEcuador(), BuildVenezuela(),
        // CONCACAF (dodatkowe 3)
        BuildPanama(), BuildHonduras(), BuildJamaica(),
        // CAF (dodatkowe 6)
        BuildEgypt(), BuildIvoryCoast(), BuildGhana(), BuildSouthAfrica(), BuildTunisia(), BuildCameroon(),
        // AFC (dodatkowe 6)
        BuildSaudiArabia(), BuildIran(), BuildAustralia(), BuildQatar(), BuildIraq(), BuildJordan(),
        // OFC
        BuildNewZealand(),
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

    // ─── UEFA (dodatkowe) ──────────────────────────────────────────────────

    private static Team BuildItaly() => new()
    {
        Id = "ita", Name = "Italy", ShortName = "ITA", FlagEmoji = "🇮🇹",
        Confederation = "UEFA", Group = "TBD", FifaRanking = 9,
        PrimaryStyle = PlayingStyle.Possession, SecondaryStyle = PlayingStyle.Defensive,
        GoalsPerGame = 1.8, GoalsConcededPerGame = 0.8, ShotsPerGame = 14.0, ShotsOnTargetPerGame = 4.8,
        FoulsPerGame = 13.2, YellowCardsPerGame = 2.1, RedCardsPerGame = 0.1,
        Possession = 57.0, PressureIntensity = 68, DefensiveLineHeight = 62,
        CurrentForm = 7.8, RecentResults = "W W D W D", SquadStrength = 86,
        ManagerName = "Luciano Spalletti", TacticalSetup = "4-3-3",
        Description = "Tactically disciplined with world-class goalkeeper and creative midfield. Compact defensive structure, lethal on counter-attacks.",
        Players = new List<Player>
        {
            new() { Id = "donnarumma", Name = "Gianluigi Donnarumma", Age = 26, Club = "PSG", Position = Position.Goalkeeper, ShirtNumber = 1, IsKeyStar = true, OverallRating = 91, CurrentForm = 9.0, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 0.5, DribblesPer90 = 0, FoulsCommittedPer90 = 0.1, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.06, RedCardsPer90 = 0, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.5, Description = "World-class shot-stopper and commanding presence.", PlayingCharacteristics = "Reflexes, positioning, distribution, commanding" },
            new() { Id = "barella", Name = "Nicolo Barella", Age = 27, Club = "Inter Milan", Position = Position.Midfielder, ShirtNumber = 8, IsKeyStar = true, OverallRating = 88, CurrentForm = 8.5, GoalsPer90 = 0.15, AssistsPer90 = 0.32, ShotsPer90 = 1.8, ShotsOnTargetPer90 = 0.7, KeyPassesPer90 = 2.8, DribblesPer90 = 2.1, FoulsCommittedPer90 = 1.9, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.38, RedCardsPer90 = 0.02, TacklesPer90 = 3.2, InterceptionsPer90 = 2.1, AerialDuelsPer90 = 1.4, Description = "Italy's engine. Box-to-box excellence.", PlayingCharacteristics = "Ball recovery, driving runs, creativity, physicality" },
            new() { Id = "chiesa", Name = "Federico Chiesa", Age = 27, Club = "Liverpool", Position = Position.Forward, ShirtNumber = 14, IsKeyStar = true, OverallRating = 85, CurrentForm = 8.1, GoalsPer90 = 0.38, AssistsPer90 = 0.28, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.1, KeyPassesPer90 = 1.8, DribblesPer90 = 2.9, FoulsCommittedPer90 = 0.9, FoulsDrawnPer90 = 2.8, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.01, TacklesPer90 = 0.6, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 0.8, Description = "Electric winger with pace and direct running.", PlayingCharacteristics = "Dribbling, pace, direct running, fouls drawn" },
            new() { Id = "retegui", Name = "Mateo Retegui", Age = 25, Club = "Atalanta", Position = Position.Forward, ShirtNumber = 9, OverallRating = 83, CurrentForm = 7.8, GoalsPer90 = 0.55, AssistsPer90 = 0.18, ShotsPer90 = 3.2, ShotsOnTargetPer90 = 1.4, KeyPassesPer90 = 0.8, DribblesPer90 = 0.6, FoulsCommittedPer90 = 1.2, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.22, RedCardsPer90 = 0.01, TacklesPer90 = 0.4, InterceptionsPer90 = 0.2, AerialDuelsPer90 = 3.2, Description = "Physical striker with good aerial ability and goalscoring instinct.", PlayingCharacteristics = "Aerial threat, hold-up play, pressing, clinical" },
        }
    };

    private static Team BuildSwitzerland() => new()
    {
        Id = "sui", Name = "Switzerland", ShortName = "SUI", FlagEmoji = "🇨🇭",
        Confederation = "UEFA", Group = "TBD", FifaRanking = 17,
        PrimaryStyle = PlayingStyle.Defensive, SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.7, GoalsConcededPerGame = 0.9, ShotsPerGame = 13.5, ShotsOnTargetPerGame = 4.5,
        FoulsPerGame = 12.4, YellowCardsPerGame = 1.9, RedCardsPerGame = 0.09,
        Possession = 51.0, PressureIntensity = 64, DefensiveLineHeight = 58,
        CurrentForm = 7.4, RecentResults = "W D W D W", SquadStrength = 82,
        ManagerName = "Murat Yakin", TacticalSetup = "3-4-3 / 4-2-3-1",
        Description = "Organized and difficult to beat. Xhaka the heartbeat of the midfield, strong defensive unit and lethal set-pieces.",
        Players = new List<Player>
        {
            new() { Id = "sommer_y", Name = "Yann Sommer", Age = 36, Club = "Inter Milan", Position = Position.Goalkeeper, ShirtNumber = 1, IsKeyStar = true, OverallRating = 86, CurrentForm = 8.2, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 0.4, DribblesPer90 = 0, FoulsCommittedPer90 = 0.1, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.04, RedCardsPer90 = 0, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.2, Description = "Experienced, reliable goalkeeper with great reflexes.", PlayingCharacteristics = "Reflexes, distribution, commanding penalty area" },
            new() { Id = "xhaka", Name = "Granit Xhaka", Age = 32, Club = "Bayer Leverkusen", Position = Position.Midfielder, ShirtNumber = 10, IsCaptain = true, IsKeyStar = true, OverallRating = 85, CurrentForm = 8.3, GoalsPer90 = 0.18, AssistsPer90 = 0.28, ShotsPer90 = 1.6, ShotsOnTargetPer90 = 0.6, KeyPassesPer90 = 3.2, DribblesPer90 = 1.2, FoulsCommittedPer90 = 2.4, FoulsDrawnPer90 = 1.1, YellowCardsPer90 = 0.48, RedCardsPer90 = 0.05, TacklesPer90 = 3.1, InterceptionsPer90 = 2.2, AerialDuelsPer90 = 1.8, Description = "Switzerland's heartbeat. Long-range shooting and combative midfield presence.", PlayingCharacteristics = "Passing range, shooting, physical duels, leadership" },
            new() { Id = "shaqiri", Name = "Xherdan Shaqiri", Age = 33, Club = "Chicago Fire", Position = Position.Midfielder, ShirtNumber = 23, OverallRating = 80, CurrentForm = 7.4, GoalsPer90 = 0.28, AssistsPer90 = 0.38, ShotsPer90 = 2.1, ShotsOnTargetPer90 = 0.9, KeyPassesPer90 = 2.4, DribblesPer90 = 2.8, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 2.2, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.01, TacklesPer90 = 0.8, InterceptionsPer90 = 0.6, AerialDuelsPer90 = 0.5, Description = "Tournament performer. Low-centre-of-gravity dribbler with explosive shooting.", PlayingCharacteristics = "Dribbling, shooting, creativity, big game moments" },
            new() { Id = "embolo", Name = "Breel Embolo", Age = 27, Club = "Monaco", Position = Position.Forward, ShirtNumber = 7, OverallRating = 79, CurrentForm = 7.2, GoalsPer90 = 0.42, AssistsPer90 = 0.18, ShotsPer90 = 2.4, ShotsOnTargetPer90 = 1.0, KeyPassesPer90 = 0.8, DribblesPer90 = 1.8, FoulsCommittedPer90 = 1.4, FoulsDrawnPer90 = 2.1, YellowCardsPer90 = 0.20, RedCardsPer90 = 0.01, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 2.4, Description = "Physical, powerful forward who causes problems for defenders.", PlayingCharacteristics = "Strength, aerial duels, pressing, goal threat" },
        }
    };

    private static Team BuildDenmark() => new()
    {
        Id = "den", Name = "Denmark", ShortName = "DEN", FlagEmoji = "🇩🇰",
        Confederation = "UEFA", Group = "TBD", FifaRanking = 21,
        PrimaryStyle = PlayingStyle.Direct, SecondaryStyle = PlayingStyle.HighPress,
        GoalsPerGame = 1.6, GoalsConcededPerGame = 0.9, ShotsPerGame = 13.8, ShotsOnTargetPerGame = 4.6,
        FoulsPerGame = 12.1, YellowCardsPerGame = 1.7, RedCardsPerGame = 0.07,
        Possession = 53.0, PressureIntensity = 70, DefensiveLineHeight = 64,
        CurrentForm = 7.2, RecentResults = "W W D L W", SquadStrength = 80,
        ManagerName = "Kasper Hjulmand", TacticalSetup = "4-2-3-1 / 4-3-3",
        Description = "Consistent, hard-working team. Eriksen pulls the strings from midfield. Strong collective unit with good set-piece delivery.",
        Players = new List<Player>
        {
            new() { Id = "eriksen", Name = "Christian Eriksen", Age = 32, Club = "Manchester United", Position = Position.Midfielder, ShirtNumber = 10, IsKeyStar = true, OverallRating = 84, CurrentForm = 8.0, GoalsPer90 = 0.21, AssistsPer90 = 0.48, ShotsPer90 = 1.9, ShotsOnTargetPer90 = 0.7, KeyPassesPer90 = 4.2, DribblesPer90 = 1.4, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 1.4, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 1.2, InterceptionsPer90 = 0.9, AerialDuelsPer90 = 0.6, Description = "Creative genius and inspiration. The heart of Denmark's build-up play.", PlayingCharacteristics = "Creativity, vision, passing, set-pieces, movement" },
            new() { Id = "hojbjerg", Name = "Pierre-Emile Hojbjerg", Age = 29, Club = "Atletico Madrid", Position = Position.Midfielder, ShirtNumber = 8, IsCaptain = true, OverallRating = 82, CurrentForm = 7.8, GoalsPer90 = 0.12, AssistsPer90 = 0.18, ShotsPer90 = 1.2, ShotsOnTargetPer90 = 0.4, KeyPassesPer90 = 2.1, DribblesPer90 = 1.1, FoulsCommittedPer90 = 2.2, FoulsDrawnPer90 = 0.9, YellowCardsPer90 = 0.38, RedCardsPer90 = 0.02, TacklesPer90 = 4.1, InterceptionsPer90 = 2.8, AerialDuelsPer90 = 2.1, Description = "Combative midfield anchor with big-game experience.", PlayingCharacteristics = "Ball-winning, physicality, covering ground, leadership" },
            new() { Id = "wind", Name = "Jonas Wind", Age = 25, Club = "Wolfsburg", Position = Position.Forward, ShirtNumber = 9, OverallRating = 78, CurrentForm = 7.2, GoalsPer90 = 0.44, AssistsPer90 = 0.18, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.2, KeyPassesPer90 = 0.6, DribblesPer90 = 0.8, FoulsCommittedPer90 = 0.9, FoulsDrawnPer90 = 1.4, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 0.4, InterceptionsPer90 = 0.2, AerialDuelsPer90 = 2.8, Description = "Physical striker with good movement and aerial threat.", PlayingCharacteristics = "Hold-up play, aerial duels, movement, pressing" },
        }
    };

    private static Team BuildAustria() => new()
    {
        Id = "aut", Name = "Austria", ShortName = "AUT", FlagEmoji = "🇦🇹",
        Confederation = "UEFA", Group = "TBD", FifaRanking = 27,
        PrimaryStyle = PlayingStyle.HighPress, SecondaryStyle = PlayingStyle.Possession,
        GoalsPerGame = 1.8, GoalsConcededPerGame = 1.0, ShotsPerGame = 14.2, ShotsOnTargetPerGame = 4.7,
        FoulsPerGame = 13.0, YellowCardsPerGame = 1.9, RedCardsPerGame = 0.09,
        Possession = 54.0, PressureIntensity = 72, DefensiveLineHeight = 66,
        CurrentForm = 7.0, RecentResults = "W L W W D", SquadStrength = 78,
        ManagerName = "Ralf Rangnick", TacticalSetup = "4-2-3-1",
        Description = "High-energy pressing football under Rangnick. Direct, aggressive, quick transitions.",
        Players = new List<Player>
        {
            new() { Id = "sabitzer", Name = "Marcel Sabitzer", Age = 30, Club = "Borussia Dortmund", Position = Position.Midfielder, ShirtNumber = 8, IsCaptain = true, IsKeyStar = true, OverallRating = 82, CurrentForm = 7.9, GoalsPer90 = 0.22, AssistsPer90 = 0.28, ShotsPer90 = 2.1, ShotsOnTargetPer90 = 0.8, KeyPassesPer90 = 2.4, DribblesPer90 = 1.8, FoulsCommittedPer90 = 1.8, FoulsDrawnPer90 = 1.4, YellowCardsPer90 = 0.28, RedCardsPer90 = 0.02, TacklesPer90 = 2.8, InterceptionsPer90 = 1.9, AerialDuelsPer90 = 1.2, Description = "Austria's key midfielder - dynamic and direct.", PlayingCharacteristics = "Box-to-box energy, shooting, pressing, leadership" },
            new() { Id = "baumgartner", Name = "Christoph Baumgartner", Age = 25, Club = "RB Leipzig", Position = Position.Midfielder, ShirtNumber = 10, IsKeyStar = true, OverallRating = 80, CurrentForm = 7.7, GoalsPer90 = 0.32, AssistsPer90 = 0.35, ShotsPer90 = 2.4, ShotsOnTargetPer90 = 1.0, KeyPassesPer90 = 2.8, DribblesPer90 = 2.1, FoulsCommittedPer90 = 1.1, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.01, TacklesPer90 = 1.4, InterceptionsPer90 = 1.1, AerialDuelsPer90 = 0.8, Description = "Creative attacking midfielder with good goal threat.", PlayingCharacteristics = "Creativity, movement, goals, pressing" },
            new() { Id = "gregoritsch", Name = "Michael Gregoritsch", Age = 30, Club = "SC Freiburg", Position = Position.Forward, ShirtNumber = 11, OverallRating = 76, CurrentForm = 7.1, GoalsPer90 = 0.41, AssistsPer90 = 0.15, ShotsPer90 = 2.6, ShotsOnTargetPer90 = 1.0, KeyPassesPer90 = 0.6, DribblesPer90 = 0.8, FoulsCommittedPer90 = 1.0, FoulsDrawnPer90 = 1.5, YellowCardsPer90 = 0.16, RedCardsPer90 = 0.0, TacklesPer90 = 0.3, InterceptionsPer90 = 0.2, AerialDuelsPer90 = 3.0, Description = "Tall striker with aerial threat and work rate.", PlayingCharacteristics = "Height, aerial duels, pressing, goal threat" },
        }
    };

    private static Team BuildTurkey() => new()
    {
        Id = "tur", Name = "Turkey", ShortName = "TUR", FlagEmoji = "🇹🇷",
        Confederation = "UEFA", Group = "TBD", FifaRanking = 38,
        PrimaryStyle = PlayingStyle.CounterAttack, SecondaryStyle = PlayingStyle.Direct,
        GoalsPerGame = 1.6, GoalsConcededPerGame = 1.1, ShotsPerGame = 13.4, ShotsOnTargetPerGame = 4.3,
        FoulsPerGame = 13.8, YellowCardsPerGame = 2.2, RedCardsPerGame = 0.12,
        Possession = 48.0, PressureIntensity = 65, DefensiveLineHeight = 54,
        CurrentForm = 7.2, RecentResults = "W W L W D", SquadStrength = 76,
        ManagerName = "Vincenzo Montella", TacticalSetup = "4-1-4-1",
        Description = "Unpredictable and passionate. Calhanoglu the creative heart, with explosive forwards. Can be physical and aggressive.",
        Players = new List<Player>
        {
            new() { Id = "calhanoglu", Name = "Hakan Calhanoglu", Age = 31, Club = "Inter Milan", Position = Position.Midfielder, ShirtNumber = 10, IsCaptain = true, IsKeyStar = true, OverallRating = 85, CurrentForm = 8.2, GoalsPer90 = 0.28, AssistsPer90 = 0.42, ShotsPer90 = 2.4, ShotsOnTargetPer90 = 0.9, KeyPassesPer90 = 3.8, DribblesPer90 = 1.8, FoulsCommittedPer90 = 1.4, FoulsDrawnPer90 = 2.1, YellowCardsPer90 = 0.28, RedCardsPer90 = 0.01, TacklesPer90 = 3.2, InterceptionsPer90 = 2.1, AerialDuelsPer90 = 0.9, Description = "World-class regista and set-piece specialist.", PlayingCharacteristics = "Deep-lying playmaking, passing, free kicks, penalties" },
            new() { Id = "yildiz", Name = "Kenan Yildiz", Age = 20, Club = "Juventus", Position = Position.Forward, ShirtNumber = 11, IsKeyStar = true, OverallRating = 81, CurrentForm = 7.8, GoalsPer90 = 0.38, AssistsPer90 = 0.32, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.1, KeyPassesPer90 = 2.4, DribblesPer90 = 3.1, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 2.8, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 0.6, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 0.5, Description = "Electrifying young talent. Turkey's next superstar.", PlayingCharacteristics = "Dribbling, creativity, directness, youth energy" },
            new() { Id = "demiral", Name = "Merih Demiral", Age = 26, Club = "Al-Qadsiah", Position = Position.Defender, ShirtNumber = 3, OverallRating = 79, CurrentForm = 7.4, GoalsPer90 = 0.12, AssistsPer90 = 0.08, ShotsPer90 = 0.6, ShotsOnTargetPer90 = 0.2, KeyPassesPer90 = 0.9, DribblesPer90 = 0.2, FoulsCommittedPer90 = 2.1, FoulsDrawnPer90 = 0.2, YellowCardsPer90 = 0.42, RedCardsPer90 = 0.04, TacklesPer90 = 3.2, InterceptionsPer90 = 2.1, AerialDuelsPer90 = 5.2, Description = "Aggressive central defender with good aerial ability.", PlayingCharacteristics = "Aerial duels, physicality, blocking, aggression" },
        }
    };

    private static Team BuildPoland() => new()
    {
        Id = "pol", Name = "Poland", ShortName = "POL", FlagEmoji = "🇵🇱",
        Confederation = "UEFA", Group = "TBD", FifaRanking = 29,
        PrimaryStyle = PlayingStyle.CounterAttack, SecondaryStyle = PlayingStyle.Direct,
        GoalsPerGame = 1.5, GoalsConcededPerGame = 1.1, ShotsPerGame = 13.0, ShotsOnTargetPerGame = 4.2,
        FoulsPerGame = 13.5, YellowCardsPerGame = 2.0, RedCardsPerGame = 0.1,
        Possession = 46.0, PressureIntensity = 60, DefensiveLineHeight = 52,
        CurrentForm = 6.8, RecentResults = "D W L W D", SquadStrength = 77,
        ManagerName = "Michal Probierz", TacticalSetup = "4-2-3-1",
        Description = "Built around Lewandowski. Counter-attacking team that defends deep and relies on Lewandowski's world-class finishing.",
        Players = new List<Player>
        {
            new() { Id = "lewandowski", Name = "Robert Lewandowski", Age = 37, Club = "Barcelona", Position = Position.Forward, ShirtNumber = 9, IsCaptain = true, IsKeyStar = true, OverallRating = 90, CurrentForm = 8.4, GoalsPer90 = 0.82, AssistsPer90 = 0.24, ShotsPer90 = 4.2, ShotsOnTargetPer90 = 1.9, KeyPassesPer90 = 1.2, DribblesPer90 = 0.8, FoulsCommittedPer90 = 0.6, FoulsDrawnPer90 = 2.2, YellowCardsPer90 = 0.12, RedCardsPer90 = 0.0, TacklesPer90 = 0.3, InterceptionsPer90 = 0.2, AerialDuelsPer90 = 3.8, Description = "One of the greatest strikers of his generation. Clinical finisher with every type of goal.", PlayingCharacteristics = "Clinical finishing, movement, aerial threat, hold-up play, penalties" },
            new() { Id = "zielinski", Name = "Piotr Zielinski", Age = 30, Club = "Inter Milan", Position = Position.Midfielder, ShirtNumber = 10, OverallRating = 83, CurrentForm = 8.0, GoalsPer90 = 0.24, AssistsPer90 = 0.38, ShotsPer90 = 2.1, ShotsOnTargetPer90 = 0.8, KeyPassesPer90 = 3.2, DribblesPer90 = 2.1, FoulsCommittedPer90 = 0.9, FoulsDrawnPer90 = 1.6, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.01, TacklesPer90 = 1.8, InterceptionsPer90 = 1.4, AerialDuelsPer90 = 0.8, Description = "Creative midfield maestro. Poland's second most important player.", PlayingCharacteristics = "Creativity, passing, dribbling, shooting" },
            new() { Id = "szczesny", Name = "Wojciech Szczesny", Age = 35, Club = "FC Barcelona", Position = Position.Goalkeeper, ShirtNumber = 1, IsKeyStar = true, OverallRating = 85, CurrentForm = 8.1, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 0.3, DribblesPer90 = 0, FoulsCommittedPer90 = 0.1, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.05, RedCardsPer90 = 0.01, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.3, Description = "Experienced goalkeeper with big-game mentality.", PlayingCharacteristics = "Shot-stopping, reflexes, distribution, penalty saves" },
        }
    };

    private static Team BuildSerbia() => new()
    {
        Id = "srb", Name = "Serbia", ShortName = "SRB", FlagEmoji = "🇷🇸",
        Confederation = "UEFA", Group = "TBD", FifaRanking = 33,
        PrimaryStyle = PlayingStyle.Direct, SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.7, GoalsConcededPerGame = 1.2, ShotsPerGame = 14.1, ShotsOnTargetPerGame = 4.5,
        FoulsPerGame = 14.2, YellowCardsPerGame = 2.3, RedCardsPerGame = 0.14,
        Possession = 49.0, PressureIntensity = 62, DefensiveLineHeight = 55,
        CurrentForm = 6.9, RecentResults = "W D W L W", SquadStrength = 75,
        ManagerName = "Dragan Stojkovic", TacticalSetup = "3-4-2-1",
        Description = "Physically powerful with lethal attack. Vlahovic and Jovic up front, Tadic creating. Physical and direct.",
        Players = new List<Player>
        {
            new() { Id = "vlahovic", Name = "Dusan Vlahovic", Age = 25, Club = "Juventus", Position = Position.Forward, ShirtNumber = 9, IsKeyStar = true, OverallRating = 86, CurrentForm = 7.8, GoalsPer90 = 0.72, AssistsPer90 = 0.14, ShotsPer90 = 3.8, ShotsOnTargetPer90 = 1.6, KeyPassesPer90 = 0.8, DribblesPer90 = 0.8, FoulsCommittedPer90 = 1.2, FoulsDrawnPer90 = 2.1, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.01, TacklesPer90 = 0.4, InterceptionsPer90 = 0.2, AerialDuelsPer90 = 3.6, Description = "Powerful striker with explosive shot and aerial threat.", PlayingCharacteristics = "Shooting power, aerial duels, movement, physicality" },
            new() { Id = "tadic", Name = "Dusan Tadic", Age = 36, Club = "Fenerbahce", Position = Position.Midfielder, ShirtNumber = 10, IsCaptain = true, OverallRating = 82, CurrentForm = 7.5, GoalsPer90 = 0.28, AssistsPer90 = 0.52, ShotsPer90 = 2.1, ShotsOnTargetPer90 = 0.8, KeyPassesPer90 = 4.1, DribblesPer90 = 2.8, FoulsCommittedPer90 = 0.9, FoulsDrawnPer90 = 2.8, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 0.8, InterceptionsPer90 = 0.6, AerialDuelsPer90 = 0.4, Description = "Veteran creative leader. Vision and set-piece delivery.", PlayingCharacteristics = "Creativity, vision, set-pieces, dribbling, leadership" },
            new() { Id = "milinkovic_v", Name = "Vanja Milinkovic-Savic", Age = 27, Club = "Torino", Position = Position.Goalkeeper, ShirtNumber = 1, OverallRating = 80, CurrentForm = 7.6, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 0.4, DribblesPer90 = 0, FoulsCommittedPer90 = 0.1, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.05, RedCardsPer90 = 0, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.3, Description = "Tall, athletic goalkeeper with strong reflexes.", PlayingCharacteristics = "Shot-stopping, commanding, distribution" },
        }
    };

    private static Team BuildUkraine() => new()
    {
        Id = "ukr", Name = "Ukraine", ShortName = "UKR", FlagEmoji = "🇺🇦",
        Confederation = "UEFA", Group = "TBD", FifaRanking = 22,
        PrimaryStyle = PlayingStyle.Direct, SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.5, GoalsConcededPerGame = 1.1, ShotsPerGame = 13.2, ShotsOnTargetPerGame = 4.3,
        FoulsPerGame = 12.8, YellowCardsPerGame = 1.9, RedCardsPerGame = 0.08,
        Possession = 50.0, PressureIntensity = 65, DefensiveLineHeight = 58,
        CurrentForm = 6.7, RecentResults = "W D L W W", SquadStrength = 74,
        ManagerName = "Serhiy Rebrov", TacticalSetup = "4-3-3",
        Description = "Resilient team with emotional motivation. Mudryk the star, Zinchenko the leader. Physical and hardworking.",
        Players = new List<Player>
        {
            new() { Id = "mudryk", Name = "Mykhailo Mudryk", Age = 24, Club = "Chelsea", Position = Position.Forward, ShirtNumber = 10, IsKeyStar = true, OverallRating = 82, CurrentForm = 7.6, GoalsPer90 = 0.35, AssistsPer90 = 0.31, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.1, KeyPassesPer90 = 2.1, DribblesPer90 = 3.4, FoulsCommittedPer90 = 0.7, FoulsDrawnPer90 = 2.4, YellowCardsPer90 = 0.12, RedCardsPer90 = 0.0, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 0.4, Description = "Explosive pace and dribbling. Direct runner who creates danger.", PlayingCharacteristics = "Pace, dribbling, direct running, creativity" },
            new() { Id = "zinchenko", Name = "Oleksandr Zinchenko", Age = 28, Club = "Arsenal", Position = Position.Defender, ShirtNumber = 3, IsCaptain = true, OverallRating = 80, CurrentForm = 7.8, GoalsPer90 = 0.08, AssistsPer90 = 0.28, ShotsPer90 = 0.8, ShotsOnTargetPer90 = 0.3, KeyPassesPer90 = 2.8, DribblesPer90 = 1.4, FoulsCommittedPer90 = 1.2, FoulsDrawnPer90 = 0.8, YellowCardsPer90 = 0.24, RedCardsPer90 = 0.01, TacklesPer90 = 2.4, InterceptionsPer90 = 1.6, AerialDuelsPer90 = 0.8, Description = "Inverted fullback who operates like a midfielder. Leadership and technical quality.", PlayingCharacteristics = "Technical quality, leadership, positioning, build-up" },
            new() { Id = "lunin", Name = "Andriy Lunin", Age = 25, Club = "Real Madrid", Position = Position.Goalkeeper, ShirtNumber = 1, OverallRating = 82, CurrentForm = 8.0, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 0.4, DribblesPer90 = 0, FoulsCommittedPer90 = 0.1, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.04, RedCardsPer90 = 0, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.2, Description = "Young elite goalkeeper at Real Madrid. Big game experience.", PlayingCharacteristics = "Reflexes, shot-stopping, distribution, composure" },
        }
    };

    // ─── CONMEBOL (dodatkowe) ──────────────────────────────────────────────

    private static Team BuildEcuador() => new()
    {
        Id = "ecu", Name = "Ecuador", ShortName = "ECU", FlagEmoji = "🇪🇨",
        Confederation = "CONMEBOL", Group = "TBD", FifaRanking = 46,
        PrimaryStyle = PlayingStyle.Direct, SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.4, GoalsConcededPerGame = 1.2, ShotsPerGame = 12.8, ShotsOnTargetPerGame = 4.1,
        FoulsPerGame = 14.1, YellowCardsPerGame = 2.2, RedCardsPerGame = 0.12,
        Possession = 46.0, PressureIntensity = 62, DefensiveLineHeight = 52,
        CurrentForm = 6.5, RecentResults = "D W L W W", SquadStrength = 72,
        ManagerName = "Sebastian Beccacece", TacticalSetup = "4-3-3",
        Description = "Physical, direct South American style. Caicedo the star in midfield, Valencia up front.",
        Players = new List<Player>
        {
            new() { Id = "caicedo_m", Name = "Moises Caicedo", Age = 23, Club = "Chelsea", Position = Position.Midfielder, ShirtNumber = 10, IsKeyStar = true, OverallRating = 84, CurrentForm = 8.1, GoalsPer90 = 0.12, AssistsPer90 = 0.18, ShotsPer90 = 1.4, ShotsOnTargetPer90 = 0.5, KeyPassesPer90 = 2.1, DribblesPer90 = 1.8, FoulsCommittedPer90 = 2.4, FoulsDrawnPer90 = 1.2, YellowCardsPer90 = 0.48, RedCardsPer90 = 0.03, TacklesPer90 = 5.1, InterceptionsPer90 = 2.8, AerialDuelsPer90 = 2.4, Description = "Elite ball-winner and one of the best defensive midfielders in the world.", PlayingCharacteristics = "Ball-winning, tackling, physicality, pressing" },
            new() { Id = "enner_v", Name = "Enner Valencia", Age = 35, Club = "Internacional", Position = Position.Forward, ShirtNumber = 13, IsCaptain = true, OverallRating = 76, CurrentForm = 7.0, GoalsPer90 = 0.48, AssistsPer90 = 0.18, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.2, KeyPassesPer90 = 0.6, DribblesPer90 = 1.1, FoulsCommittedPer90 = 1.2, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.01, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 2.8, Description = "Ecuador legend. Strong, mobile forward who scores in big games.", PlayingCharacteristics = "Movement, aerial threat, physicality, experience" },
        }
    };

    private static Team BuildVenezuela() => new()
    {
        Id = "ven", Name = "Venezuela", ShortName = "VEN", FlagEmoji = "🇻🇪",
        Confederation = "CONMEBOL", Group = "TBD", FifaRanking = 55,
        PrimaryStyle = PlayingStyle.CounterAttack, SecondaryStyle = PlayingStyle.Direct,
        GoalsPerGame = 1.3, GoalsConcededPerGame = 1.3, ShotsPerGame = 12.0, ShotsOnTargetPerGame = 3.8,
        FoulsPerGame = 14.8, YellowCardsPerGame = 2.3, RedCardsPerGame = 0.13,
        Possession = 44.0, PressureIntensity = 58, DefensiveLineHeight = 48,
        CurrentForm = 6.2, RecentResults = "L W D W L", SquadStrength = 68,
        ManagerName = "Fernando Batista", TacticalSetup = "4-4-2",
        Description = "Emerging South American force. Physical and counter-attacking with growing talent pool.",
        Players = new List<Player>
        {
            new() { Id = "soteldo", Name = "Yeferson Soteldo", Age = 27, Club = "Santos", Position = Position.Forward, ShirtNumber = 11, IsKeyStar = true, OverallRating = 76, CurrentForm = 7.2, GoalsPer90 = 0.38, AssistsPer90 = 0.32, ShotsPer90 = 2.4, ShotsOnTargetPer90 = 1.0, KeyPassesPer90 = 2.1, DribblesPer90 = 3.8, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 3.2, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 0.4, Description = "Tiny but dangerous. Exceptional dribbler who draws fouls.", PlayingCharacteristics = "Dribbling, pace, creativity, fouls drawn" },
        }
    };

    // ─── CONCACAF (dodatkowe) ─────────────────────────────────────────────

    private static Team BuildPanama() => new()
    {
        Id = "pan", Name = "Panama", ShortName = "PAN", FlagEmoji = "🇵🇦",
        Confederation = "CONCACAF", Group = "TBD", FifaRanking = 55,
        PrimaryStyle = PlayingStyle.Defensive, SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.2, GoalsConcededPerGame = 1.3, ShotsPerGame = 11.2, ShotsOnTargetPerGame = 3.5,
        FoulsPerGame = 15.2, YellowCardsPerGame = 2.5, RedCardsPerGame = 0.15,
        Possession = 42.0, PressureIntensity = 55, DefensiveLineHeight = 44,
        CurrentForm = 6.0, RecentResults = "W D L W D", SquadStrength = 65,
        ManagerName = "Thomas Christiansen", TacticalSetup = "4-4-2",
        Description = "Compact, defensive and very physical. Relies on set-pieces and counter-attacks.",
        Players = new List<Player>
        {
            new() { Id = "godoy_m", Name = "Maximiliano Godoy", Age = 28, Club = "FC Dallas", Position = Position.Midfielder, ShirtNumber = 10, OverallRating = 72, CurrentForm = 6.8, GoalsPer90 = 0.18, AssistsPer90 = 0.24, ShotsPer90 = 1.4, ShotsOnTargetPer90 = 0.5, KeyPassesPer90 = 1.8, DribblesPer90 = 1.4, FoulsCommittedPer90 = 2.1, FoulsDrawnPer90 = 1.2, YellowCardsPer90 = 0.38, RedCardsPer90 = 0.03, TacklesPer90 = 2.8, InterceptionsPer90 = 1.8, AerialDuelsPer90 = 1.2, Description = "Creative spark in the Panama midfield.", PlayingCharacteristics = "Creativity, work rate, set-pieces" },
        }
    };

    private static Team BuildHonduras() => new()
    {
        Id = "hon", Name = "Honduras", ShortName = "HON", FlagEmoji = "🇭🇳",
        Confederation = "CONCACAF", Group = "TBD", FifaRanking = 74,
        PrimaryStyle = PlayingStyle.Defensive, SecondaryStyle = PlayingStyle.Direct,
        GoalsPerGame = 1.1, GoalsConcededPerGame = 1.5, ShotsPerGame = 10.8, ShotsOnTargetPerGame = 3.2,
        FoulsPerGame = 15.8, YellowCardsPerGame = 2.6, RedCardsPerGame = 0.18,
        Possession = 40.0, PressureIntensity = 52, DefensiveLineHeight = 42,
        CurrentForm = 5.8, RecentResults = "L D W D L", SquadStrength = 60,
        ManagerName = "Reinaldo Rueda", TacticalSetup = "4-5-1",
        Description = "Hard-working and physical. Defend deep, look for set-pieces and counter-attacks.",
        Players = new List<Player>
        {
            new() { Id = "beckeles", Name = "Romell Quioto", Age = 30, Club = "CF Montreal", Position = Position.Forward, ShirtNumber = 11, OverallRating = 70, CurrentForm = 6.4, GoalsPer90 = 0.28, AssistsPer90 = 0.22, ShotsPer90 = 2.0, ShotsOnTargetPer90 = 0.8, KeyPassesPer90 = 1.4, DribblesPer90 = 2.1, FoulsCommittedPer90 = 1.0, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.16, RedCardsPer90 = 0.01, TacklesPer90 = 0.6, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 0.8, Description = "Honduras's best attacking outlet.", PlayingCharacteristics = "Pace, dribbling, direct running" },
        }
    };

    private static Team BuildJamaica() => new()
    {
        Id = "jam", Name = "Jamaica", ShortName = "JAM", FlagEmoji = "🇯🇲",
        Confederation = "CONCACAF", Group = "TBD", FifaRanking = 57,
        PrimaryStyle = PlayingStyle.Direct, SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.2, GoalsConcededPerGame = 1.4, ShotsPerGame = 11.4, ShotsOnTargetPerGame = 3.6,
        FoulsPerGame = 14.4, YellowCardsPerGame = 2.2, RedCardsPerGame = 0.12,
        Possession = 44.0, PressureIntensity = 60, DefensiveLineHeight = 50,
        CurrentForm = 6.1, RecentResults = "W L W D W", SquadStrength = 63,
        ManagerName = "Heimir Hallgrimsson", TacticalSetup = "4-3-3",
        Description = "Athletic and energetic. Reggae Boyz with Premier League-based talent. Fast and direct.",
        Players = new List<Player>
        {
            new() { Id = "antonio_m", Name = "Michail Antonio", Age = 34, Club = "West Ham United", Position = Position.Forward, ShirtNumber = 9, IsCaptain = true, IsKeyStar = true, OverallRating = 74, CurrentForm = 6.8, GoalsPer90 = 0.41, AssistsPer90 = 0.21, ShotsPer90 = 2.4, ShotsOnTargetPer90 = 1.0, KeyPassesPer90 = 0.8, DribblesPer90 = 1.4, FoulsCommittedPer90 = 1.4, FoulsDrawnPer90 = 2.1, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.01, TacklesPer90 = 0.4, InterceptionsPer90 = 0.2, AerialDuelsPer90 = 3.1, Description = "Physical forward with Premier League experience.", PlayingCharacteristics = "Power, aerial threat, link-up play, experience" },
        }
    };

    // ─── CAF (dodatkowe) ──────────────────────────────────────────────────

    private static Team BuildEgypt() => new()
    {
        Id = "egy", Name = "Egypt", ShortName = "EGY", FlagEmoji = "🇪🇬",
        Confederation = "CAF", Group = "TBD", FifaRanking = 35,
        PrimaryStyle = PlayingStyle.CounterAttack, SecondaryStyle = PlayingStyle.Possession,
        GoalsPerGame = 1.4, GoalsConcededPerGame = 1.1, ShotsPerGame = 13.1, ShotsOnTargetPerGame = 4.2,
        FoulsPerGame = 13.4, YellowCardsPerGame = 2.0, RedCardsPerGame = 0.1,
        Possession = 48.0, PressureIntensity = 62, DefensiveLineHeight = 55,
        CurrentForm = 6.8, RecentResults = "W W D W L", SquadStrength = 73,
        ManagerName = "Hossam Hassan", TacticalSetup = "4-2-3-1",
        Description = "Built around Salah's brilliance. Defensively organized, lethal in counter-attacks through Salah.",
        Players = new List<Player>
        {
            new() { Id = "salah", Name = "Mohamed Salah", Age = 33, Club = "Liverpool", Position = Position.Forward, ShirtNumber = 10, IsCaptain = true, IsKeyStar = true, OverallRating = 91, CurrentForm = 9.0, GoalsPer90 = 0.78, AssistsPer90 = 0.58, ShotsPer90 = 4.4, ShotsOnTargetPer90 = 2.1, KeyPassesPer90 = 3.8, DribblesPer90 = 3.2, FoulsCommittedPer90 = 0.4, FoulsDrawnPer90 = 3.8, YellowCardsPer90 = 0.08, RedCardsPer90 = 0.0, TacklesPer90 = 0.6, InterceptionsPer90 = 0.5, AerialDuelsPer90 = 0.4, Description = "One of the world's best players. Relentless scorer and creator.", PlayingCharacteristics = "Pace, cutting inside, shooting, dribbling, creativity" },
            new() { Id = "elneny", Name = "Mohamed Elneny", Age = 32, Club = "Arsenal", Position = Position.Midfielder, ShirtNumber = 4, OverallRating = 76, CurrentForm = 7.1, GoalsPer90 = 0.08, AssistsPer90 = 0.12, ShotsPer90 = 0.8, ShotsOnTargetPer90 = 0.3, KeyPassesPer90 = 1.8, DribblesPer90 = 0.8, FoulsCommittedPer90 = 2.1, FoulsDrawnPer90 = 0.8, YellowCardsPer90 = 0.38, RedCardsPer90 = 0.02, TacklesPer90 = 3.2, InterceptionsPer90 = 2.1, AerialDuelsPer90 = 1.4, Description = "Experienced midfielder providing defensive cover for Salah.", PlayingCharacteristics = "Defensive work, ball recovery, experience" },
        }
    };

    private static Team BuildIvoryCoast() => new()
    {
        Id = "civ", Name = "Ivory Coast", ShortName = "CIV", FlagEmoji = "🇨🇮",
        Confederation = "CAF", Group = "TBD", FifaRanking = 46,
        PrimaryStyle = PlayingStyle.Direct, SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.5, GoalsConcededPerGame = 1.2, ShotsPerGame = 13.5, ShotsOnTargetPerGame = 4.3,
        FoulsPerGame = 13.8, YellowCardsPerGame = 2.1, RedCardsPerGame = 0.11,
        Possession = 46.0, PressureIntensity = 64, DefensiveLineHeight = 54,
        CurrentForm = 6.9, RecentResults = "W W D L W", SquadStrength = 72,
        ManagerName = "Emerse Fae", TacticalSetup = "4-3-3",
        Description = "Physical and direct with creative talent. Sangare dominating midfield, Pepe and Zaha in attack.",
        Players = new List<Player>
        {
            new() { Id = "sangare", Name = "Ibrahim Sangare", Age = 27, Club = "Nottingham Forest", Position = Position.Midfielder, ShirtNumber = 8, IsKeyStar = true, OverallRating = 82, CurrentForm = 7.9, GoalsPer90 = 0.11, AssistsPer90 = 0.14, ShotsPer90 = 1.2, ShotsOnTargetPer90 = 0.4, KeyPassesPer90 = 1.8, DribblesPer90 = 1.4, FoulsCommittedPer90 = 2.8, FoulsDrawnPer90 = 1.1, YellowCardsPer90 = 0.48, RedCardsPer90 = 0.03, TacklesPer90 = 4.8, InterceptionsPer90 = 2.8, AerialDuelsPer90 = 3.2, Description = "Physical midfield powerhouse who dominates opponents.", PlayingCharacteristics = "Tackling, physicality, ball recovery, aerial duels" },
            new() { Id = "zaha", Name = "Wilfried Zaha", Age = 32, Club = "Al-Qadsiah", Position = Position.Forward, ShirtNumber = 10, OverallRating = 79, CurrentForm = 7.4, GoalsPer90 = 0.38, AssistsPer90 = 0.28, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.1, KeyPassesPer90 = 1.8, DribblesPer90 = 3.8, FoulsCommittedPer90 = 0.6, FoulsDrawnPer90 = 3.4, YellowCardsPer90 = 0.12, RedCardsPer90 = 0.0, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 0.5, Description = "Explosive dribbler who draws fouls and creates danger constantly.", PlayingCharacteristics = "Dribbling, pace, direct running, creativity" },
        }
    };

    private static Team BuildGhana() => new()
    {
        Id = "gha", Name = "Ghana", ShortName = "GHA", FlagEmoji = "🇬🇭",
        Confederation = "CAF", Group = "TBD", FifaRanking = 62,
        PrimaryStyle = PlayingStyle.Direct, SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.4, GoalsConcededPerGame = 1.3, ShotsPerGame = 12.8, ShotsOnTargetPerGame = 4.0,
        FoulsPerGame = 14.2, YellowCardsPerGame = 2.2, RedCardsPerGame = 0.12,
        Possession = 44.0, PressureIntensity = 60, DefensiveLineHeight = 50,
        CurrentForm = 6.3, RecentResults = "W D L W W", SquadStrength = 68,
        ManagerName = "Otto Addo", TacticalSetup = "4-2-3-1",
        Description = "Athletic and energetic. Black Stars with Premier League-based talent throughout the squad.",
        Players = new List<Player>
        {
            new() { Id = "partey", Name = "Thomas Partey", Age = 32, Club = "Arsenal", Position = Position.Midfielder, ShirtNumber = 5, IsCaptain = true, IsKeyStar = true, OverallRating = 82, CurrentForm = 7.8, GoalsPer90 = 0.11, AssistsPer90 = 0.14, ShotsPer90 = 1.2, ShotsOnTargetPer90 = 0.4, KeyPassesPer90 = 2.1, DribblesPer90 = 1.4, FoulsCommittedPer90 = 2.2, FoulsDrawnPer90 = 1.0, YellowCardsPer90 = 0.42, RedCardsPer90 = 0.02, TacklesPer90 = 4.2, InterceptionsPer90 = 2.6, AerialDuelsPer90 = 2.4, Description = "Physical midfield monster with Arsenal pedigree.", PlayingCharacteristics = "Ball-winning, physicality, range of passing, leadership" },
            new() { Id = "kudus", Name = "Mohammed Kudus", Age = 24, Club = "West Ham United", Position = Position.Midfielder, ShirtNumber = 10, IsKeyStar = true, OverallRating = 82, CurrentForm = 8.0, GoalsPer90 = 0.42, AssistsPer90 = 0.32, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.2, KeyPassesPer90 = 2.4, DribblesPer90 = 3.1, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 2.8, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 0.8, InterceptionsPer90 = 0.6, AerialDuelsPer90 = 0.6, Description = "Exciting attacking midfielder with goalscoring instinct and dribbling ability.", PlayingCharacteristics = "Dribbling, goalscoring, creativity, directness" },
        }
    };

    private static Team BuildSouthAfrica() => new()
    {
        Id = "rsa", Name = "South Africa", ShortName = "RSA", FlagEmoji = "🇿🇦",
        Confederation = "CAF", Group = "TBD", FifaRanking = 60,
        PrimaryStyle = PlayingStyle.Defensive, SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.3, GoalsConcededPerGame = 1.4, ShotsPerGame = 11.8, ShotsOnTargetPerGame = 3.7,
        FoulsPerGame = 14.1, YellowCardsPerGame = 2.1, RedCardsPerGame = 0.11,
        Possession = 43.0, PressureIntensity = 58, DefensiveLineHeight = 46,
        CurrentForm = 6.1, RecentResults = "L W D W D", SquadStrength = 65,
        ManagerName = "Hugo Broos", TacticalSetup = "4-5-1",
        Description = "Organized and disciplined. Bafana Bafana defend deep and break quickly. Strong goalkeeper.",
        Players = new List<Player>
        {
            new() { Id = "williams_r", Name = "Ronwen Williams", Age = 32, Club = "Mamelodi Sundowns", Position = Position.Goalkeeper, ShirtNumber = 1, IsKeyStar = true, OverallRating = 77, CurrentForm = 7.6, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 0.3, DribblesPer90 = 0, FoulsCommittedPer90 = 0.1, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.04, RedCardsPer90 = 0, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.1, Description = "AFCON 2023 Goalkeeper of the Tournament. Shot-stopping excellence.", PlayingCharacteristics = "Reflexes, penalty saves, commanding" },
            new() { Id = "dolly_k", Name = "Keagan Dolly", Age = 31, Club = "Kaizer Chiefs", Position = Position.Forward, ShirtNumber = 10, OverallRating = 73, CurrentForm = 6.8, GoalsPer90 = 0.31, AssistsPer90 = 0.24, ShotsPer90 = 2.1, ShotsOnTargetPer90 = 0.8, KeyPassesPer90 = 1.8, DribblesPer90 = 2.4, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 2.1, YellowCardsPer90 = 0.12, RedCardsPer90 = 0.0, TacklesPer90 = 0.6, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 0.5, Description = "Creative winger who can unlock defences.", PlayingCharacteristics = "Dribbling, creativity, pace, set-pieces" },
        }
    };

    private static Team BuildTunisia() => new()
    {
        Id = "tun", Name = "Tunisia", ShortName = "TUN", FlagEmoji = "🇹🇳",
        Confederation = "CAF", Group = "TBD", FifaRanking = 35,
        PrimaryStyle = PlayingStyle.Defensive, SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.3, GoalsConcededPerGame = 1.1, ShotsPerGame = 12.2, ShotsOnTargetPerGame = 3.8,
        FoulsPerGame = 14.8, YellowCardsPerGame = 2.3, RedCardsPerGame = 0.13,
        Possession = 44.0, PressureIntensity = 60, DefensiveLineHeight = 50,
        CurrentForm = 6.5, RecentResults = "D W W L D", SquadStrength = 70,
        ManagerName = "Jalel Kadri", TacticalSetup = "4-4-1-1",
        Description = "Organized, defensive and difficult to beat. Known for tenacity and physicality.",
        Players = new List<Player>
        {
            new() { Id = "msakni", Name = "Youssef Msakni", Age = 33, Club = "Espérance Tunis", Position = Position.Forward, ShirtNumber = 10, OverallRating = 74, CurrentForm = 6.8, GoalsPer90 = 0.28, AssistsPer90 = 0.31, ShotsPer90 = 2.1, ShotsOnTargetPer90 = 0.8, KeyPassesPer90 = 2.4, DribblesPer90 = 2.8, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 2.4, YellowCardsPer90 = 0.12, RedCardsPer90 = 0.0, TacklesPer90 = 0.6, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 0.4, Description = "Tunisia's creative talisman. Skilled dribbler and creator.", PlayingCharacteristics = "Dribbling, creativity, vision, experience" },
        }
    };

    private static Team BuildCameroon() => new()
    {
        Id = "cmr", Name = "Cameroon", ShortName = "CMR", FlagEmoji = "🇨🇲",
        Confederation = "CAF", Group = "TBD", FifaRanking = 43,
        PrimaryStyle = PlayingStyle.Direct, SecondaryStyle = PlayingStyle.HighPress,
        GoalsPerGame = 1.4, GoalsConcededPerGame = 1.3, ShotsPerGame = 12.8, ShotsOnTargetPerGame = 4.0,
        FoulsPerGame = 14.4, YellowCardsPerGame = 2.2, RedCardsPerGame = 0.12,
        Possession = 45.0, PressureIntensity = 64, DefensiveLineHeight = 52,
        CurrentForm = 6.4, RecentResults = "D W L W W", SquadStrength = 69,
        ManagerName = "Marc Brys", TacticalSetup = "4-3-3",
        Description = "Athletic and physical Indomitable Lions. Mbeumo and Toko Ekambi providing pace up front. Anguissa controlling midfield.",
        Players = new List<Player>
        {
            new() { Id = "onana_a", Name = "Andre Onana", Age = 28, Club = "Manchester United", Position = Position.Goalkeeper, ShirtNumber = 1, IsKeyStar = true, OverallRating = 84, CurrentForm = 7.8, GoalsPer90 = 0, AssistsPer90 = 0, ShotsPer90 = 0, ShotsOnTargetPer90 = 0, KeyPassesPer90 = 0.6, DribblesPer90 = 0, FoulsCommittedPer90 = 0.1, FoulsDrawnPer90 = 0, YellowCardsPer90 = 0.05, RedCardsPer90 = 0, TacklesPer90 = 0, InterceptionsPer90 = 0, AerialDuelsPer90 = 1.3, Description = "Elite shot-stopper with excellent feet. Sweeper-keeper style.", PlayingCharacteristics = "Shot-stopping, distribution, sweeping, commanding" },
            new() { Id = "mbeumo", Name = "Bryan Mbeumo", Age = 25, Club = "Brentford", Position = Position.Forward, ShirtNumber = 11, IsKeyStar = true, OverallRating = 81, CurrentForm = 7.9, GoalsPer90 = 0.52, AssistsPer90 = 0.28, ShotsPer90 = 3.2, ShotsOnTargetPer90 = 1.3, KeyPassesPer90 = 1.8, DribblesPer90 = 2.4, FoulsCommittedPer90 = 0.7, FoulsDrawnPer90 = 2.1, YellowCardsPer90 = 0.12, RedCardsPer90 = 0.0, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 0.6, Description = "Prolific goalscorer who converted to striker. Excellent in front of goal.", PlayingCharacteristics = "Goalscoring, pace, movement, clinical finishing" },
            new() { Id = "anguissa", Name = "Andre-Frank Zambo Anguissa", Age = 29, Club = "Napoli", Position = Position.Midfielder, ShirtNumber = 8, OverallRating = 83, CurrentForm = 8.1, GoalsPer90 = 0.11, AssistsPer90 = 0.14, ShotsPer90 = 1.2, ShotsOnTargetPer90 = 0.4, KeyPassesPer90 = 1.8, DribblesPer90 = 1.8, FoulsCommittedPer90 = 2.4, FoulsDrawnPer90 = 1.1, YellowCardsPer90 = 0.44, RedCardsPer90 = 0.02, TacklesPer90 = 4.4, InterceptionsPer90 = 2.6, AerialDuelsPer90 = 2.8, Description = "Dominant box-to-box midfielder at Napoli. Physical powerhouse.", PlayingCharacteristics = "Ball-winning, physicality, progressive carrying, energy" },
        }
    };

    // ─── AFC (dodatkowe) ──────────────────────────────────────────────────

    private static Team BuildSaudiArabia() => new()
    {
        Id = "ksa", Name = "Saudi Arabia", ShortName = "KSA", FlagEmoji = "🇸🇦",
        Confederation = "AFC", Group = "TBD", FifaRanking = 58,
        PrimaryStyle = PlayingStyle.CounterAttack, SecondaryStyle = PlayingStyle.Direct,
        GoalsPerGame = 1.3, GoalsConcededPerGame = 1.4, ShotsPerGame = 11.8, ShotsOnTargetPerGame = 3.6,
        FoulsPerGame = 14.8, YellowCardsPerGame = 2.3, RedCardsPerGame = 0.13,
        Possession = 43.0, PressureIntensity = 60, DefensiveLineHeight = 48,
        CurrentForm = 6.2, RecentResults = "W D W L D", SquadStrength = 66,
        ManagerName = "Roberto Mancini", TacticalSetup = "4-3-3",
        Description = "Boosted by Saudi Pro League. Al-Dawsari and Al-Shehri the key forwards. Famous for beating Argentina in 2022.",
        Players = new List<Player>
        {
            new() { Id = "aldawsari", Name = "Salem Al-Dawsari", Age = 32, Club = "Al-Hilal", Position = Position.Forward, ShirtNumber = 10, IsCaptain = true, IsKeyStar = true, OverallRating = 76, CurrentForm = 7.0, GoalsPer90 = 0.38, AssistsPer90 = 0.32, ShotsPer90 = 2.4, ShotsOnTargetPer90 = 1.0, KeyPassesPer90 = 2.1, DribblesPer90 = 2.8, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 2.2, YellowCardsPer90 = 0.14, RedCardsPer90 = 0.0, TacklesPer90 = 0.6, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 0.5, Description = "Saudi Arabia's creative forward and captain. Scored famously vs Argentina in 2022.", PlayingCharacteristics = "Dribbling, creativity, finishing, pace, leadership" },
        }
    };

    private static Team BuildIran() => new()
    {
        Id = "irn", Name = "Iran", ShortName = "IRN", FlagEmoji = "🇮🇷",
        Confederation = "AFC", Group = "TBD", FifaRanking = 22,
        PrimaryStyle = PlayingStyle.Defensive, SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.3, GoalsConcededPerGame = 1.1, ShotsPerGame = 12.0, ShotsOnTargetPerGame = 3.8,
        FoulsPerGame = 14.8, YellowCardsPerGame = 2.4, RedCardsPerGame = 0.13,
        Possession = 44.0, PressureIntensity = 58, DefensiveLineHeight = 48,
        CurrentForm = 6.6, RecentResults = "W W D L W", SquadStrength = 72,
        ManagerName = "Amir Ghalenoei", TacticalSetup = "4-3-3 / 4-1-4-1",
        Description = "Organized and disciplined. Taremi the star, strong team defensive structure. Consistent Asian qualifier.",
        Players = new List<Player>
        {
            new() { Id = "taremi", Name = "Mehdi Taremi", Age = 32, Club = "Inter Milan", Position = Position.Forward, ShirtNumber = 9, IsCaptain = true, IsKeyStar = true, OverallRating = 82, CurrentForm = 8.0, GoalsPer90 = 0.62, AssistsPer90 = 0.28, ShotsPer90 = 3.4, ShotsOnTargetPer90 = 1.5, KeyPassesPer90 = 1.4, DribblesPer90 = 1.2, FoulsCommittedPer90 = 1.0, FoulsDrawnPer90 = 2.4, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.01, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 3.8, Description = "World-class striker at Inter Milan. Clinical and strong in the air.", PlayingCharacteristics = "Finishing, aerial threat, hold-up play, link-up" },
        }
    };

    private static Team BuildAustralia() => new()
    {
        Id = "aus", Name = "Australia", ShortName = "AUS", FlagEmoji = "🇦🇺",
        Confederation = "AFC", Group = "TBD", FifaRanking = 24,
        PrimaryStyle = PlayingStyle.HighPress, SecondaryStyle = PlayingStyle.Direct,
        GoalsPerGame = 1.6, GoalsConcededPerGame = 1.1, ShotsPerGame = 13.4, ShotsOnTargetPerGame = 4.3,
        FoulsPerGame = 12.8, YellowCardsPerGame = 1.8, RedCardsPerGame = 0.09,
        Possession = 49.0, PressureIntensity = 70, DefensiveLineHeight = 62,
        CurrentForm = 7.0, RecentResults = "W W D W L", SquadStrength = 74,
        ManagerName = "Tony Popovic", TacticalSetup = "4-3-3",
        Description = "High-energy Socceroos with European-based talent. Hrustic creative, Duke the goalscorer. Reached 2022 R16.",
        Players = new List<Player>
        {
            new() { Id = "leckie", Name = "Mathew Leckie", Age = 33, Club = "Melbourne City", Position = Position.Forward, ShirtNumber = 7, IsCaptain = true, IsKeyStar = true, OverallRating = 76, CurrentForm = 7.2, GoalsPer90 = 0.32, AssistsPer90 = 0.24, ShotsPer90 = 2.2, ShotsOnTargetPer90 = 0.9, KeyPassesPer90 = 1.2, DribblesPer90 = 2.1, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.16, RedCardsPer90 = 0.0, TacklesPer90 = 0.6, InterceptionsPer90 = 0.4, AerialDuelsPer90 = 0.8, Description = "Australia's captain and talisman. Scored famous winner vs Denmark in 2022 WC.", PlayingCharacteristics = "Pace, directness, leadership, pressing, big game moments" },
            new() { Id = "hrustic", Name = "Ajdin Hrustic", Age = 28, Club = "Columbus Crew", Position = Position.Midfielder, ShirtNumber = 10, OverallRating = 74, CurrentForm = 7.0, GoalsPer90 = 0.18, AssistsPer90 = 0.31, ShotsPer90 = 1.6, ShotsOnTargetPer90 = 0.6, KeyPassesPer90 = 2.8, DribblesPer90 = 1.8, FoulsCommittedPer90 = 1.0, FoulsDrawnPer90 = 1.4, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.0, TacklesPer90 = 1.4, InterceptionsPer90 = 1.0, AerialDuelsPer90 = 0.6, Description = "Creative midfield orchestrator. Set-piece specialist.", PlayingCharacteristics = "Creativity, passing, set-pieces, technical quality" },
        }
    };

    private static Team BuildQatar() => new()
    {
        Id = "qat", Name = "Qatar", ShortName = "QAT", FlagEmoji = "🇶🇦",
        Confederation = "AFC", Group = "TBD", FifaRanking = 37,
        PrimaryStyle = PlayingStyle.Possession, SecondaryStyle = PlayingStyle.Defensive,
        GoalsPerGame = 1.2, GoalsConcededPerGame = 1.4, ShotsPerGame = 11.8, ShotsOnTargetPerGame = 3.5,
        FoulsPerGame = 13.4, YellowCardsPerGame = 2.0, RedCardsPerGame = 0.1,
        Possession = 50.0, PressureIntensity = 60, DefensiveLineHeight = 54,
        CurrentForm = 6.0, RecentResults = "W D L W D", SquadStrength = 68,
        ManagerName = "Marquez Lopez", TacticalSetup = "4-3-3",
        Description = "Built for possession football through Aspire Academy. Afif the creative star, Almoez Ali the target man.",
        Players = new List<Player>
        {
            new() { Id = "afif", Name = "Akram Afif", Age = 28, Club = "Al-Sadd", Position = Position.Forward, ShirtNumber = 11, IsKeyStar = true, OverallRating = 77, CurrentForm = 7.2, GoalsPer90 = 0.42, AssistsPer90 = 0.38, ShotsPer90 = 2.4, ShotsOnTargetPer90 = 1.0, KeyPassesPer90 = 2.8, DribblesPer90 = 3.1, FoulsCommittedPer90 = 0.6, FoulsDrawnPer90 = 3.1, YellowCardsPer90 = 0.10, RedCardsPer90 = 0.0, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 0.4, Description = "AFC Player of the Year. Creative winger who draws fouls and creates danger.", PlayingCharacteristics = "Dribbling, creativity, pace, fouls drawn" },
        }
    };

    private static Team BuildIraq() => new()
    {
        Id = "irq", Name = "Iraq", ShortName = "IRQ", FlagEmoji = "🇮🇶",
        Confederation = "AFC", Group = "TBD", FifaRanking = 58,
        PrimaryStyle = PlayingStyle.CounterAttack, SecondaryStyle = PlayingStyle.Defensive,
        GoalsPerGame = 1.2, GoalsConcededPerGame = 1.4, ShotsPerGame = 11.5, ShotsOnTargetPerGame = 3.5,
        FoulsPerGame = 15.1, YellowCardsPerGame = 2.4, RedCardsPerGame = 0.14,
        Possession = 43.0, PressureIntensity = 57, DefensiveLineHeight = 46,
        CurrentForm = 5.9, RecentResults = "W L D W L", SquadStrength = 64,
        ManagerName = "Jesus Casas", TacticalSetup = "4-2-3-1",
        Description = "Disciplined team with passionate support. Physical and direct, hard to break down.",
        Players = new List<Player>
        {
            new() { Id = "mohanad_ali", Name = "Mohanad Ali", Age = 28, Club = "Al-Quwa Al-Jawiya", Position = Position.Forward, ShirtNumber = 10, OverallRating = 70, CurrentForm = 6.5, GoalsPer90 = 0.38, AssistsPer90 = 0.21, ShotsPer90 = 2.2, ShotsOnTargetPer90 = 0.9, KeyPassesPer90 = 1.2, DribblesPer90 = 1.8, FoulsCommittedPer90 = 1.0, FoulsDrawnPer90 = 1.6, YellowCardsPer90 = 0.16, RedCardsPer90 = 0.01, TacklesPer90 = 0.4, InterceptionsPer90 = 0.2, AerialDuelsPer90 = 2.2, Description = "Iraq's main goal threat.", PlayingCharacteristics = "Finishing, movement, pressing" },
        }
    };

    private static Team BuildJordan() => new()
    {
        Id = "jor", Name = "Jordan", ShortName = "JOR", FlagEmoji = "🇯🇴",
        Confederation = "AFC", Group = "TBD", FifaRanking = 68,
        PrimaryStyle = PlayingStyle.Defensive, SecondaryStyle = PlayingStyle.CounterAttack,
        GoalsPerGame = 1.1, GoalsConcededPerGame = 1.3, ShotsPerGame = 11.0, ShotsOnTargetPerGame = 3.3,
        FoulsPerGame = 14.8, YellowCardsPerGame = 2.3, RedCardsPerGame = 0.13,
        Possession = 42.0, PressureIntensity = 56, DefensiveLineHeight = 44,
        CurrentForm = 5.8, RecentResults = "D W L W D", SquadStrength = 62,
        ManagerName = "Hashim Mustafa", TacticalSetup = "4-4-2",
        Description = "Organized and disciplined. Surprised everyone reaching 2023 Asian Cup Final. Physical and determined.",
        Players = new List<Player>
        {
            new() { Id = "tamari", Name = "Musa Al-Tamari", Age = 28, Club = "Montpellier", Position = Position.Forward, ShirtNumber = 7, IsKeyStar = true, OverallRating = 72, CurrentForm = 6.8, GoalsPer90 = 0.34, AssistsPer90 = 0.28, ShotsPer90 = 2.1, ShotsOnTargetPer90 = 0.8, KeyPassesPer90 = 1.8, DribblesPer90 = 2.4, FoulsCommittedPer90 = 0.8, FoulsDrawnPer90 = 2.2, YellowCardsPer90 = 0.12, RedCardsPer90 = 0.0, TacklesPer90 = 0.4, InterceptionsPer90 = 0.3, AerialDuelsPer90 = 0.5, Description = "Jordan's most exciting player. Fast and direct winger.", PlayingCharacteristics = "Pace, dribbling, directness, creativity" },
        }
    };

    // ─── OFC ──────────────────────────────────────────────────────────────

    private static Team BuildNewZealand() => new()
    {
        Id = "nzl", Name = "New Zealand", ShortName = "NZL", FlagEmoji = "🇳🇿",
        Confederation = "OFC", Group = "TBD", FifaRanking = 106,
        PrimaryStyle = PlayingStyle.Direct, SecondaryStyle = PlayingStyle.Defensive,
        GoalsPerGame = 0.9, GoalsConcededPerGame = 1.8, ShotsPerGame = 9.8, ShotsOnTargetPerGame = 2.8,
        FoulsPerGame = 13.2, YellowCardsPerGame = 1.8, RedCardsPerGame = 0.09,
        Possession = 41.0, PressureIntensity = 54, DefensiveLineHeight = 42,
        CurrentForm = 5.0, RecentResults = "L D W L D", SquadStrength = 55,
        ManagerName = "Darren Bazeley", TacticalSetup = "4-5-1",
        Description = "All Whites representing OFC. Physical and determined. Chris Wood the experienced target man.",
        Players = new List<Player>
        {
            new() { Id = "wood_c", Name = "Chris Wood", Age = 33, Club = "Nottingham Forest", Position = Position.Forward, ShirtNumber = 9, IsCaptain = true, IsKeyStar = true, OverallRating = 76, CurrentForm = 6.8, GoalsPer90 = 0.48, AssistsPer90 = 0.14, ShotsPer90 = 2.8, ShotsOnTargetPer90 = 1.1, KeyPassesPer90 = 0.5, DribblesPer90 = 0.4, FoulsCommittedPer90 = 1.1, FoulsDrawnPer90 = 1.8, YellowCardsPer90 = 0.18, RedCardsPer90 = 0.01, TacklesPer90 = 0.4, InterceptionsPer90 = 0.2, AerialDuelsPer90 = 4.2, Description = "New Zealand's experienced Premier League striker. Aerial specialist.", PlayingCharacteristics = "Aerial threat, hold-up play, physicality, experience" },
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
