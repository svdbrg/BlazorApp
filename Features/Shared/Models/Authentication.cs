using Google.Cloud.Firestore;

namespace BlazorApp.Features.Shared.Models;

public class Authentication
{
    public bool IsAuthenticated { get; set; }
    public bool IsAdmin { get; set; }
}

[FirestoreData]
public class AuthenticationDto
{
    [FirestoreProperty]
    public bool IsAdmin { get; set; }
}