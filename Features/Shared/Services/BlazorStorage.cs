using BlazorApp.Features.Shared.Services.Abstractions;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorApp.Features.Shared.Services;

public class BlazorStorage : ILocalStorage
{
    private readonly ProtectedLocalStorage _localStore;

    public BlazorStorage(ProtectedLocalStorage localStore)
    {
        _localStore = localStore ?? throw new NullReferenceException(nameof(localStore));
    }

    public async Task<string?> GetStringAsync(string key)
    {
        var result = await _localStore.GetAsync<string>(key);

        if (result.Success)
        {
            return result.Value;
        }

        return null;
    }

    public async Task SaveStringAsync(string key, string value)
    {
        await _localStore.SetAsync(key, value);
    }

    public async Task RemoveAsync(string key)
    {
        await _localStore.DeleteAsync(key);
    }
}