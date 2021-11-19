using System.Reflection;
using AutoMapper;

public static class AutoMapperConfiguration
{
    public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        var profiles = Assembly.GetExecutingAssembly().GetTypes().Where(a => typeof(Profile).IsAssignableFrom(a));
        var mapperConfig = new MapperConfiguration(mc =>
        {
            foreach (var item in profiles)
            {
                mc.AddProfile(Activator.CreateInstance(item) as Profile);
            }
        });

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}