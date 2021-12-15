using BlazorApp.Features.Shared.Models;
using BlazorApp.Features.TravelPlanner.Services;
using BlazorApp.Features.TravelPlanner.Services.Abstractions;

namespace BlazorApp.Features.TravelPlanner.Configuration;

public static class LiveDisplayerConfigurator
{
    private static FeatureInformation featureInformation = new FeatureInformation
    {
        NavMenuItem = new NavMenuItem
        {
            Href = "travelplanner",
            Label = "Travel Planner",
            Icon = "oi oi-dashboard"
        },
        Name = "Travel Planner",
        Description = "Makes it a breeze to get an overview of when the next bus or train leaves."
    };

    public static IServiceCollection ConfigureTravelPlanner(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<ITravelPlanner, SlTravelPlanner>();
        
        services.AddHttpClient("TravelPlanner", c =>
        {
            c.BaseAddress = new Uri("https://api.sl.se/");
        });

        services.Configure<List<FeatureInformation>>(opt =>
        {
            opt.Add(featureInformation);
        });

        return services;
    }
}