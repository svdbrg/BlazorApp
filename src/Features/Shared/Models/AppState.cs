using BlazorApp.Features.TravelPlanner.Models;

namespace BlazorApp.Features.Shared.Models;

public class AppState
{
    public bool AutoRefresh { get; private set; }
    public NearbyStop HomeStation { get; private set; } = new();
    public NearbyStop DestinationStation { get; private set; } = new();
    public bool IsDarkMode { get; private set; }
    public event Action? OnChange;

    public void SetHomeStation(NearbyStop newStation)
    {
        HomeStation = newStation;
        NotifyStateChanged();
    }

    public void SetDestination(NearbyStop newStation)
    {
        DestinationStation = newStation;
        NotifyStateChanged();
    }

    public void SetDarkMode(bool darkMode)
    {
        IsDarkMode = darkMode;
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

public class LoadingState
{
    public bool IsLoading { get; private set; }
    public event Action? OnChange;

    public void ToggleLoading(bool isLoading)
    {
        IsLoading = isLoading;
        NotifyStateChanged();
    }

    public void ToggleLoading()
    {
        ToggleLoading(!IsLoading);
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

public class AuthState
{
    public bool IsAdmin { get; set; }
    public event Action? OnChange;

    public void IsAuthenticated()
    {
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

public class SettingsState
{
    public event Action? OnChange;

    public void IsSaving()
    {
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}