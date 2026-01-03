using armavir.transport.core.Operations;
using armavir.transport.core.Profiles;
using AutoMapper;
using core.abstractions.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace armavir.transport.core;

public static class ServiceConfiguration
{
    public static void ConfigureCore(this IServiceCollection services)
    {
        services.AddScoped<ICommandTransportOperations, CommandTransportOperations>();
        services.AddScoped<IQueryTransportOperations, QueryTransportOperations>();
    }

    public static void ConfigureCoreProfiles(this IMapperConfigurationExpression mc)
    {
        var profiles = typeof(CoreProfiles)
            .Assembly
            .GetTypes()
            .Where(x => typeof(Profile).IsAssignableFrom(x));

        foreach (var profile in profiles)
        {
            mc.AddProfile(Activator.CreateInstance(profile) as Profile);
        }
    }
}