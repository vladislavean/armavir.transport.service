namespace armavir.transport.dal.Entities;

public class Transports
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Number { get; set; }
    public required bool IsActive { get; set; }
    public required string Company { get; set; }
    public required int MaxCount { get; set; }
    public required double ShortRoute { get; set; }
    public required double LongRoute { get; set; }
    
    public virtual ICollection<TransportStops>? Stop { get; set; }
}