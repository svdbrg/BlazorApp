using AutoMapper;
using BlazorApp.Features.Mortgager.Data;
using BlazorApp.Features.Mortgager.Services.Abstractions;
using Google.Cloud.Firestore;

namespace BlazorApp.Features.Mortgager.Services;

public class GcmDataService : IDataService
{
    private readonly IMapper _mapper;
    private readonly ILogger<GcmDataService> _logger;
    private readonly ILocalStorage _localStorage;

    public GcmDataService(IMapper mapper, ILogger<GcmDataService> logger, ILocalStorage localStorage)
    {
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
        _localStorage = localStorage ?? throw new NullReferenceException(nameof(localStorage));
    }

    public async Task<MortgageItem?> GetSavedDataAsync()
    {
        var documentSuffix = await _localStorage.GetStringAsync("documentSuffix");

        if (string.IsNullOrWhiteSpace(documentSuffix))
        {
            _logger.LogInformation($"Did not find: {documentSuffix}");

            return null;
        }

        _logger.LogInformation($"Found store suffix: {documentSuffix}");

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

    public async Task<bool> SaveDataAsync(MortgageItem item)
    {
        var documentSuffix = await _localStorage.GetStringAsync("documentSuffix");

        if (string.IsNullOrWhiteSpace(documentSuffix))
        {
            return false;
        }

        _logger.LogInformation($"Found store suffix: {documentSuffix}");

        var db = FirestoreDb.Create("mortgager");

        var docRef = db.Collection("mortgages").Document($"mortgage-{documentSuffix}");

        var mortgageItemDto = _mapper.Map<MortgageItemDto>(item);

        var result = await docRef.SetAsync(mortgageItemDto);

        return true;
    }

    public async Task<List<string>> GetAllMortgageDocuments()
    {
        var db = FirestoreDb.Create("mortgager");
        var collection = db.Collection("mortgages");
        return await collection.ListDocumentsAsync().Select(d => d.Id.Replace("mortgage-", "")).ToListAsync();
    }
}