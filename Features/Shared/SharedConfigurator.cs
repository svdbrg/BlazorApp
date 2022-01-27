using BlazorApp.Features.Shared.Services;
using BlazorApp.Features.Shared.Models;
using BlazorApp.Features.Shared.Services.Abstractions;
using AutoMapper;

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

public class AuthenticationProfile : Profile
{
    public AuthenticationProfile()
    {
        CreateMap<AuthenticationDto, Authentication>();
        CreateMap<Authentication, AuthenticationDto>();
    }
}