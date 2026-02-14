using armavir.transport.dal.InternalInterfaces;
using dal.abstractions.Models;
using dal.abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace armavir.transport.dal.Repositories;

internal sealed class StopQueryRepository(
    IModelReader modelReader
    ) : IStopQueryRepository
{
    public async Task<StopRepositoryModel?> GetStopByName(string name)
    {
        var stopEntity = await modelReader.Stops.Where(x => x.Name == name)
            .FirstOrDefaultAsync();
        return stopEntity != null ? 
            new StopRepositoryModel() { Id = stopEntity.Id, Name = stopEntity.Name } 
            : null;
    }

    public async Task<ICollection<StopRepositoryModel>?> GetStopsByNamesBatchAsync(ICollection<string> name)
    {
        var stopEntites = await modelReader.Stops
            .Where(x => name.Contains(x.Name))
            .ToListAsync();

        return stopEntites
            .Select(x => new StopRepositoryModel() { Id = x.Id, Name = x.Name })
            .ToList();
    }
}
