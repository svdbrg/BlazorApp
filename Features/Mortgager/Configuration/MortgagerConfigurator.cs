using BlazorApp.Features.Mortgager.Services;

namespace BlazorApp.Features.Mortgager.Configuration;

public static class MortgagerConfigurator
{
    public static IServiceCollection ConfigureMortgager(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<IDataService, GcmDataService>();

        return services;
    }
}