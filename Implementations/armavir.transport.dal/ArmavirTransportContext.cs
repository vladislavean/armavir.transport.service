using armavir.transport.dal.Entities;
using armavir.transport.dal.InternalInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace armavir.transport.dal;

public class ArmavirTransportContext : DbContext, IModelReader, IModelUpdater
{
    public const string ConnectionDataBase = "ArmavirTransportCN";
    public const string AdminDbName = "AdminDbName";
    
    public ArmavirTransportContext()
    {
        
    }

    public ArmavirTransportContext(DbContextOptions<ArmavirTransportContext> options) : base(options)
    {
        
    }
    
    IDbContextTransaction IModelUpdater.CurrentTransaction { get; set; }
    DatabaseFacade IModelUpdater.Database => Database;

    public virtual DbSet<Transports> Transports { get; set; }
    public virtual DbSet<Stops> Stops { get; set; }
    public virtual DbSet<TransportStops> TransportStops { get; set; }
    
    IQueryable<Transports> IModelReader.Transports => Transports
        .Include(x => x.TransportStops)
        .AsNoTracking();
    
    IQueryable<TransportStops> IModelReader.TransportStops => TransportStops
        .Include(x => x.Stop)
        .Include(x => x.Transport)
        .AsNoTracking();
    
    IQueryable<Stops> IModelReader.Stops => Stops
        .Include(x => x.TransportStops)
        .AsNoTracking();
    
    Task<int> IModelUpdater.SaveChangesAsync()
    {
        return SaveChangesAsync();
    }
    
    Task<int> IModelUpdater.SaveChangesAsync(CancellationToken cancellationToken)
    {
        return SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArmavirTransportContext).Assembly);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) // для запуска создания миграций
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..",
                    "armavir.transport.gateway"))
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var connectionString = configuration.GetConnectionString(ConnectionDataBase);
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseNpgsql(connectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }
}
