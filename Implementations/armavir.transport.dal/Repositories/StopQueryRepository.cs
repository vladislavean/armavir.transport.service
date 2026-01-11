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
}
