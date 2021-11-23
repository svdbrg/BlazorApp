using BlazorApp.Features.LiveDisplayer.Services;
using BlazorApp.Features.Shared;

namespace BlazorApp.Features.LiveDisplayer.Configuration;

public static class LiveDisplayerConfigurator
{
    public static IServiceCollection ConfigureLiveDisplayer(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddHttpClient("PL", c => {
            c.DefaultRequestHeaders.Add("Origin", "https://www.premierleague.com");
            c.BaseAddress = new Uri("https://footballapi.pulselive.com/");
        });

        services.AddTransient<IDataService, PremierLeagueScraperService>();

        services.Configure<List<NavMenuItem>>(opt =>
        {
            opt.Add(new NavMenuItem
            {
                Href = "livedisplayer",
                Label = "Live Displayer",
                Icon = "oi oi-aperture"
            });
        });

        return services;
    }
}