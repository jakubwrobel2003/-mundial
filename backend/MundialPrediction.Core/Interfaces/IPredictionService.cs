using MundialPrediction.Core.Models;

namespace MundialPrediction.Core.Interfaces;

public interface IPredictionService
{
    Task<MatchPrediction> PredictMatchAsync(string homeTeamId, string awayTeamId);
    Task<MatchPrediction> PredictCustomMatchAsync(string homeTeamId, string awayTeamId, string stage);
}
