namespace MundialPrediction.Core.Models;

public class Match
{
    public string Id { get; set; } = string.Empty;
    public string HomeTeamId { get; set; } = string.Empty;
    public string AwayTeamId { get; set; } = string.Empty;
    public string Stage { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public DateTime MatchDate { get; set; }
    public string KickoffTimeEt { get; set; } = string.Empty;
    public string Venue { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
    public bool IsPlayed => HomeScore.HasValue && AwayScore.HasValue;
    public string MatchStatus => IsPlayed ? "FT" : MatchDate.Date == DateTime.UtcNow.Date ? "Today" : MatchDate < DateTime.UtcNow ? "Upcoming" : "Upcoming";
}
