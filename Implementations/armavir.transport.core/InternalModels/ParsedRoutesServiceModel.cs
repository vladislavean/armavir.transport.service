namespace armavir.transport.core.InternalModels;

internal sealed record ParsedRoutesServiceModel
{
    public required string RouteNumber { get; init; }
    public required string Company { get; init; }
    public required string RouteName { get; init; }
    public required string RouteForward { get; init; }
    public required string RouteBackward { get; init; }
    public required string RouteInKm { get; init; }
    public required string MaxTransportCount { get; init; }
}
