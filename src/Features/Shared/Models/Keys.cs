namespace BlazorApp.Features.Shared.Models;

public class Keys
{
    public ApiKeys ApiKeys { get; set; } = new();
    public EncryptionKeys EncryptionKeys { get; set; } = new();
}

public class ApiKeys
{
    public string Bing { get; set; } = string.Empty;
    public string SlLineData { get; set; } = string.Empty;
    public string SlNearbyStops { get; set; } = string.Empty;
    public string SlTravelPlanner { get; set; } = string.Empty;
    public string Resrobot { get; set; } = string.Empty;
}

public class EncryptionKeys
{
    public string AuthorizationEncryptionKey { get; set; } = string.Empty;
    public string DocumentSuffixEncryptionKey { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
}