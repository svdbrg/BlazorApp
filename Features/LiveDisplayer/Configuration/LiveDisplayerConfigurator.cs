using BlazorApp.Features.LiveDisplayer.Services;

namespace BlazorApp.Features.LiveDisplayer.Configuration;
public static class LiveDisplayerConfigurator
{
    public static IServiceCollection ConfigureLiveDisplayer(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddTransient<IDataService, PremierLeagueScraperService>();

        return services;
    }
}