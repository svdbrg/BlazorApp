namespace BlazorApp.Features.TravelPlanner.Models;

public class StopsResult
{
    public string StopPointNumber { get; set; } = string.Empty;
    public string StopPointName { get; set; } = string.Empty;
    public string StopAreaNumber { get; set; } = string.Empty;
    public string LocationNorthingCoordinate { get; set; } = string.Empty;
    public string LocationEastingCoordinate { get; set; } = string.Empty;
    public string ZoneShortName { get; set; } = string.Empty;
    public string StopAreaTypeCode { get; set; } = string.Empty;
    public string LastModifiedUtcDateTime { get; set; } = string.Empty;
    public string ExistsFromDate { get; set; } = string.Empty;
}

public class StopsResultDto
{
    public string StopPointNumber { get; set; } = string.Empty;
    public string StopPointName { get; set; } = string.Empty;
    public string StopAreaNumber { get; set; } = string.Empty;
    public double LocationNorthingCoordinate { get; set; }
    public double LocationEastingCoordinate { get; set; }
    public string ZoneShortName { get; set; } = string.Empty;
    public string StopAreaTypeCode { get; set; } = string.Empty;
    public string LastModifiedUtcDateTime { get; set; } = string.Empty;
    public string ExistsFromDate { get; set; } = string.Empty;
}

public class StopsResponseData
{
    public string Version { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public List<StopsResult> Result { get; set; } = new();
}

public class StopsDTO
{
    public int StatusCode { get; set; }
    public object Message { get; set; } = new();
    public int ExecutionTime { get; set; }
    public StopsResponseData ResponseData { get; set; } = new();
}