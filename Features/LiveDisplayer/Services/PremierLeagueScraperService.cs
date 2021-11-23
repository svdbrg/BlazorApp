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

    public async Task<IEnumerable<Team>> GetTableAsync()
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            client.DefaultRequestHeaders.Add("Origin", "https://www.premierleague.com");
            var response = await client.GetAsync("https://footballapi.pulselive.com/football/standings?compSeasons=418&altIds=true");

            var data = JsonSerializer.Deserialize<TableRootDto>(await response.Content.ReadAsStreamAsync()) ?? new TableRootDto();

            return data.tables
                .First()
                .entries
                .Select(_mapper.Map<Team>)
                .OrderBy(t => t.Position);
        }
    }
}