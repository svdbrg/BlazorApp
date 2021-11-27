namespace BlazorApp.Features.LiveDisplayer.Services;

public static class HelperMethods
{
    public static string GetCssClassForCurrentStatus(string status)
    {
        switch (status)
        {
            case "L": // Currently playing
                return "team-is-playing";
            case "C": // Game is over
                return "team-is-done";
            case "U": // Not started
            default:
                return "";
        }
    }
}