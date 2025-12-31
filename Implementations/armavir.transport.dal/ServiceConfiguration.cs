using armavir.transport.dal.InternalInterfaces;
using armavir.transport.dal.Profiles;
using AutoMapper;
using dal.abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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

        services.AddScoped<ITransportCommandRepository, ITransportCommandRepository>();
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
    
    public static void AddDalProfiles(this IMapperConfigurationExpression config)
    {
        config.AddProfile<DalMapperProfiles>();
    }
}