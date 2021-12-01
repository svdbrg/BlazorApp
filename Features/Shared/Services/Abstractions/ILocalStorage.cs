namespace BlazorApp.Features.Shared.Services.Abstractions;

public interface ILocalStorage
{
    public Task RemoveAsync(string key);
    public Task SaveStringAsync(string key, string value);
    public Task<string> GetStringAsync(string key);
    public Task SaveStringArrayAsync(string key, string[] values);
    public Task<string[]?> GetStringArrayAsync(string key);
    public Task SaveObjectAsync(string key, object value);
    public Task<T?> GetObjectAsync<T>(string key);
}