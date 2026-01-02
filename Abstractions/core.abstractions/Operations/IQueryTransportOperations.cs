using core.abstractions.Models;

namespace core.abstractions.Operations;

public interface IQueryTransportOperations
{
    Task<Result<GetTransportOperationModel>> GetTransportByIdAsync(Guid id);
    Task<Result<GetTransportOperationModel>> GetTransportByNumberAsync(string number);
}
