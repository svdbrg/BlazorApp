using BlazorApp.Features.Mortgager.Data;
using BlazorApp.Features.Shared.Models;

namespace BlazorApp.Features.Shared.Services.Abstractions;

public interface IDataService
{
    Task<bool> SaveDataAsync(MortgageItem item);
    Task<MortgageItem?> GetSavedDataAsync();
    Task<List<string>> GetAllMortgageDocuments();
    Task DeleteMortgageAsync(string suffix);
    Task<Authentication> Authenticate(string password);
    Task<List<Authentication>> GetAllAccounts();
    Task SaveNewUser(Authentication newUser, string password);
    Task DeleteUser(Authentication user);
}