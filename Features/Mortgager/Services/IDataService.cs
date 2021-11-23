using BlazorApp.Features.Mortgager.Data;

namespace BlazorApp.Features.Mortgager.Services;

public interface IDataService {
    Task<string> SaveDataAsync(MortgageItem item);
    Task<MortgageItem?> GetSavedDataAsync();
}