using BlazorApp.Features.Mortgager.Data;

namespace BlazorApp.Features.Mortgager.Services.Abstractions;

public interface IDataService {
    Task<bool> SaveDataAsync(MortgageItem item);
    Task<MortgageItem?> GetSavedDataAsync();
    Task<List<string>> GetAllMortgageDocuments();
    Task DeleteMortgageAsync(string suffix);
}