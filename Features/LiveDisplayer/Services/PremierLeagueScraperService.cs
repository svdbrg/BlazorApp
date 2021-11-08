using BlazorApp.Features.LiveDisplayer.Data;
using HtmlAgilityPack;

namespace BlazorApp.Features.LiveDisplayer.Services;

public class PremierLeagueScraperService : IDataService
{
    private readonly ILogger<PremierLeagueScraperService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public PremierLeagueScraperService(ILogger<PremierLeagueScraperService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task<IEnumerable<Fixture>> GetFixtures()
    {
        var fixtures = new List<Fixture>();
        var client = _httpClientFactory.CreateClient();
        var rawHtml = await client.GetStringAsync("https://www.premierleague.com/");

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(rawHtml);

        var matches = htmlDoc.DocumentNode.Descendants("a").Where(a => a.HasClass("embeddableMatchContainer"));

        foreach (var match in matches)
        {
            var teamNames = match.Descendants("abbr").ToArray();
            var home = teamNames[0].InnerText.Trim();
            var away = teamNames[1].InnerText.Trim();

            var result = match
                ?.Descendants("span")
                ?.FirstOrDefault(s => s.HasClass("score"))
                ?.InnerText;

            fixtures.Add(new Fixture
            {
                HomeTeam = home,
                AwayTeam = away,
                Result = result ?? "N/A"
            });
        }

        return fixtures;
    }

    public async Task<TableStandings> GetTable()
    {
        return await GetTableValuesAsync();
    }

    private async Task<TableStandings> GetTableValuesAsync()
    {
        var tableToReturn = new TableStandings();
        var client = _httpClientFactory.CreateClient();
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