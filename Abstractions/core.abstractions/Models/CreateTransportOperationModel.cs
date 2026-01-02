namespace core.abstractions.Models;

public sealed record CreateTransportOperationModel
{
    public required string Name { get; init; }
    public required string Number { get; init; }
    public required bool IsActive { get; init; }
    public required string Company { get; init; }
    public required int MaxCount { get; init; }
    public required double ShortRoute { get; init; }
    public required double LongRoute { get; init; }
}
