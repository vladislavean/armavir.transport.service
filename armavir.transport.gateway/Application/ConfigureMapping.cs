using armavir.transport.core;
using armavir.transport.dal;
using armavir.transport.gateway.Profiles;
using AutoMapper;

namespace armavir.transport.gateway.Application;

public static class ConfigureMapping
{
    public static void ConfigureMapper(this IServiceCollection services)
    {
        
        var mapperConfig = new MapperConfiguration(mc =>
        {
            //mc.ShouldMapMethod = m => false;
            mc.ConfigureGatewayProfiles();
            mc.ConfigureCoreProfiles();
            mc.ConfigureDalMapperProfiles();
        });
        mapperConfig.AssertConfigurationIsValid();
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
    
    private static void ConfigureGatewayProfiles(this IMapperConfigurationExpression mc)
    {
        var profiles = typeof(GatewayMapperProfile)
            .Assembly
            .GetTypes()
            .Where(x => typeof(Profile).IsAssignableFrom(x));

        foreach (var profile in profiles)
        {
            mc.AddProfile(Activator.CreateInstance(profile) as Profile);
        }
    }
}