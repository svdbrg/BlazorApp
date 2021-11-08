namespace BlazorApp.Features.LiveDisplayer.Data;

public class Day {
    public string Date { get; set; } = string.Empty;
    public List<Fixture> Fixtures { get; set; } = new();
}

public class Fixture
{    
    public string HomeTeam { get; set; } = string.Empty;
    public string AwayTeam { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
}