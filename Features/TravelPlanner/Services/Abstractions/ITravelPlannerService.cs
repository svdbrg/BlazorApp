using BlazorApp.Features.TravelPlanner.Models;

namespace BlazorApp.Features.TravelPlanner.Services.Abstractions;

public interface ITravelPlannerService
{
    IAsyncEnumerable<NearbyStopLocation> GetAllNearbyStops();
}