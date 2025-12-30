using armavir.transport.dal.Entities;

namespace armavir.transport.dal.InternalInterfaces;

internal interface IModelReader
{
    IQueryable<Transports> Transports { get; }
    IQueryable<Stops> Stops { get; }
    IQueryable<TransportStops> TransportStops { get; }
}
