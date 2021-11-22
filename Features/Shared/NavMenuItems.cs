namespace BlazorApp.Features.Shared;

public class NavMenuItems {
    public List<NavMenuItem> Items { get; set; }
}

public class NavMenuItem {
    public string Href { get; set; }
    public string Label { get; set; }
    public string Icon { get; set; }
}