using AutoMapper;
using core.abstractions;
using core.abstractions.Models;
using core.abstractions.Operations;
using dal.abstractions.Repositories;

namespace armavir.transport.core.Operations;

internal sealed class QueryTransportOperations(
    ITransportQueryRepository transportQueryRepository,
    IMapper mapper
    ) 
    : IQueryTransportOperations
{
    public async Task<Result<GetTransportOperationModel>> GetTransportByIdAsync(Guid id)
    {
        var result = await transportQueryRepository.GetTransportByIdAsync(id);
        if (result == null)
        {
            return Error.Failure("No transport found with id: " + id);
        }

        var operationModel = mapper.Map<GetTransportOperationModel>(result);
        return operationModel;
    }

    public async Task<Result<GetTransportOperationModel>> GetTransportByNumberAsync(string number)
    {
        throw new NotImplementedException();
    }
}
