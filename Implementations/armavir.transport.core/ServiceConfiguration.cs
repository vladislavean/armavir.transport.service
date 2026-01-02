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
    }

    public static void ConfigureCoreMapper(this IMapperConfigurationExpression config)
    {
        config.AddProfile<CoreProfiles>();
    }
}