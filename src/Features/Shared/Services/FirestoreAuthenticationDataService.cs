using AutoMapper;
using BlazorApp.Features.Shared.Models;
using BlazorApp.Features.Shared.Services.Abstractions;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;

namespace BlazorApp.Features.Shared.Services;

public class FirestoreAuthenticationDataService : IAuthenticationDataService
{
    private readonly IMapper _mapper;
    private readonly ILogger<FirestoreAuthenticationDataService> _logger;
    private readonly EncryptionKeys _encryptionKeys;

    public FirestoreAuthenticationDataService(IMapper mapper, ILogger<FirestoreAuthenticationDataService> logger, IOptions<EncryptionKeys> encryptionKeys)
    {
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
        _encryptionKeys = encryptionKeys?.Value ?? throw new NullReferenceException(nameof(encryptionKeys));
    }

    public async Task<Authentication> Authenticate(string password)
    {
        var token = Encryption.EncryptString($"{password}-{_encryptionKeys.Salt}", _encryptionKeys.AuthorizationEncryptionKey);

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

        return snapshot.Documents
            .Select(d => d.ConvertTo<AuthenticationDto>())
            .Select(_mapper.Map<Authentication>)
            .ToList();
    }

    public async Task<bool> SaveNewUser(Authentication newUser, string password)
    {
        var token = Encryption.EncryptString($"{password}-{_encryptionKeys.Salt}", _encryptionKeys.AuthorizationEncryptionKey);
        newUser.EncryptedPassword = token;

        var newUserDto = _mapper.Map<AuthenticationDto>(newUser);

        var db = FirestoreDb.Create("mortgager");
        var docref = await db.Collection("passwords").AddAsync(newUserDto);

        return docref?.Id != null;
    }

    public async Task<bool> DeleteUser(Authentication user)
    {
        var db = FirestoreDb.Create("mortgager");
        var document = db.Collection("passwords").Document(user.Id);
        var result = await document.DeleteAsync();

        return result?.UpdateTime != null;
    }
}