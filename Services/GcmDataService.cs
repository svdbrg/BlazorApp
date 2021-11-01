using AutoMapper;
using BlazorApp.Data;
using Google.Cloud.Firestore;

namespace BlazorApp.Services;

public class GcmDataService : IDataService
{
    private readonly IMapper _mapper;
    public GcmDataService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<MortgageItem?> GetSavedData()
    {
        var db = FirestoreDb.Create("mortgager");

        var collection = db.Collection("mortgages");
        var snapshot = await collection.GetSnapshotAsync();

        if (snapshot != null)
        {
            var mortgageDto = snapshot.Documents.First().ConvertTo<MortgageItemDto>();

            return _mapper.Map<MortgageItem>(mortgageDto);
        }

        return null;
    }

    public async Task<string> SaveData(MortgageItem item)
    {
        var db = FirestoreDb.Create("mortgager");

        var docRef = db.Collection("mortgages").Document("mortgage");

        var mortgageItemDto = _mapper.Map<MortgageItemDto>(item);

        await docRef.SetAsync(mortgageItemDto);

        return docRef.Path;
    }
}