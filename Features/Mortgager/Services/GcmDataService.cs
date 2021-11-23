using AutoMapper;
using BlazorApp.Features.Mortgager.Data;
using Google.Cloud.Firestore;

namespace BlazorApp.Features.Mortgager.Services;

public class GcmDataService : IDataService
{
    private readonly IMapper _mapper;
    private readonly ILogger<GcmDataService> _logger;

    public GcmDataService(IMapper mapper, ILogger<GcmDataService> logger)
    {
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
    }

    private string documentSuffix = Environment.GetEnvironmentVariable("Environment") ?? "test";

    public async Task<MortgageItem?> GetSavedDataAsync()
    {
        var db = FirestoreDb.Create("mortgager");

        var docRef = db.Collection("mortgages").Document($"mortgage-{documentSuffix}");

        var snapshot = await docRef.GetSnapshotAsync();

        if (snapshot != null)
        {
            _logger.LogInformation("Call to Firestore succeeded");

            var mortgageDto = snapshot.ConvertTo<MortgageItemDto>();

            return _mapper.Map<MortgageItem>(mortgageDto);
        }

        _logger.LogWarning("Call to Firestore failed when getting data");

        return null;
    }

    public async Task<string> SaveDataAsync(MortgageItem item)
    {
        var db = FirestoreDb.Create("mortgager");

        var docRef = db.Collection("mortgages").Document($"mortgage-{documentSuffix}");

        var mortgageItemDto = _mapper.Map<MortgageItemDto>(item);

        await docRef.SetAsync(mortgageItemDto);

        return docRef.Path;
    }
}