using BlazorApp.Features.Shared.Services;
using BlazorApp.Features.Shared.Services.Abstractions;
using BlazorApp.Features.Shared.Models;
using MudBlazor;

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

        return services;
    }
}