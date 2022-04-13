namespace BlazorApp.Features.TravelPlanner.Models;

public class JourneyDetailRef
{
    public string @ref { get; set; } = string.Empty;
}

public class ProductAtStop
{
    public Icon icon { get; set; } = new();
    public string name { get; set; } = string.Empty;
    public string internalName { get; set; } = string.Empty;
    public string displayNumber { get; set; } = string.Empty;
    public string num { get; set; } = string.Empty;
    public string line { get; set; } = string.Empty;
    public string lineId { get; set; } = string.Empty;
    public string catOut { get; set; } = string.Empty;
    public string catIn { get; set; } = string.Empty;
    public string catCode { get; set; } = string.Empty;
    public string cls { get; set; } = string.Empty;
    public string catOutS { get; set; } = string.Empty;
    public string catOutL { get; set; } = string.Empty;
    public string operatorCode { get; set; } = string.Empty;
    public string @operator { get; set; } = string.Empty;
    public string admin { get; set; } = string.Empty;
    public string matchId { get; set; } = string.Empty;
}

public class Product
{
    public Icon icon { get; set; } = new();
    public string name { get; set; } = string.Empty;
    public string internalName { get; set; } = string.Empty;
    public string displayNumber { get; set; } = string.Empty;
    public string num { get; set; } = string.Empty;
    public string line { get; set; } = string.Empty;
    public string lineId { get; set; } = string.Empty;
    public string catOut { get; set; } = string.Empty;
    public string catIn { get; set; } = string.Empty;
    public string catCode { get; set; } = string.Empty;
    public string cls { get; set; } = string.Empty;
    public string catOutS { get; set; } = string.Empty;
    public string catOutL { get; set; } = string.Empty;
    public string operatorCode { get; set; } = string.Empty;
    public string @operator { get; set; } = string.Empty;
    public string admin { get; set; } = string.Empty;
    public int routeIdxFrom { get; set; }
    public int routeIdxTo { get; set; }
    public string matchId { get; set; } = string.Empty;
}

public class Departure
{
    public JourneyDetailRef JourneyDetailRef { get; set; } = new();
    public string JourneyStatus { get; set; } = string.Empty;
    public ProductAtStop ProductAtStop { get; set; } = new();
    public List<Product> Product { get; set; } = new();
    public string name { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
    public string stop { get; set; } = string.Empty;
    public string stopid { get; set; } = string.Empty;
    public string stopExtId { get; set; } = string.Empty;
    public string time { get; set; } = string.Empty;
    public string date { get; set; } = string.Empty;
    public bool reachable { get; set; }
    public string direction { get; set; } = string.Empty;
    public string directionFlag { get; set; } = string.Empty;
}

public class DeparturesDTO
{
    public List<Departure> Departure { get; set; } = new();
    public TechnicalMessages TechnicalMessages { get; set; } = new();
    public string serverVersion { get; set; } = string.Empty;
    public string dialectVersion { get; set; } = string.Empty;
    public DateTime planRtTs { get; set; } = new();
    public string requestId { get; set; } = string.Empty;
}

