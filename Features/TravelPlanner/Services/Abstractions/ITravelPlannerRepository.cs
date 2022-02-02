using BlazorApp.Features.TravelPlanner.Models;

namespace BlazorApp.Features.TravelPlanner.Services.Abstractions;

public interface ITravelPlannerRepository
{
    Task<List<PlaceOfInterest>> GetPlacesOfInterestAsync();
    Task<List<NearbyStop>> GetOtherNearbyStopsAsync();
}