using BlazorApp.Features.LiveDisplayer.Services;
using BlazorApp.Features.Shared;

namespace BlazorApp.Features.LiveDisplayer.Configuration;
public static class LiveDisplayerConfigurator
{
    public static IServiceCollection ConfigureLiveDisplayer(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddHttpClient();
        services.AddTransient<IDataService, PremierLeagueScraperService>();

        services.Configure<NavMenuItems>(opt =>
        {
            opt.Items.Add(new NavMenuItem
            {
                Href = "livedisplayer",
                Label = "Live Displayer",
                Icon = "oi oi-aperture"
            });
        });

        return services;
    }
}