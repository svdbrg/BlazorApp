namespace BlazorApp.Features.Shared.Services.Abstractions;

public interface ILocalStorage
{
    Task RemoveAsync(string key);
    Task SaveStringAsync(string key, string value);
    Task<string> GetStringAsync(string key);
    Task SaveStringArrayAsync(string key, string[] values);
    Task<string[]?> GetStringArrayAsync(string key);
    Task SaveObjectAsync(string key, object value);
    Task<T?> GetObjectAsync<T>(string key);
}