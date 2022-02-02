using AutoMapper;
using BlazorApp.Features.TravelPlanner.Models;
using BlazorApp.Features.TravelPlanner.Services.Abstractions;
using Google.Cloud.Firestore;

namespace BlazorApp.Features.TravelPlanner.Services;

public class FirebaseTravelPlannerRepository : ITravelPlannerRepository
{
    private readonly IMapper _mapper;

    public FirebaseTravelPlannerRepository(IMapper mapper)
    {
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
    }

    public async Task<List<PlaceOfInterest>> GetPlacesOfInterestAsync()
    {
        var db = FirestoreDb.Create("mortgager");
        var snapshot = await db.Collection("placesofinterest").GetSnapshotAsync();

        return snapshot.Documents
            .Select(d => d.ConvertTo<PlaceOfInterestDto>())
            .Select(_mapper.Map<PlaceOfInterest>)
            .ToList();
    }

    public async Task<List<NearbyStop>> GetOtherNearbyStopsAsync()
    {
        var db = FirestoreDb.Create("mortgager");
        var snapshot = await db.Collection("nearbystops").GetSnapshotAsync();

        return snapshot.Documents
            .Select(d => d.ConvertTo<NearbyStopDto>())
            .Select(_mapper.Map<NearbyStop>)
            .ToList();
    }
}