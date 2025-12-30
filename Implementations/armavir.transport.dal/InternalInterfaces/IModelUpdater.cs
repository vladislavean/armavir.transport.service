using armavir.transport.dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace armavir.transport.dal.InternalInterfaces;

internal interface IModelUpdater
{
    IDbContextTransaction CurrentTransaction { get; set; }
    DatabaseFacade Database { get; }
    
    DbSet<Transports> Transports { get; set; }
    DbSet<Stops> Stops { get; set; }
    DbSet<TransportStops> TransportStops { get; set; }
    
    Task<int> SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    
}
