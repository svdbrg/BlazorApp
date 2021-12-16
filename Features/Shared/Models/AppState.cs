namespace BlazorApp.Features.Shared.Models;

public class AppState
{
    public bool AutoRefresh { get; private set; }
    public string HomeStation { get; private set; } = "300104014";
    public string DestinationStation { get; private set; } = "300109192";
    public event Action? OnChange;

    public void SetHomeStation(string newStation)
    {
        HomeStation = newStation;
        NotifyStateChanged();
    }

    public void SetDestination(string newStation)
    {
        DestinationStation = newStation;
        NotifyStateChanged();
    }

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