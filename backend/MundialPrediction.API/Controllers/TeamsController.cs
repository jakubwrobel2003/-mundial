using Microsoft.AspNetCore.Mvc;
using MundialPrediction.Core.Interfaces;

namespace MundialPrediction.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamRepository _teams;

    public TeamsController(ITeamRepository teams) => _teams = teams;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? group)
    {
        var teams = group != null
            ? await _teams.GetTeamsByGroupAsync(group)
            : await _teams.GetAllTeamsAsync();

        var result = teams.Select(t => new
        {
            t.Id, t.Name, t.ShortName, t.FlagEmoji, t.Confederation,
            t.Group, t.FifaRanking, t.SquadStrength, t.CurrentForm,
            t.GoalsPerGame, t.GoalsConcededPerGame, t.FoulsPerGame,
            t.YellowCardsPerGame, t.Possession, t.PressureIntensity,
            PrimaryStyle = t.PrimaryStyle.ToString(),
            t.ManagerName, t.TacticalSetup, t.Description,
            t.RecentResults, t.ShotsPerGame
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var team = await _teams.GetTeamByIdAsync(id);
        if (team == null) return NotFound();

        return Ok(new
        {
            team.Id, team.Name, team.ShortName, team.FlagEmoji, team.Confederation,
            team.Group, team.FifaRanking, team.SquadStrength, team.CurrentForm,
            team.GoalsPerGame, team.GoalsConcededPerGame, team.FoulsPerGame,
            team.YellowCardsPerGame, team.RedCardsPerGame, team.Possession,
            team.PressureIntensity, team.DefensiveLineHeight, team.ShotsPerGame,
            team.ShotsOnTargetPerGame,
            PrimaryStyle = team.PrimaryStyle.ToString(),
            SecondaryStyle = team.SecondaryStyle.ToString(),
            team.ManagerName, team.TacticalSetup, team.Description, team.RecentResults,
            Players = team.Players.Select(p => new
            {
                p.Id, p.Name, p.Age, p.Club, Position = p.Position.ToString(),
                p.ShirtNumber, p.IsCaptain, p.IsKeyStar, p.OverallRating, p.CurrentForm,
                p.GoalsPer90, p.AssistsPer90, p.ShotsPer90, p.ShotsOnTargetPer90,
                p.KeyPassesPer90, p.DribblesPer90, p.FoulsCommittedPer90, p.FoulsDrawnPer90,
                p.YellowCardsPer90, p.RedCardsPer90, p.TacklesPer90, p.InterceptionsPer90,
                p.AerialDuelsPer90, p.Description, p.PlayingCharacteristics
            })
        });
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q)) return BadRequest("Query required");
        var teams = await _teams.SearchTeamsAsync(q);
        return Ok(teams.Select(t => new { t.Id, t.Name, t.ShortName, t.FlagEmoji, t.FifaRanking }));
    }
}
