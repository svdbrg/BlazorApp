namespace BlazorApp.Features.LiveDisplayer.Data;

public class PageInfoDto
{
    public int page { get; set; }
    public int numPages { get; set; }
    public int pageSize { get; set; }
    public int numEntries { get; set; }
}

public class CompetitionDto
{
    public string abbreviation { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string level { get; set; } = string.Empty;
    public string source { get; set; } = string.Empty;
    public double id { get; set; }
}

public class CompSeasonDto
{
    public string label { get; set; } = string.Empty;
    public CompetitionDto competition { get; set; } = new();
    public double id { get; set; }
}

public class CompetitionPhaseDto
{
    public double id { get; set; }
    public string type { get; set; } = string.Empty;
    public List<double> gameweekRange { get; set; } = new();
}

public class GameweekDto
{
    public double id { get; set; }
    public CompSeasonDto compSeason { get; set; } = new();
    public double gameweek { get; set; }
    public CompetitionPhaseDto competitionPhase { get; set; } = new();
}

public class KickoffDto
{
    public double completeness { get; set; }
    public double millis { get; set; }
    public string label { get; set; } = string.Empty;
    public string kickoffDay
    {
        get
        {
            if (DateTime.TryParse(label, out var date))
            {
                if (date.Date == DateTime.Today)
                {
                    return "Today";
                }
                else if (date.Date == DateTime.Today.AddDays(1))
                {
                    return "Tomorrow";
                }
                else if (date.Date == DateTime.Today.AddDays(-1))
                {
                    return "Yesterday";
                }
            }

            return label.Split(',')[0];
        }
    }
}

public class ProvisionalKickoffDto
{
    public double completeness { get; set; }
    public double millis { get; set; }
    public string label { get; set; } = string.Empty;
}

public class ClubDto
{
    public string name { get; set; } = string.Empty;
    public string shortName { get; set; } = string.Empty;
    public string abbr { get; set; } = string.Empty;
    public double id { get; set; }
}

public class Team2Dto
{
    public string name { get; set; } = string.Empty;
    public ClubDto club { get; set; } = new();
    public string teamType { get; set; } = string.Empty;
    public string shortName { get; set; } = string.Empty;
    public double id { get; set; }
    public AltIdsDto altIds { get; set; } = new();
}

public class TeamDto
{
    public Team2Dto team { get; set; } = new();
    public double score { get; set; }
}

public class GroundDto
{
    public string name { get; set; } = string.Empty;
    public string city { get; set; } = string.Empty;
    public string source { get; set; } = string.Empty;
    public double id { get; set; }
}

public class ClockDto
{
    public double secs { get; set; }
    public string label { get; set; } = string.Empty;
}

public class GoalDto
{
    public double personId { get; set; }
    public double assistId { get; set; }
    public ClockDto clock { get; set; } = new();
    public string phase { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
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
    public string outcome { get; set; } = string.Empty;
    public double attendance { get; set; }
    public ClockDto clock { get; set; } = new();
    public string fixtureType { get; set; } = string.Empty;
    public bool extraTime { get; set; }
    public bool shootout { get; set; }
    public List<GoalDto> goals { get; set; } = new();
    public List<object> penaltyShootouts { get; set; } = new();
    public bool behindClosedDoors { get; set; }
    public double id { get; set; }
    public AltIdsDto altIds { get; set; } = new();
}

public class RootDto
{
    public PageInfoDto pageInfo { get; set; } = new();
    public List<FixtureDto> content { get; set; } = new();
}

public class AltIdsDto
{
    public string opta { get; set; } = string.Empty;
}

public class TimestampDto
{
    public long millis { get; set; }
    public string label { get; set; } = string.Empty;
}

public class OverallDto
{
    public int played { get; set; }
    public int won { get; set; }
    public int drawn { get; set; }
    public int lost { get; set; }
    public int goalsFor { get; set; }
    public int goalsAgainst { get; set; }
    public int goalsDifference { get; set; }
    public int points { get; set; }
}

public class HomeDto
{
    public int played { get; set; }
    public int won { get; set; }
    public int drawn { get; set; }
    public int lost { get; set; }
    public int goalsFor { get; set; }
    public int goalsAgainst { get; set; }
    public int goalsDifference { get; set; }
    public int points { get; set; }
}

public class AwayDto
{
    public int played { get; set; }
    public int won { get; set; }
    public int drawn { get; set; }
    public int lost { get; set; }
    public int goalsFor { get; set; }
    public int goalsAgainst { get; set; }
    public int goalsDifference { get; set; }
    public int points { get; set; }
}

public class AnnotationDto
{
    public string type { get; set; } = string.Empty;
    public string destination { get; set; } = string.Empty;
}

public class LocationDto
{
    public double latitude { get; set; }
    public double longitude { get; set; }
}

public class TableDto
{
    public int gameWeek { get; set; }
    public List<EntryDto> entries { get; set; } = new();
}

public class EntryDto
{
    public Team2Dto team { get; set; } = new();
    public int position { get; set; }
    public int startingPosition { get; set; }
    public OverallDto overall { get; set; } = new();
    public HomeDto home { get; set; } = new();
    public AwayDto away { get; set; } = new();
    public List<AnnotationDto> annotations { get; set; } = new();
    public GroundDto ground { get; set; } = new();
}

public class TableRootDto
{
    public CompSeasonDto compSeason { get; set; } = new();
    public TimestampDto timestamp { get; set; } = new();
    public bool live { get; set; }
    public bool dynamicallyGenerated { get; set; }
    public List<TableDto> tables { get; set; } = new();
}

public class FixtureRoot
{
    public PageInfoDto pageInfo { get; set; } = new();
    public List<FixtureDto> content { get; set; } = new();
}