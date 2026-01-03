using armavir.transport.dal.InternalInterfaces;
using AutoMapper;
using dal.abstractions.Models;
using dal.abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace armavir.transport.dal.Repositories;

internal sealed class TransportQueryRepository(
    IModelReader modelReader,
    IMapper mapper
    ) : ITransportQueryRepository
{
    public async Task<GetTransportRepositoryModel> GetTransportByIdAsync(Guid id)
    {
        var transport = await modelReader.Transports
            .Where(x => x.Id == id)
            .Include(x => x.TransportStops)
            .FirstOrDefaultAsync();
        var models = transport?.TransportStops.Select(x =>
        {
            return new GetTransportStopsRepositoryModel
            {
                TransportStopId = x.Stop.Id,
                Direction = x.Direction,
                StopOrder = x.StopOrder,
                StopId = x.StopId,
                StopName = x.Stop.Name,
            };
        }).ToList();
        
        var model = mapper.Map<GetTransportRepositoryModel>(transport, x =>
        {
            x.Items[nameof(GetTransportRepositoryModel.Stops)] = models;
        });
        return model;
    }

    public async Task<GetTransportRepositoryModel> GetTransportByNumberAsync(string number)
    {
        var transport = await modelReader.Transports
            .Where(x => x.Number == number)
            .Include(x => x.TransportStops)
            .FirstOrDefaultAsync();
        var models = transport?.TransportStops.Select(x =>
        {
            return new GetTransportStopsRepositoryModel
            {
                TransportStopId = x.Stop.Id,
                Direction = x.Direction,
                StopOrder = x.StopOrder,
                StopId = x.StopId,
                StopName = x.Stop.Name,
            };
        }).ToList();
        
        var model = mapper.Map<GetTransportRepositoryModel>(transport, x =>
        {
            x.Items[nameof(GetTransportRepositoryModel.Stops)] = models;
        });
        return model;
    }
}
