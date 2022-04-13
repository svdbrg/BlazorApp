namespace BlazorApp.Features.TravelPlanner.Models;

public class Icon
{
    public string res { get; set; } = string.Empty;
}

public class StopLocation
{
    public List<ProductAtStop> productAtStop { get; set; } = new();
    public int timezoneOffset { get; set; }
    public string id { get; set; } = string.Empty;
    public string extId { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public double lon { get; set; }
    public double lat { get; set; }
    public int weight { get; set; }
    public int products { get; set; }
}

public class Link2
{
    public string rel { get; set; } = string.Empty;
    public string href { get; set; } = string.Empty;
}

public class Link
{
    public List<Link2> link { get; set; } = new();
}

public class CoordLocation
{
    public List<Link> links { get; set; } = new();
    public Icon icon { get; set; } = new();
    public string id { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
    public double lon { get; set; }
    public double lat { get; set; }
    public bool refinable { get; set; }
}

public class StopLocationOrCoordLocation
{
    public StopLocation StopLocation { get; set; } = new();
    public CoordLocation CoordLocation { get; set; } = new();
}

public class TechnicalMessage
{
    public string value { get; set; } = string.Empty;
    public string key { get; set; } = string.Empty;
}

public class TechnicalMessages
{
    public List<TechnicalMessage> TechnicalMessage { get; set; } = new();
}

public class ResrobotDTO
{
    public List<StopLocationOrCoordLocation> stopLocationOrCoordLocation { get; set; } = new();
    public TechnicalMessages TechnicalMessages { get; set; } = new();
    public string serverVersion { get; set; } = string.Empty;
    public string dialectVersion { get; set; } = string.Empty;
    public string requestId { get; set; } = string.Empty;
}

