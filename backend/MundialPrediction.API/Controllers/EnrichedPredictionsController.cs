using Microsoft.AspNetCore.Mvc;
using MundialPrediction.Infrastructure.Services;

namespace MundialPrediction.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrichedPredictionsController : ControllerBase
{
    private readonly EnrichedPredictionService _service;
    private readonly ILogger<EnrichedPredictionsController> _logger;

    public EnrichedPredictionsController(
        EnrichedPredictionService service,
        ILogger<EnrichedPredictionsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Pełna analiza meczu: Poisson + football-data.org + Tavily + RAG + Claude
    /// </summary>
    [HttpGet("{homeTeamId}/{awayTeamId}")]
    public async Task<IActionResult> Analyze(
        string homeTeamId,
        string awayTeamId,
        [FromQuery] string stage = "Group",
        [FromQuery] bool useWeb = true,
        [FromQuery] bool useFootballData = true)
    {
        try
        {
            var result = await _service.AnalyzeAsync(homeTeamId, awayTeamId, stage, useWeb, useFootballData);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Błąd analizy {Home} vs {Away}", homeTeamId, awayTeamId);
            return StatusCode(500, new { error = "Błąd wewnętrzny serwera" });
        }
    }

    /// <summary>
    /// Ostatnie N analiz (wszystkich par)
    /// </summary>
    [HttpGet("history")]
    public async Task<IActionResult> GetAllHistory([FromQuery] int limit = 20)
    {
        var history = await _service.GetAllHistoryAsync(limit);
        return Ok(history);
    }

    /// <summary>
    /// Historia predykcji dla danej pary drużyn
    /// </summary>
    [HttpGet("{homeTeamId}/{awayTeamId}/history")]
    public async Task<IActionResult> GetHistory(string homeTeamId, string awayTeamId)
    {
        var history = await _service.GetHistoryAsync(homeTeamId, awayTeamId);
        return Ok(history);
    }

    /// <summary>
    /// Dodaj dokument do bazy wiedzy RAG
    /// </summary>
    [HttpPost("knowledge")]
    public async Task<IActionResult> AddKnowledge([FromBody] AddKnowledgeRequest req)
    {
        await _service.AddKnowledgeAsync(req.EntityId, req.DocType, req.Title, req.Content, req.SourceUrl);
        return Ok(new { message = "Dokument dodany do RAG" });
    }
}

public class AddKnowledgeRequest
{
    public string EntityId { get; set; } = string.Empty;
    public string DocType { get; set; } = "team_news";
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? SourceUrl { get; set; }
}
