using BlazorApp.Features.Mortgager.Data;

namespace BlazorApp.Features.Mortgager.Services.Abstractions;

public interface IMortgageDataService
{
    Task<bool> SaveDataAsync(MortgageItem item);
    Task<MortgageItem?> GetSavedDataAsync();
    Task<List<string>> GetAllMortgageDocuments();
    Task DeleteMortgageAsync(string suffix);
}