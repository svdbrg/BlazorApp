using AutoMapper;
using BlazorApp.Features.Mortgager.Services;

namespace BlazorApp.Features.Mortgager.Configuration;

public static class MortgagerConfigurator
{
    public static IServiceCollection ConfigureMortgager(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MortgageItemProfile());
        });

        var mapper = mapperConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);  

        services.AddTransient<IDataService, GcmDataService>();


        return services;
    }
}