using BlazorApp.Features.Shared.Models;

namespace BlazorApp.Features.TravelPlanner.Configuration;

public static class LiveDisplayerConfigurator
{
    private static FeatureInformation featureInformation = new FeatureInformation
    {
        NavMenuItem = new NavMenuItem
        {
            Href = "travelplanner",
            Label = "Travel Planner",
            Icon = "oi oi-headphones"
        },
        Name = "Travel Planner",
        Description = "Makes it a breeze to get an overview of when the next bus or train leaves."
    };

    public static IServiceCollection ConfigureTravelPlanner(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddHttpClient("TP", c =>
        {
            // c.DefaultRequestHeaders.Add("Origin", "https://www.premierleague.com");
            // c.BaseAddress = new Uri("https://footballapi.pulselive.com/");
        });

        services.Configure<List<FeatureInformation>>(opt =>
        {
            opt.Add(featureInformation);
        });

        return services;
    }
}