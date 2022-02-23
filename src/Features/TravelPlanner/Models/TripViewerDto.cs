namespace BlazorApp.Features.TravelPlanner.Models;

public class TripViewerDto
{
    public BoxDto Box { get; set; } = new();
    public IList<StopDto> Stops { get; set; } = new List<StopDto>();
}

public class BoxDto
{
    public double MaxLong { get; set; }
    public double MinLong { get; set; }
    public double MaxLat { get; set; }
    public double MinLat { get; set; }
}

public class StopDto
{
    public double Long { get; set; }
    public double Lat { get; set; }
    public string Name { get; set; } = string.Empty;
}