using core.abstractions.Options;

namespace armavir.transport.gateway.Application;

public static class OptionsConfiguration
{
    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TransportPageOptions>(configuration.GetSection("TransportPageOptions"));
    }
}
