namespace BlazorApp.Features.LiveDisplayer.Data;

public class TableStandings
{
    public List<Team> Teams { get; set; } = new();
}

public class Team
{
    public int Position { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public int Played { get; set; }
    public int GoalDifference { get; set; }
    public int Points { get; set; }
}