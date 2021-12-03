namespace BlazorApp.Features.LiveDisplayer.Data;

public class Day
{
    public string Date { get; set; } = string.Empty;
    public IEnumerable<Fixture> Fixtures { get; set; } = new List<Fixture>();
}

public class Fixture
{
    public string HomeTeam { get; set; } = string.Empty;
    public string AwayTeam { get; set; } = string.Empty;
    public string HomeTeamShortName { get; set; } = string.Empty;
    public string AwayTeamShortName { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime DateAndTime { get; set; }
}