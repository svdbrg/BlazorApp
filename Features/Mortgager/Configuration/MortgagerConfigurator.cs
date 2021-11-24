using BlazorApp.Features.Mortgager.Services;
using BlazorApp.Features.Shared;

namespace BlazorApp.Features.Mortgager.Configuration;

public static class MortgagerConfigurator
{
    private static NavMenuItem MenuItem = new NavMenuItem
    {
        Href = "mortgage",
        Label = "Mortgage",
        Icon = "oi oi-list-rich"
    };

    public static IServiceCollection ConfigureMortgager(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<IDataService, GcmDataService>();

        services.Configure<List<NavMenuItem>>(opt =>
        {
            opt.Add(MenuItem);
        });

        return services;
    }
}