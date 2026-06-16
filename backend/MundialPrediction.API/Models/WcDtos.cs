namespace MundialPrediction.API.Models;

public class WcMatchDto
{
    public int Matchday     { get; set; }
    public string Stage     { get; set; } = "";
    public string FdStage   { get; set; } = "";
    public string? Group    { get; set; }
    public string? UtcDate  { get; set; }
    public string Status    { get; set; } = "TIMED";
    public WcTeamRef HomeTeam { get; set; } = new();
    public WcTeamRef AwayTeam { get; set; } = new();
    public int? ScoreHome   { get; set; }
    public int? ScoreAway   { get; set; }
    public string? Venue    { get; set; }
}

public class WcTeamRef
{
    public string? Id       { get; set; }
    public string FdName    { get; set; } = "";
    public string Name      { get; set; } = "";
    public string FlagEmoji { get; set; } = "🏳️";
    public bool Known       { get; set; }
}
