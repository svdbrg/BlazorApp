using BlazorApp.Features.Shared.Services;
using BlazorApp.Features.Shared.Services.Abstractions;

namespace BlazorApp.Features.Shared;

public static class SharedConfigurator
{
    public static IServiceCollection ConfigureSharedServices(this IServiceCollection services)
    {
        services.AddTransient<ILocalStorage, LocalStorage>();
        services.AddTransient<IDataService, GcmDataService>();

        return services;
    }
}