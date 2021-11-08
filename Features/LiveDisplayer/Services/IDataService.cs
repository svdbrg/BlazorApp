using BlazorApp.Features.LiveDisplayer.Data;
namespace BlazorApp.Features.LiveDisplayer.Services;

public interface IDataService
{
    Task<TableStandings> GetTable();
    Task<IEnumerable<Fixture>> GetFixtures();
}