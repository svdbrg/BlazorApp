namespace BlazorApp.Features.TravelPlanner.Models;

public class RouteStopsDto
{
    public MapBox Box { get; set; } = new();
    public IList<StopsResultDto> Stops { get; set; } = new List<StopsResultDto>();
}

public class MapBox
{
    public double MaxLat { get; set; }
    public double MinLat { get; set; }
    public double MaxLon { get; set; }
    public double MinLon { get; set; }
}