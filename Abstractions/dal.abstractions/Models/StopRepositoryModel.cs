namespace dal.abstractions.Models;

public sealed record StopRepositoryModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
