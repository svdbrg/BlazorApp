using BlazorApp.Features.TravelPlanner.Models;

namespace BlazorApp.Features.TravelPlanner.Services.Abstractions;

public interface ITravelPlannerDataClient
{
    Task<IEnumerable<Trip>> GetTravelPlan(string selectedStationOfOrigin, string workStation);
    IAsyncEnumerable<NearbyStop> GetNearbyStops(double lon, double lat);
}