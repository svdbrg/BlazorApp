using AutoMapper;
using BlazorApp.Features.TravelPlanner.Models;
using BlazorApp.Features.TravelPlanner.Services.Abstractions;
using Google.Cloud.Firestore;

namespace BlazorApp.Features.TravelPlanner.Services;

public class FirestoreTravelPlannerRepository : ITravelPlannerRepository
{
    private readonly IMapper _mapper;

    public FirestoreTravelPlannerRepository(IMapper mapper)
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
            .OrderBy(p => p.Name)
            .ToList();
    }

    public async Task<List<NearbyStop>> GetOtherNearbyStopsAsync()
    {
        var db = FirestoreDb.Create("mortgager");
        var snapshot = await db.Collection("nearbystops").GetSnapshotAsync();

        return snapshot.Documents
            .Select(d => d.ConvertTo<NearbyStopDto>())
            .Select(_mapper.Map<NearbyStop>)
            .OrderBy(s => s.Name)
            .ToList();
    }

    public async Task<bool> SaveNewNearbyStopAsync(NearbyStop stop)
    {
        var newStopDto = _mapper.Map<NearbyStopDto>(stop);
        var db = FirestoreDb.Create("mortgager");
        var docref = await db.Collection("nearbystops").AddAsync(newStopDto);

        return docref?.Id != null;
    }

    public async Task<bool> DeleteStopAsync(NearbyStop stop)
    {
        var db = FirestoreDb.Create("mortgager");
        var doc = db.Collection("nearbystops").Document(stop.Id);
        var result = await doc.DeleteAsync();

        return result?.UpdateTime != null;
    }
}