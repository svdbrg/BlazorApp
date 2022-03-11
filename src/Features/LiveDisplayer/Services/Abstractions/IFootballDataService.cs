using BlazorApp.Features.LiveDisplayer.Data;

namespace BlazorApp.Features.LiveDisplayer.Services.Abstractions;

public interface IFootballDataService
{
    Task<IEnumerable<Team>> GetTableAsync();
    IAsyncEnumerable<Day> GetDaysAndFixturesAsync();
    IEnumerable<Team> EnrichTableWithStatus(IEnumerable<Team> teams, IEnumerable<Day> days);
    Task<IEnumerable<Fixture>> GetUpcomingFixtureForTeam(int teamId);
    Task<IEnumerable<Fixture>> GetResultsForTeam(int teamId);
}