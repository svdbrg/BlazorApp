// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


public class BroadcasterDto
{
    public string name { get; set; } = string.Empty;
    public string abbreviation { get; set; } = string.Empty;
    public string url { get; set; } = string.Empty;
    public string country { get; set; } = string.Empty;
    public List<TvShowDto> tvShows { get; set; } = new();
}

public class PageInfoDto
{
    public int page { get; set; }
    public int numPages { get; set; }
    public int pageSize { get; set; }
    public int numEntries { get; set; }
}

public class GameweekDto
{
    public int id { get; set; }
    public int gameweek { get; set; }
}

public class KickoffDto
{
    public int completeness { get; set; }
    public object millis { get; set; } = new();
    public string label { get; set; } = string.Empty;
    public string kickoffDay
    {
        get
        {
            return label.Split(',')[0];
        }
    }
}

public class ProvisionalKickoffDto
{
    public int completeness { get; set; }
    public object millis { get; set; } = new();
    public string label { get; set; } = string.Empty;
}

public class ClubDto
{
    public string name { get; set; } = string.Empty;
    public string abbr { get; set; } = string.Empty;
    public int id { get; set; }
}

public class Team2Dto
{
    public string name { get; set; } = string.Empty;
    public ClubDto club { get; set; } = new();
    public string teamType { get; set; } = string.Empty;
    public string shortName { get; set; } = string.Empty;
    public int id { get; set; }
}

public class TeamDto
{
    public Team2Dto team { get; set; } = new();
}

public class GroundDto
{
    public string name { get; set; } = string.Empty;
    public string city { get; set; } = string.Empty;
    public string source { get; set; } = string.Empty;
    public int id { get; set; }
}

public class FixtureDto
{
    public GameweekDto gameweek { get; set; } = new();
    public KickoffDto kickoff { get; set; } = new();
    public ProvisionalKickoffDto provisionalKickoff { get; set; } = new();
    public List<TeamDto> teams { get; set; } = new();
    public bool replay { get; set; }
    public GroundDto ground { get; set; } = new();
    public bool neutralGround { get; set; }
    public string status { get; set; } = string.Empty;
    public string phase { get; set; } = string.Empty;
    public string fixtureType { get; set; } = string.Empty;
    public bool extraTime { get; set; }
    public bool shootout { get; set; }
    public bool behindClosedDoors { get; set; }
    public int id { get; set; }
}

public class TvShowDto
{
    public string channel { get; set; } = string.Empty;
    public List<string> territories { get; set; } = new();
    public string showTime { get; set; } = string.Empty;
    public string timeZone { get; set; } = string.Empty;
    public string abbreviation { get; set; } = string.Empty;
    public List<string> streamingURLs { get; set; } = new();
}

public class ContentDto
{
    public FixtureDto fixture { get; set; } = new();
    public List<BroadcasterDto> broadcasters { get; set; } = new();
}

public class RootDto
{
    public string countryCode { get; set; } = string.Empty;
    public List<BroadcasterDto> broadcasters { get; set; } = new();
    public PageInfoDto pageInfo { get; set; } = new();
    public List<ContentDto> content { get; set; } = new();
}

