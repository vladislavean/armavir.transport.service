using AutoMapper;
using core.abstractions;
using core.abstractions.Models;
using core.abstractions.Operations;
using dal.abstractions.Models;
using dal.abstractions.Repositories;

namespace armavir.transport.core.Operations;

internal sealed class CommandTransportOperations(
    ITransportCommandRepository transportCommandRepository,
    IMapper mapper
    ) 
    : ICommandTransportOperations
{
    public async Task<Result> CreateTransportAsync(CreateTransportOperationModel createTransportOperationModel)
    {
        var repositoryModel = mapper.Map<CreateTransportCommandRepositoryModel>(createTransportOperationModel); 
        await transportCommandRepository.CreateTransport(repositoryModel);
        return Result.Success();
    }

    public async Task<Result> DeleteTransport(Guid id)
    {
        await transportCommandRepository.DeleteTransport(id);
        return Result.Success();
    }
}
