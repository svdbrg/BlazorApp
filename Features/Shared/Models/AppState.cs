namespace BlazorApp.Features.Shared.Models;

public class AppState
{
    public bool AutoRefresh { get; private set; }
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

    public void ChangeMortageSetting()
    {
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}