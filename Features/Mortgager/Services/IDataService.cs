using BlazorApp.Features.Mortgager.Data;

namespace BlazorApp.Features.Mortgager.Services;

public interface IDataService {
    Task<string> SaveData(MortgageItem item);
    Task<MortgageItem?> GetSavedData();
}