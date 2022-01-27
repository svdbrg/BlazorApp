using AutoMapper;
using BlazorApp.Features.Mortgager.Data;
using BlazorApp.Features.Shared.Services.Abstractions;
using Google.Cloud.Firestore;
using BlazorApp.Features.Shared.Models;

namespace BlazorApp.Features.Shared.Services;

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
        var encryptedDocumentSuffix = await _localStorage.GetStringAsync("documentSuffix");
        var documentSuffix = Encryption.DecryptString(encryptedDocumentSuffix, Encryption.DocumentSuffixEncryptionKey);

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

            return _mapper.Map<MortgageItem>(mortgageDto);
        }

        _logger.LogWarning("Call to Firestore failed when getting data");

        throw new FileNotFoundException("Call to Firestore failed when getting data");
    }

    public async Task<bool> SaveDataAsync(MortgageItem item)
    {
        var encryptedDocumentSuffix = await _localStorage.GetStringAsync("documentSuffix");
        var documentSuffix = Encryption.DecryptString(encryptedDocumentSuffix, Encryption.DocumentSuffixEncryptionKey);

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

        return true;
    }

    public async Task<List<string>> GetAllMortgageDocuments()
    {
        var db = FirestoreDb.Create("mortgager");
        var collection = db.Collection("mortgages");
        return await collection.ListDocumentsAsync().Select(d => d.Id.Replace("mortgage-", "")).ToListAsync();
    }

    public async Task DeleteMortgageAsync(string suffix)
    {
        var db = FirestoreDb.Create("mortgager");
        var document = db.Collection("mortgages").Document($"mortgage-{suffix}");
        await document.DeleteAsync();
    }

    public async Task<Authentication> Authenticate(string password)
    {
        var token = Encryption.EncryptString($"{password}-{Encryption.Salt}", Encryption.AuthorizationEncryptionKey);

        var db = FirestoreDb.Create("mortgager");
        var collection = db.Collection("passwords");

        var query = collection.WhereEqualTo("EncryptedPassword", token);
        var querySnapshot = await query.GetSnapshotAsync();

        if (querySnapshot != null && querySnapshot.Any() && querySnapshot.Count() == 1)
        {
            _logger.LogInformation("Call to Firestore succeeded");

            var authDto = querySnapshot.Documents.First().ConvertTo<AuthenticationDto>();

            var auth = _mapper.Map<Authentication>(authDto);
            auth.IsAuthenticated = true;

            return auth;
        }

        _logger.LogWarning("Call to Firestore failed when getting data");

        return new Authentication();
    }

    public async Task<List<Authentication>> GetAllAccounts()
    {
        var db = FirestoreDb.Create("mortgager");
        var snapshot = await db.Collection("passwords").GetSnapshotAsync();
        // var dtos = snapshot.Documents.Select(d => d.ConvertTo<AuthenticationDto>());

        


        return snapshot.Documents.Select(d => d.ConvertTo<AuthenticationDto>()).Select(_mapper.Map<Authentication>).ToList();
    }
}