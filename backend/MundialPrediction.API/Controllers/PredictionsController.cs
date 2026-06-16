using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MundialPrediction.Core.Interfaces;
using MundialPrediction.Core.Models;

namespace MundialPrediction.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PredictionsController : ControllerBase
{
    private readonly IPredictionService _predictions;
    private readonly IMemoryCache _cache;

    public PredictionsController(IPredictionService predictions, IMemoryCache cache)
    {
        _predictions = predictions;
        _cache = cache;
    }

    [HttpGet("analyze")]
    public async Task<IActionResult> Analyze(
        [FromQuery] string homeTeamId,
        [FromQuery] string awayTeamId,
        [FromQuery] string stage = "Group")
    {
        if (string.IsNullOrWhiteSpace(homeTeamId) || string.IsNullOrWhiteSpace(awayTeamId))
            return BadRequest("homeTeamId and awayTeamId are required");

        var key = $"poisson:{homeTeamId}:{awayTeamId}:{stage}";
        if (_cache.TryGetValue(key, out MatchPrediction? cached))
            return Ok(cached);

        try
        {
            var prediction = await _predictions.PredictCustomMatchAsync(homeTeamId, awayTeamId, stage);
            // Wynik Poisson jest deterministyczny – cache na 24h (zmienia się tylko przy deploy)
            _cache.Set(key, prediction, TimeSpan.FromHours(24));
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

        var key = $"poisson:{request.HomeTeamId}:{request.AwayTeamId}:{request.Stage ?? "Group"}";
        if (_cache.TryGetValue(key, out MatchPrediction? cached))
            return Ok(cached);

        try
        {
            var prediction = await _predictions.PredictCustomMatchAsync(
                request.HomeTeamId, request.AwayTeamId, request.Stage ?? "Group");
            _cache.Set(key, prediction, TimeSpan.FromHours(24));
            return Ok(prediction);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

public record AnalyzeRequest(string HomeTeamId, string AwayTeamId, string? Stage);
