using BlazorApp.Features.Shared.Models;

namespace BlazorApp.Features.Shared.Services.Abstractions;

public interface IAuthenticationDataService
{
    Task<Authentication> Authenticate(string password);
    Task<List<Authentication>> GetAllAccounts();
    Task SaveNewUser(Authentication newUser, string password);
    Task DeleteUser(Authentication user);
}