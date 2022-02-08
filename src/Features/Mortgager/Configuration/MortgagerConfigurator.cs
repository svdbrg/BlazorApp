using BlazorApp.Features.Shared.Models;
using MudBlazor;
using BlazorApp.Features.Mortgager.Services.Abstractions;
using BlazorApp.Features.Mortgager.Services;

namespace BlazorApp.Features.Mortgager.Configuration;

public static class MortgagerConfigurator
{
    private static FeatureInformation featureInformation = new FeatureInformation
    {
        NavMenuItem = new NavMenuItem
        {
            Href = "mortgage",
            Label = "Mortgage",
            Icon = Icons.Filled.AttachMoney
        },
        Name = "Mortgage",
        Description = "A tool to calculate mortgage costs based on purchase price, down payment yearly salary etc."
    };

    public static IServiceCollection ConfigureMortgager(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<List<FeatureInformation>>(opt =>
        {
            opt.Add(featureInformation);
        });

        services.AddTransient<IMortgageDataService, FirestoreMortgageDataService>();

        return services;
    }
}