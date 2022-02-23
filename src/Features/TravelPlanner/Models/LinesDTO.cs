namespace BlazorApp.Features.TravelPlanner.Models;

public class LinesResult
{
    public string LineNumber { get; set; } = string.Empty;
    public string DirectionCode { get; set; } = string.Empty;
    public string JourneyPatternPointNumber { get; set; } = string.Empty;
    public string LastModifiedUtcDateTime { get; set; } = string.Empty;
    public string ExistsFromDate { get; set; } = string.Empty;
}

public class LinesResponseData
{
    public string Version { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public List<LinesResult> Result { get; set; } = new();
}

public class LinesDTO
{
    public int StatusCode { get; set; }
    public object Message { get; set; } = new();
    public int ExecutionTime { get; set; }
    public LinesResponseData ResponseData { get; set; } = new();
}