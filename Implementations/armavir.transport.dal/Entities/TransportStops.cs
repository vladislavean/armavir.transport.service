using dal.abstractions.Models;

namespace armavir.transport.dal.Entities;

public class TransportStops
{
    public required DirectionEnum Direction { get; set; }
    public required int StopOrder { get; set; }
    
    public required Guid TransportId { get; set; }
    public required Guid StopId { get; set; }
    
    public virtual Transports? Transport { get; set; }
    public virtual Stops? Stop { get; set; }
}
