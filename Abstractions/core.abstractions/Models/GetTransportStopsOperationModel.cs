namespace core.abstractions.Models;

public sealed record GetTransportStopsOperationModel
{
    public required Guid TransportStopId { get; init; }
    public required DirectionOperationEnum Direction { get; init; }
    public required int StopOrder { get; init; }
    public required Guid StopId { get; init; }
    public required string StopName { get; init; }
}
