using MundialPrediction.Core.Interfaces;
using MundialPrediction.Core.Models;

namespace MundialPrediction.Infrastructure.Services;

public class PredictionService : IPredictionService
{
    private readonly ITeamRepository _teams;
    private readonly PredictionEngine _engine;

    public PredictionService(ITeamRepository teams, PredictionEngine engine)
    {
        _teams = teams;
        _engine = engine;
    }

    public async Task<MatchPrediction> PredictMatchAsync(string homeTeamId, string awayTeamId)
    {
        var home = await _teams.GetTeamByIdAsync(homeTeamId)
            ?? throw new ArgumentException($"Team not found: {homeTeamId}");
        var away = await _teams.GetTeamByIdAsync(awayTeamId)
            ?? throw new ArgumentException($"Team not found: {awayTeamId}");
        return _engine.Analyze(home, away);
    }

    public async Task<MatchPrediction> PredictCustomMatchAsync(string homeTeamId, string awayTeamId, string stage)
    {
        var home = await _teams.GetTeamByIdAsync(homeTeamId)
            ?? throw new ArgumentException($"Team not found: {homeTeamId}");
        var away = await _teams.GetTeamByIdAsync(awayTeamId)
            ?? throw new ArgumentException($"Team not found: {awayTeamId}");
        return _engine.Analyze(home, away, stage);
    }
}
