using Microsoft.AspNetCore.Mvc;
using MundialPrediction.API.Services;

namespace MundialPrediction.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly ScheduleService _schedule;
    private readonly ILogger<MatchesController> _logger;

    public MatchesController(ScheduleService schedule, ILogger<MatchesController> logger)
    {
        _schedule = schedule;
        _logger = logger;
    }

    [HttpGet("wc2026")]
    public async Task<IActionResult> GetSchedule()
    {
        try
        {
            var result = await _schedule.GetScheduleAsync();
            if (result == null)
                return StatusCode(503, new { error = "football-data.org niedostępne lub brak klucza API" });
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Błąd pobierania terminarza WC 2026");
            return StatusCode(500, new { error = "Błąd wewnętrzny" });
        }
    }
}
