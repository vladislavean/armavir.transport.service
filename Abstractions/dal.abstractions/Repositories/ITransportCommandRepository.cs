using dal.abstractions.Models;

namespace dal.abstractions.Repositories;

public interface ITransportCommandRepository
{
    Task CreateTransport(CreateTransportCommandRepositoryModel repositoryModel);
    Task DeleteTransport(Guid id);
    Task CreateTransportBatchAsync(ICollection<CreateTransportCommandRepositoryModel> repositoryModel);
}
