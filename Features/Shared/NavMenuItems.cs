namespace BlazorApp.Features.Shared;

public class NavMenuItems
{
    public List<NavMenuItem> Items { get; set; } = new();
}

public class NavMenuItem
{
    public string Href { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}