using BlazorApp.Features.Mortgager.Services;
using BlazorApp.Features.Shared;

namespace BlazorApp.Features.Mortgager.Configuration;

public static class MortgagerConfigurator
{
    public static IServiceCollection ConfigureMortgager(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<IDataService, GcmDataService>();

        services.Configure<List<NavMenuItem>>(opt =>
        {
            opt.Add(new NavMenuItem
            {
                Href = "mortgage",
                Label = "Mortgage",
                Icon = "oi oi-list-rich"
            });
        });

        return services;
    }
}