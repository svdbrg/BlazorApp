using BlazorApp.Features.Shared.Models;
using BlazorApp.Features.TravelPlanner.Services;
using BlazorApp.Features.TravelPlanner.Services.Abstractions;
using MudBlazor;

namespace BlazorApp.Features.TravelPlanner.Configuration;

public static class LiveDisplayerConfigurator
{
    private static FeatureInformation featureInformation = new FeatureInformation
    {
        NavMenuItem = new NavMenuItem
        {
            Href = "travelplanner",
            Label = "Travel Planner",
            Icon = Icons.Filled.TravelExplore
        },
        Name = "Travel Planner",
        Description = "Makes it a breeze to get an overview of when the next bus or train leaves."
    };

    public static IServiceCollection ConfigureTravelPlanner(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<ITravelPlannerDataClient, SlTravelPlannerDataClient>();
        services.AddTransient<ITravelPlannerRepository, FirebaseTravelPlannerRepository>();
        services.AddTransient<ITravelPlannerService, TravelPlannerService>();
        
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