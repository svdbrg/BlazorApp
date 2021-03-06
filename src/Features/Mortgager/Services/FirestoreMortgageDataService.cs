using AutoMapper;
using BlazorApp.Features.Mortgager.Data;
using BlazorApp.Features.Mortgager.Services.Abstractions;
using BlazorApp.Features.Shared.Models;
using BlazorApp.Features.Shared.Services;
using BlazorApp.Features.Shared.Services.Abstractions;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;

namespace BlazorApp.Features.Mortgager.Services;

public class FirestoreMortgageDataService : IMortgageDataService
{
    private readonly ILocalStorage _localStorage;
    private readonly ILogger<FirestoreMortgageDataService> _logger;
    private readonly IMapper _mapper;
    private readonly Keys _keys;

    public FirestoreMortgageDataService(ILocalStorage localStorage, ILogger<FirestoreMortgageDataService> logger, IMapper mapper, IOptions<Keys> keys)
    {
        _localStorage = localStorage ?? throw new NullReferenceException(nameof(localStorage));
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        _keys = keys?.Value ?? throw new NullReferenceException(nameof(keys));
    }

    public async Task<MortgageItem?> GetSavedDataAsync()
    {
        var encryptedDocumentSuffix = await _localStorage.GetStringAsync("documentSuffix");

        if (encryptedDocumentSuffix == null)
        {
            _logger.LogInformation($"Did not find documentSuffix in local storage");
            throw new FileNotFoundException("Did not find documentSuffix in local storage");
        }

        var documentSuffix = Encryption.DecryptString(encryptedDocumentSuffix, _keys.EncryptionKeys.DocumentSuffixEncryptionKey);

        if (string.IsNullOrWhiteSpace(documentSuffix))
        {
            _logger.LogInformation($"Did not find documentSuffix in local storage");
            throw new FileNotFoundException("Did not find documentSuffix in local storage");
        }

        _logger.LogInformation($"Found store suffix: {documentSuffix}");

        var db = FirestoreDb.Create("mortgager");

        var docRef = db.Collection("mortgages").Document($"mortgage-{documentSuffix}");

        var snapshot = await docRef.GetSnapshotAsync();

        if (snapshot != null)
        {
            _logger.LogInformation("Call to Firestore succeeded");

            var mortgageDto = snapshot.ConvertTo<MortgageItemDto>();
            var mortgageItem = _mapper.Map<MortgageItem>(mortgageDto);
            mortgageItem.Name = documentSuffix;

            return mortgageItem;
        }

        _logger.LogWarning("Call to Firestore failed when getting data");

        throw new FileNotFoundException("Call to Firestore failed when getting data");
    }

    public async Task<bool> SaveDataAsync(MortgageItem item)
    {
        var encryptedDocumentSuffix = await _localStorage.GetStringAsync("documentSuffix");

        if (string.IsNullOrWhiteSpace(encryptedDocumentSuffix))
        {
            _logger.LogInformation($"Did not find documentSuffix in local storage");

            return false;
        }

        var documentSuffix = Encryption.DecryptString(encryptedDocumentSuffix, _keys.EncryptionKeys.DocumentSuffixEncryptionKey);

        if (string.IsNullOrWhiteSpace(documentSuffix))
        {
            _logger.LogInformation($"Did not find documentSuffix in local storage");

            return false;
        }

        _logger.LogInformation($"Found stored suffix: {documentSuffix}");

        var db = FirestoreDb.Create("mortgager");

        var docRef = db.Collection("mortgages").Document($"mortgage-{documentSuffix}");

        var mortgageItemDto = _mapper.Map<MortgageItemDto>(item);

        var result = await docRef.SetAsync(mortgageItemDto);

        return result?.UpdateTime != null;
    }

    public async Task<List<string>> GetAllMortgageDocuments()
    {
        var db = FirestoreDb.Create("mortgager");
        var collection = db.Collection("mortgages");

        return await collection.ListDocumentsAsync().Select(d => d.Id.Replace("mortgage-", "")).ToListAsync();
    }

    public async Task<bool> DeleteMortgageAsync(string suffix)
    {
        var db = FirestoreDb.Create("mortgager");
        var document = db.Collection("mortgages").Document($"mortgage-{suffix}");
        var result = await document.DeleteAsync();

        return result?.UpdateTime != null;
    }
}