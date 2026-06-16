using MundialPrediction.Core.Models;

namespace MundialPrediction.Core.Interfaces;

public interface ITeamRepository
{
    Task<IEnumerable<Team>> GetAllTeamsAsync();
    Task<Team?> GetTeamByIdAsync(string id);
    Task<IEnumerable<Team>> GetTeamsByGroupAsync(string group);
    Task<IEnumerable<Team>> SearchTeamsAsync(string query);
}
