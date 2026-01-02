namespace dal.abstractions.Models;

public sealed record GetTransportRepositoryModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Number { get; init; }
    public required bool IsActive { get; init; }
    public required string Company { get; init; }
    public required int MaxCount { get; init; }
    public required double ShortRoute { get; init; }
    public required double LongRoute { get; init; }
    public required List<GetTransportStopsRepositoryModel> Stops { get; init; }
}