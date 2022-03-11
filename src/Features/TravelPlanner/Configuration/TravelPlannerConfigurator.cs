using BlazorApp.Features.Shared.Models;
using BlazorApp.Features.TravelPlanner.Services;
using BlazorApp.Features.TravelPlanner.Services.Abstractions;
using MudBlazor;

namespace BlazorApp.Features.TravelPlanner.Configuration;

public static class LiveDisplayerConfigurator
{
    private static FeatureInformation travelPlanner = new FeatureInformation
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

    private static FeatureInformation tripViewer = new FeatureInformation
    {
        NavMenuItem = new NavMenuItem
        {
            Href = "tripviewer",
            Label = "Bus Line Viewer",
            Icon = Icons.Filled.DirectionsBus
        },
        Name = "Bus Line Viewer",
        Description = "Where's my bus stop?"
    };

    public static IServiceCollection ConfigureTravelPlanner(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<ITravelPlannerDataClient, SlTravelPlannerDataClient>();
        services.AddTransient<ITravelPlannerRepository, FirestoreTravelPlannerRepository>();
        services.AddTransient<ITravelPlannerService, TravelPlannerService>();
        services.AddSingleton<IStopsAndMapsDataClient, SlStopsAndMapsDataClient>();
        
        services.AddHttpClient("TravelPlanner", c =>
        {
            c.BaseAddress = new Uri("https://api.sl.se/");
        });

        services.Configure<List<FeatureInformation>>(opt =>
        {
            opt.Add(travelPlanner);
            opt.Add(tripViewer);
        });

        return services;
    }
}