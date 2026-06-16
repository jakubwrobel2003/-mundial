using Microsoft.AspNetCore.Mvc;
using MundialPrediction.Core.Interfaces;

namespace MundialPrediction.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PredictionsController : ControllerBase
{
    private readonly IPredictionService _predictions;

    public PredictionsController(IPredictionService predictions) => _predictions = predictions;

    [HttpGet("analyze")]
    public async Task<IActionResult> Analyze(
        [FromQuery] string homeTeamId,
        [FromQuery] string awayTeamId,
        [FromQuery] string stage = "Group")
    {
        if (string.IsNullOrWhiteSpace(homeTeamId) || string.IsNullOrWhiteSpace(awayTeamId))
            return BadRequest("homeTeamId and awayTeamId are required");

        try
        {
            var prediction = await _predictions.PredictCustomMatchAsync(homeTeamId, awayTeamId, stage);
            return Ok(prediction);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("analyze")]
    public async Task<IActionResult> AnalyzePost([FromBody] AnalyzeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.HomeTeamId) || string.IsNullOrWhiteSpace(request.AwayTeamId))
            return BadRequest("homeTeamId and awayTeamId are required");

        try
        {
            var prediction = await _predictions.PredictCustomMatchAsync(
                request.HomeTeamId, request.AwayTeamId, request.Stage ?? "Group");
            return Ok(prediction);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

public record AnalyzeRequest(string HomeTeamId, string AwayTeamId, string? Stage);
