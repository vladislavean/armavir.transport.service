using armavir.transport.dal.InternalInterfaces;
using armavir.transport.dal.Profiles;
using armavir.transport.dal.Repositories;
using AutoMapper;
using dal.abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace armavir.transport.dal;

public static class ServiceConfiguration
{
    public static void AddDal(this IServiceCollection services, IConfiguration configuration, bool inMemory = false,
        string? databaseName = null)
    {
        if (inMemory)
        {
            services.AddDbContext<ArmavirTransportContext>(options => options
                .UseInMemoryDatabase(databaseName: databaseName ?? nameof(ArmavirTransportContext)));
        }
        else
        {
            services.AddDbContext<ArmavirTransportContext>(options => options
                .UseLazyLoadingProxies()
                .UseNpgsql(configuration.GetConnectionString(ArmavirTransportContext.ConnectionDataBase),
                    sql =>
                    {
                        sql.CommandTimeout(240);
                        sql.UseAdminDatabase(configuration.GetConnectionString(ArmavirTransportContext.AdminDbName));
                    }));
        }
        
        services.AddScoped<IModelReader>(x =>
        {
            var context = x.GetRequiredService<ArmavirTransportContext>();
            return context;
        });

        services.AddScoped<IModelUpdater>(x =>
        {
            var context = x.GetRequiredService<ArmavirTransportContext>();
            return context;
        });
        
        // repositories

        services.AddScoped<ITransportCommandRepository, TransportCommandRepository>();
        services.AddScoped<ITransportQueryRepository, TransportQueryRepository>();
    }
    
    public static void MigrateDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ArmavirTransportContext>();
        context.Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
        try
        {
            context.Database.Migrate();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public static void ConfigureDalMapperProfiles(this IMapperConfigurationExpression mc)
    {
        var profiles = typeof(DalMapperProfiles)
            .Assembly
            .GetTypes()
            .Where(x => typeof(Profile).IsAssignableFrom(x));

        foreach (var profile in profiles)
        {
            mc.AddProfile(Activator.CreateInstance(profile) as Profile);
        }
    }
}