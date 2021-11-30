namespace BlazorApp.Models;

public class AppState
{
    public bool AutoRefresh { get; private set; }
    public bool ShouldDisplayToggler { get; private set; }
    public event Action? OnChange;

    public void ToggleAutoRefresh()
    {
        ToggleAutoRefresh(!AutoRefresh);
    }

    public void ToggleAutoRefresh(bool toggle)
    {
        AutoRefresh = toggle;
        NotifyStateChanged();
    }

    public void ToggleDisplayMode(bool displayMode)
    {
        ShouldDisplayToggler = displayMode;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}