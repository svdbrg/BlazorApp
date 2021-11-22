using System.Text.Json;
using AutoMapper;
using BlazorApp.Features.LiveDisplayer.Data;
using HtmlAgilityPack;

namespace BlazorApp.Features.LiveDisplayer.Services;

public class PremierLeagueScraperService : IDataService
{
    private readonly ILogger<PremierLeagueScraperService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMapper _mapper;

    public PremierLeagueScraperService(ILogger<PremierLeagueScraperService> logger, IHttpClientFactory httpClientFactory, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async IAsyncEnumerable<Day> GetDaysAndFixturesAsync()
    {
        var days = new List<Day>();
        var dtos = await GetFixturesWithApiAsync();
        var dtosGrouped = dtos.content.GroupBy(c => c.kickoff.kickoffDay);

        foreach (var item in dtosGrouped)
        {
            yield return new Day
            {
                Date = item.Key,
                Fixtures = dtosGrouped
                    .SelectMany(fix => fix)
                    .Where(d => d.kickoff.kickoffDay == item.Key)
                    .Select(f => _mapper.Map<Fixture>(f))
            };
        }
    }

    private async Task<RootDto> GetFixturesWithApiAsync()
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            var rawHtml = await client.GetStringAsync("https://www.premierleague.com/");

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawHtml);

            var gameweekId = htmlDoc.DocumentNode
                ?.Descendants("div")
                ?.Where(d => d.GetAttributeValue("data-widget", "miss") == "gameweek-matches")
                ?.First()
                ?.GetDataAttribute("gameweek")
                ?.Value;

            if (gameweekId == null)
            {
                return new RootDto();
            }

            client.DefaultRequestHeaders.Add("Origin", "https://www.premierleague.com");
            var response = await client.GetAsync($"https://footballapi.pulselive.com/football/fixtures?pageSize=100&comps=1&gameweeks={gameweekId}");

            return JsonSerializer.Deserialize<RootDto>(await response.Content.ReadAsStreamAsync()) ?? new RootDto();
        }
    }

    public async Task<TableStandings> GetTableAsync()
    {
        var tableToReturn = new TableStandings();
        using (var client = _httpClientFactory.CreateClient())
        {
            var rawHtml = await client.GetStringAsync("https://www.premierleague.com/tables");

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(rawHtml);

            var table = htmlDoc.DocumentNode
                .Descendants("tbody")
                ?.FirstOrDefault(t => t.HasClass("isPL"))
                ?.SelectNodes("//tr")
                .Where(tr => tr.GetAttributeValue("data-filtered-table-row-name", null) != null
                && tr.GetAttributeValue("data-filtered-entry-size", "") == "20");

            if (table == null)
            {
                throw new KeyNotFoundException();
            }

            foreach (var row in table)
            {
                if (row == null)
                {
                    continue;
                }

                var teamTds = row.ChildNodes
                    ?.FirstOrDefault(c => c.HasClass("team"))
                    ?.Descendants("span")
                    .Where(s => s.HasClass("long") || s.HasClass("short")).ToArray();

                if (teamTds == null)
                {
                    continue;
                }

                var position = row.GetAttributeValue<int>("data-position", 0);
                var name = teamTds[0].InnerText;
                var shortName = teamTds[1].InnerText;
                var points = row.Descendants("td")
                    ?.FirstOrDefault(d => d.HasClass("points"))
                    ?.InnerText;

                var gd = row
                    .ChildNodes?[19]
                    ?.InnerText
                    .Trim();

                var played = row
                    .ChildNodes?[7]
                    .InnerText;

                tableToReturn.Teams.Add(new Team
                {
                    Position = position,
                    Name = name,
                    ShortName = shortName,
                    Played = played != null ? int.Parse(played) : 0,
                    GoalDifference = gd ?? "0",
                    Points = points != null ? int.Parse(points) : 0
                });
            }

            return tableToReturn;
        }
    }
}