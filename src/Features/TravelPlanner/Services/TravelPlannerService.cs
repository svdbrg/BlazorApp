using BlazorApp.Features.TravelPlanner.Services.Abstractions;
using BlazorApp.Features.TravelPlanner.Models;

namespace BlazorApp.Features.TravelPlanner.Services;

public class TravelPlannerService : ITravelPlannerService
{
    private readonly ITravelPlannerDataClient _dataClient;
    private readonly ITravelPlannerRepository _repository;

    public TravelPlannerService(ITravelPlannerDataClient dataClient, ITravelPlannerRepository repository)
    {
        _dataClient = dataClient ?? throw new NullReferenceException(nameof(dataClient));
        _repository = repository ?? throw new NullReferenceException(nameof(repository));
    }

    public async IAsyncEnumerable<NearbyStopLocation> GetAllNearbyStops()
    {
        var pois = await _repository.GetPlacesOfInterestAsync();
        var nearbyStopLocations = new List<NearbyStopLocation>();

        foreach (var poi in pois)
        {
            yield return new NearbyStopLocation
            {
                Name = poi.Name,
                NearbyStops = await _dataClient.GetNearbyStops(poi.Longitude, poi.Latitude).ToListAsync()
            };
        }

        yield return new NearbyStopLocation
        {
            Name = "Other",
            NearbyStops = await _repository.GetOtherNearbyStopsAsync()
        };
    }
}