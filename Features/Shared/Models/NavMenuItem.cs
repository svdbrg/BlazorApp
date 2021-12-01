namespace BlazorApp.Features.Shared.Models;

public class FeatureInformation
{
    public NavMenuItem NavMenuItem { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

}

public class NavMenuItem
{
    public string Href { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}