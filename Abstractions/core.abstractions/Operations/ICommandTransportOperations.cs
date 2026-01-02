using core.abstractions.Models;

namespace core.abstractions.Operations;

public interface ICommandTransportOperations
{
    Task<Result> CreateTransportAsync(CreateTransportOperationModel createTransportOperationModel);
    Task<Result> DeleteTransport(Guid id);
}
