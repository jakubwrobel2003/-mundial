using MundialPrediction.Core.Interfaces;
using MundialPrediction.Core.Models;
using MundialPrediction.Infrastructure.Data;

namespace MundialPrediction.Infrastructure.Services;

public class TeamRepository : ITeamRepository
{
    private static readonly List<Team> Teams = WorldCupDataProvider.GetTeams();

    public Task<IEnumerable<Team>> GetAllTeamsAsync() =>
        Task.FromResult<IEnumerable<Team>>(Teams);

    public Task<Team?> GetTeamByIdAsync(string id) =>
        Task.FromResult(Teams.FirstOrDefault(t => t.Id == id));

    public Task<IEnumerable<Team>> GetTeamsByGroupAsync(string group) =>
        Task.FromResult<IEnumerable<Team>>(Teams.Where(t => t.Group == group));

    public Task<IEnumerable<Team>> SearchTeamsAsync(string query) =>
        Task.FromResult<IEnumerable<Team>>(
            Teams.Where(t => t.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                          || t.ShortName.Contains(query, StringComparison.OrdinalIgnoreCase)));
}
