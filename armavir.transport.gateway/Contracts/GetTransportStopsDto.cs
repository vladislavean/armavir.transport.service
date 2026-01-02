namespace armavir.transport.gateway.Contracts;

public sealed record GetTransportStopsDto
{
    public required Guid TransportStopId { get; init; }
    public required DirectionDtoEnum Direction { get; init; }
    public required int StopOrder { get; init; }
    public required Guid StopId { get; init; }
    public required string StopName { get; init; }
}
