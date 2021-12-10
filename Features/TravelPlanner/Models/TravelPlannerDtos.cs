namespace BlazorApp.Features.TravelPlanner.Models;
public class ServiceDayDto
{
    public string planningPeriodBegin { get; set; } = string.Empty;
    public string planningPeriodEnd { get; set; } = string.Empty;
    public string sDaysR { get; set; } = string.Empty;
    public string sDaysI { get; set; } = string.Empty;
    public string sDaysB { get; set; } = string.Empty;
}

public class OriginDto
{
    public string name { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
    public string id { get; set; } = string.Empty;
    public string extId { get; set; } = string.Empty;
    public double lon { get; set; }
    public double lat { get; set; }
    public string prognosisType { get; set; } = string.Empty;
    public string time { get; set; } = string.Empty;
    public string date { get; set; } = string.Empty;
    public string rtTime { get; set; } = string.Empty;
    public string rtDate { get; set; } = string.Empty;
    public bool hasMainMast { get; set; }
    public string mainMastId { get; set; } = string.Empty;
    public string mainMastExtId { get; set; } = string.Empty;
    public bool additional { get; set; }
}

public class DestinationDto
{
    public string name { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
    public string id { get; set; } = string.Empty;
    public string extId { get; set; } = string.Empty;
    public double lon { get; set; }
    public double lat { get; set; }
    public string prognosisType { get; set; } = string.Empty;
    public string time { get; set; } = string.Empty;
    public string date { get; set; } = string.Empty;
    public string track { get; set; } = string.Empty;
    public string rtTime { get; set; } = string.Empty;
    public string rtDate { get; set; } = string.Empty;
    public bool hasMainMast { get; set; }
    public string mainMastId { get; set; } = string.Empty;
    public string mainMastExtId { get; set; } = string.Empty;
    public bool additional { get; set; }
}

public class JourneyDetailRefDto
{
    public string @ref { get; set; } = string.Empty;
}

public class ProductDto
{
    public string name { get; set; } = string.Empty;
    public string num { get; set; } = string.Empty;
    public string line { get; set; } = string.Empty;
    public string catOut { get; set; } = string.Empty;
    public string catIn { get; set; } = string.Empty;
    public string catCode { get; set; } = string.Empty;
    public string catOutS { get; set; } = string.Empty;
    public string catOutL { get; set; } = string.Empty;
    public string operatorCode { get; set; } = string.Empty;
    public string @operator { get; set; } = string.Empty;
    public string admin { get; set; } = string.Empty;
}

public class LegDto
{
    public OriginDto Origin { get; set; } = new();
    public DestinationDto Destination { get; set; } = new();
    public JourneyDetailRefDto JourneyDetailRef { get; set; } = new();
    public string JourneyStatus { get; set; } = string.Empty;
    public ProductDto Product { get; set; } = new();
    public object Stops { get; set; } = new();
    public string idx { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string number { get; set; } = string.Empty;
    public string category { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
    public bool reachable { get; set; }
    public bool redirected { get; set; }
    public string direction { get; set; } = string.Empty;
    public string duration { get; set; } = string.Empty;
    public int? dist { get; set; }
    public bool? hide { get; set; }
}

public class LegListDto
{
    public List<LegDto> Leg { get; set; } = new();
}

public class FareItemDto
{
    public string name { get; set; } = string.Empty;
    public string desc { get; set; } = string.Empty;
    public int price { get; set; }
    public string cur { get; set; } = string.Empty;
}

public class FareSetItemDto
{
    public List<FareItemDto> fareItem { get; set; } = new();
    public string name { get; set; } = string.Empty;
    public string desc { get; set; } = string.Empty;
}

public class TariffResultDto
{
    public List<FareSetItemDto> fareSetItem { get; set; } = new();
}

public class TripDto
{
    public List<ServiceDayDto> ServiceDays { get; set; } = new();
    public LegListDto LegList { get; set; } = new();
    public TariffResultDto TariffResult { get; set; } = new();
    public bool alternative { get; set; }
    public bool valid { get; set; }
    public int idx { get; set; }
    public string tripId { get; set; } = string.Empty;
    public string ctxRecon { get; set; } = string.Empty;
    public string duration { get; set; } = string.Empty;
    public bool @return { get; set; }
    public string checksum { get; set; } = string.Empty;
    public int transferCount { get; set; }
}

public class TravelPlannerRootDto
{
    public List<TripDto> Trip { get; set; } = new();
    public string scrB { get; set; } = string.Empty;
    public string scrF { get; set; } = string.Empty;
    public string serverVersion { get; set; } = string.Empty;
    public string dialectVersion { get; set; } = string.Empty;
    public string requestId { get; set; } = string.Empty;
}

public class StopLocationDto
{
    public string id { get; set; } = string.Empty;
    public string extId { get; set; } = string.Empty;
    public bool hasMainMast { get; set; }
    public string mainMastId { get; set; } = string.Empty;
    public string mainMastExtId { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public double lon { get; set; }
    public double lat { get; set; }
    public int weight { get; set; }
    public int dist { get; set; }
    public int products { get; set; }
}

public class StopLocationOrCoordLocationDto
{
    public StopLocationDto StopLocation { get; set; } = new();
}

public class NearbyStopsRoot
{
    public List<StopLocationOrCoordLocationDto> stopLocationOrCoordLocation { get; set; } = new();
    public string serverVersion { get; set; } = string.Empty;
    public string dialectVersion { get; set; } = string.Empty;
    public string requestId { get; set; } = string.Empty;
}

