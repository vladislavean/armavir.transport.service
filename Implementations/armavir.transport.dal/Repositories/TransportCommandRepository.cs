using armavir.transport.dal.Entities;
using armavir.transport.dal.InternalInterfaces;
using AutoMapper;
using dal.abstractions.Models;
using dal.abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace armavir.transport.dal.Repositories;

internal sealed class TransportCommandRepository(
    IModelUpdater modelUpdater,
    IMapper mapper
    ) : ITransportCommandRepository
{
    public async Task CreateTransport(CreateTransportCommandRepositoryModel repositoryModel)
    {
        var model = await modelUpdater.Transports
            .Where(x => x.Number == repositoryModel.Number)
            .AsNoTracking()
            .ToListAsync();

        if (model.Count != 0)
        {
            throw new Exception($"There is already a transport with the number {repositoryModel.Number}");
        }
        
        var entity = mapper.Map<Transports>(repositoryModel);
        await modelUpdater.Transports.AddAsync(entity);
        await modelUpdater.SaveChangesAsync();
    }
}
