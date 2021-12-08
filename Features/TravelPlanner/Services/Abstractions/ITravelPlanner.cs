using BlazorApp.Features.TravelPlanner.Models;

namespace BlazorApp.Features.TravelPlanner.Services.Abstractions;

public interface ITravelPlanner
{
    Task<TravelPlannerRoot> GetTravelPlan(string selectedStationOfOrigin);
    Task<NearbyStopsRoot> GetNearbyStops();
}