namespace BlazorApp.Features.TravelPlanner.Models;

public class Trip
{
    public IEnumerable<Leg> Legs { get; set; } = new List<Leg>();
    public string Line { get; set; } = string.Empty;
}

public class Leg
{
    public Stop Origin { get; set; } = new();
    public Stop Destination { get; set; } = new();
}

public class Stop
{
    public string Name { get; set; } = string.Empty;
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public DateTime Time { get; set; }
}