using BlazorApp.Features.LiveDisplayer.Data;
namespace BlazorApp.Features.LiveDisplayer.Services;

public interface IDataService
{
    Task<IEnumerable<Team>> GetTableAsync();
    IAsyncEnumerable<Day> GetDaysAndFixturesAsync();
}