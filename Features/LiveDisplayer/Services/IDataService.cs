using BlazorApp.Features.LiveDisplayer.Data;
namespace BlazorApp.Features.LiveDisplayer.Services;

public interface IDataService
{
    Task<TableStandings> GetTableAsync();
    IAsyncEnumerable<Day> GetDaysAndFixturesAsync();
}