namespace BlazorApp.Features.LiveDisplayer.Data;

public class Team
{
    public int Position { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public int Played { get; set; }
    public int GoalDifference { get; set; }
    public int Points { get; set; }
    public string Status { get; set; } = string.Empty;
    public string CssClass { get; set; } = "none";
}