using BlazorApp.Features.TravelPlanner.Models;

namespace BlazorApp.Features.TravelPlanner.Services.Abstractions;

public interface ITravelPlanner
{
    Task<IEnumerable<Trip>> GetTravelPlan(string selectedStationOfOrigin);
    Task<NearbyStopsRoot> GetNearbyStops();
}