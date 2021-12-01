using BlazorApp.Features.Mortgager.Services;
using BlazorApp.Features.Mortgager.Services.Abstractions;
using BlazorApp.Features.Shared.Models;
using Blazored.Modal;

namespace BlazorApp.Features.Mortgager.Configuration;

public static class MortgagerConfigurator
{
    private static FeatureInformation featureInformation = new FeatureInformation
    {
        NavMenuItem = new NavMenuItem
        {
            Href = "mortgage",
            Label = "Mortgage",
            Icon = "oi oi-list-rich"
        },
        Name = "Mortgage",
        Description = "A tool to calculate mortgage costs based on purchase price, down payment yearly salary etc."
    };

    public static IServiceCollection ConfigureMortgager(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<IDataService, GcmDataService>();
        services.AddTransient<ILocalStorage, LocalStorage>();
        services.AddBlazoredModal();

        services.Configure<List<FeatureInformation>>(opt =>
        {
            opt.Add(featureInformation);
        });

        return services;
    }
}