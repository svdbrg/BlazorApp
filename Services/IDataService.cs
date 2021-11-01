using BlazorApp.Data;

namespace BlazorApp.Services;

public interface IDataService {
    Task<string> SaveData(MortgageItem item);
    Task<MortgageItem?> GetSavedData();
}