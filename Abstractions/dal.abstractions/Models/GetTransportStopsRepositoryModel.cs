namespace dal.abstractions.Models;

public sealed record GetTransportStopsRepositoryModel
{
    public required Guid TransportStopId { get; init; }
    public required DirectionEnum Direction { get; init; }
    public required int StopOrder { get; init; }
    public required Guid StopId { get; init; }
    public required string StopName { get; init; }
}
