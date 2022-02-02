using BlazorApp.Features.TravelPlanner.Services.Abstractions;

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
    public async Task GetAllNearbyStops() {
        var pois = _repository.GetPlacesOfInterestAsync();
    }
}