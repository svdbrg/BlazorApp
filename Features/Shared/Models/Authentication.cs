using Google.Cloud.Firestore;

namespace BlazorApp.Features.Shared.Models;

public class Authentication
{
    public string Id { get; set; } = string.Empty;
    public bool IsAuthenticated { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    public string EncryptedPassword { get; set; } = string.Empty;
}

[FirestoreData]
public class AuthenticationDto
{
    [FirestoreDocumentId]
    public string Id { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Name { get; set; } = string.Empty;

    [FirestoreProperty]
    public bool IsAdmin { get; set; }

    [FirestoreProperty]
    public string EncryptedPassword { get; set; } = string.Empty;
}