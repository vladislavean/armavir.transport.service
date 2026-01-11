using armavir.transport.dal.Entities;
using armavir.transport.dal.InternalInterfaces;
using dal.abstractions.Repositories;

namespace armavir.transport.dal.Repositories;

internal sealed class StopCommandRepository(
    IModelUpdater modelUpdater
    ) : IStopCommandRepository
{
    public async Task CreateStop(string name)
    {
        var model = new Stops { Id = Guid.NewGuid(), Name = name };
        await modelUpdater.Stops.AddAsync(model);
    }
}
