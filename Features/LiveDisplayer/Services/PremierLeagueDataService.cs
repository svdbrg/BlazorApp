using System.Text.Json;
using AutoMapper;
using BlazorApp.Features.LiveDisplayer.Data;
using HtmlAgilityPack;

namespace BlazorApp.Features.LiveDisplayer.Services;

public class PremierLeagueDataService : IFootballDataService
{
    private readonly ILogger<PremierLeagueDataService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMapper _mapper;

    public PremierLeagueDataService(ILogger<PremierLeagueDataService> logger, IHttpClientFactory httpClientFactory, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async IAsyncEnumerable<Day> GetDaysAndFixturesAsync()
    {
        var gameweekId = await GetGameweekId();
        var fixtureDtos = await GetFixturesWithApiAsync(gameweekId);
        var dtosGrouped = fixtureDtos.content.GroupBy(c => c.kickoff.kickoffDay);

        foreach (var item in dtosGrouped)
        {
            yield return new Day
            {
                Date = item.Key,
                Fixtures = dtosGrouped
                    .SelectMany(fix => fix)
                    .Where(d => d.kickoff.kickoffDay == item.Key)
                    .Select(_mapper.Map<Fixture>)
            };
        }
    }

    public async Task<IEnumerable<Team>> GetTableAsync()
    {
        using (var client = _httpClientFactory.CreateClient("PL"))
        {
            var response = await client.GetAsync("/football/standings?compSeasons=418&altIds=true&live=true");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("No table received", response.ReasonPhrase);

                return new List<Team>();
            }

            _logger.LogInformation("Received table from API");
            var data = JsonSerializer.Deserialize<TableRootDto>(await response.Content.ReadAsStreamAsync()) ?? new TableRootDto();

            return data.tables
                .First()
                .entries
                .Select(_mapper.Map<Team>)
                .OrderBy(t => t.Position);
        }
    }

    public IEnumerable<Team> EnrichTableWithStatus(IEnumerable<Team> teams, IEnumerable<Day> days)
    {
        foreach (var team in teams)
        {
            var allFixtures = days.SelectMany(d => d.Fixtures);

            team.Status = allFixtures
                ?.FirstOrDefault(f => f.AwayTeamShortName == team.ShortName || f.HomeTeamShortName == team.ShortName)
                ?.Status ?? "N/A";

            yield return team;
        }
    }

    public async Task<IEnumerable<Fixture>> GetUpcomingFixtureForTeam(int teamId)
    {
        using (var client = _httpClientFactory.CreateClient("PL"))
        {
            var response = await client.GetAsync($"/football/fixtures?comps=1&compSeasons=418&page=0&pageSize=8&sort=asc&statuses=U,L&altIds=true&teams={teamId}");

            _logger.LogInformation("Revieved upcoming fixtures for team with id {teamId}", teamId);
            var data = JsonSerializer.Deserialize<FixtureRoot>(await response.Content.ReadAsStreamAsync()) ?? new FixtureRoot();

            return data.content.Where(c => c.kickoff.completeness > 0.0).Select(_mapper.Map<Fixture>);
        }
    }

    private async Task<RootDto> GetFixturesWithApiAsync(string? gameweekId)
    {
        using (var client = _httpClientFactory.CreateClient("PL"))
        {
            if (gameweekId == null)
            {
                return new RootDto();
            }

            var response = await client.GetAsync($"/football/fixtures?pageSize=100&comps=1&altIds=true&gameweeks={gameweekId}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("No fixtures received", response.ReasonPhrase);
                return new RootDto();
            }

            _logger.LogInformation("Received fixtures from API");
            return JsonSerializer.Deserialize<RootDto>(await response.Content.ReadAsStreamAsync()) ?? new RootDto();
        }
    }

    private async Task<string?> GetGameweekId()
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            var rawHtml = await client.GetStringAsync("https://www.premierleague.com/");

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawHtml);

            var gameweek = htmlDoc.DocumentNode
                ?.Descendants("div")
                ?.Where(d => d.GetAttributeValue("data-widget", "not found").Equals("gameweek-matches"))
                ?.FirstOrDefault()
                ?.GetDataAttribute("gameweek")
                ?.Value;

            if (gameweek == null)
            {
                gameweek = htmlDoc.DocumentNode
                    ?.Descendants("div")
                    ?.Where(d => d.HasClass("matchWeekNavContainer"))
                    ?.FirstOrDefault()
                    ?.GetDataAttribute("gameweeks")
                    ?.Value;
            }

            return gameweek;
        }
    }
}