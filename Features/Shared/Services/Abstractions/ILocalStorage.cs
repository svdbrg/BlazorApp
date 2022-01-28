namespace BlazorApp.Features.Shared.Services.Abstractions;

public interface ILocalStorage
{
    Task RemoveAsync(string key);
    Task SaveStringAsync(string key, string value);
    Task<string?> GetStringAsync(string key);
}