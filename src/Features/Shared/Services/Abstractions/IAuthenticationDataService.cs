using BlazorApp.Features.Shared.Models;

namespace BlazorApp.Features.Shared.Services.Abstractions;

public interface IAuthenticationDataService
{
    Task<Authentication> Authenticate(string password);
    Task<List<Authentication>> GetAllAccounts();
    Task<bool> SaveNewUser(Authentication newUser, string password);
    Task<bool> DeleteUser(Authentication user);
}