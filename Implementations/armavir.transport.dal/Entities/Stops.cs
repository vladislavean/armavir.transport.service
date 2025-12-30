namespace armavir.transport.dal.Entities;

public class Stops
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    
    public virtual ICollection<TransportStops>? TransportStops { get; set; }
}
