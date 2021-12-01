using BlazorApp.Features.LiveDisplayer.Services;
using BlazorApp.Features.Shared;

namespace BlazorApp.Features.LiveDisplayer.Configuration;

public static class LiveDisplayerConfigurator
{
    private static FeatureInformation featureInformation = new FeatureInformation
    {
        NavMenuItem = new NavMenuItem
        {
            Href = "livedisplayer",
            Label = "Live Displayer",
            Icon = "oi oi-aperture"
        },
        Name = "Live Displayer",
        Description = "A live display to get an overview of the current Premier League table and current and/or upcoming fixtures."
    };

    public static IServiceCollection ConfigureLiveDisplayer(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddHttpClient("PL", c =>
        {
            c.DefaultRequestHeaders.Add("Origin", "https://www.premierleague.com");
            c.BaseAddress = new Uri("https://footballapi.pulselive.com/");
        });

        services.AddTransient<IFootballDataService, PremierLeagueDataService>();

        services.Configure<List<FeatureInformation>>(opt =>
        {
            opt.Add(featureInformation);
        });

        return services;
    }
}