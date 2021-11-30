namespace BlazorApp.Features.Mortgager.Services.Abstractions;

public interface ILocalStorage
{
    /// <summary>
    /// Remove a key from browser local storage.
    /// </summary>
    /// <param name="key">The key previously used to save to local storage.</param>
    public Task RemoveAsync(string key);

    /// <summary>
    /// Save a string value to browser local storage.
    /// </summary>
    /// <param name="key">The key to use to save to and retrieve from local storage.</param>
    /// <param name="value">The string value to save to local storage.</param>
    public Task SaveStringAsync(string key, string value);

    /// <summary>
    /// Get a string value from browser local storage.
    /// </summary>
    /// <param name="key">The key previously used to save to local storage.</param>
    /// <returns>The string previously saved to local storage.</returns>
    public Task<string> GetStringAsync(string key);

    /// <summary>
    /// Save an array of string values to browser local storage.
    /// </summary>
    /// <param name="key">The key previously used to save to local storage.</param>
    /// <param name="values">The array of string values to save to local storage.</param>
    public Task SaveStringArrayAsync(string key, string[] values);

    /// <summary>
    /// Get an array of string values from browser local storage.
    /// </summary>
    /// <param name="key">The key previously used to save to local storage.</param>
    /// <returns>The array of string values previously saved to local storage.</returns>
    public Task<string[]?> GetStringArrayAsync(string key);

    /// <summary>
    /// Save an object value to browser local storage.
    /// </summary>
    /// <param name="key">The key to use to save to and retrieve from local storage.</param>
    /// <param name="value">The object to save to local storage.</param>
    public Task SaveObjectAsync(string key, object value);

    /// <summary>
    /// Get an object from browser local storage.
    /// </summary>
    /// <param name="key">The key previously used to save to local storage.</param>
    /// <returns>The object previously saved to local storage.</returns>
    public Task<T?> GetObjectAsync<T>(string key);
}