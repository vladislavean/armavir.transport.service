using dal.abstractions.Models;

namespace dal.abstractions.Repositories;

public interface ITransportQueryRepository
{
    Task<GetTransportRepositoryModel> GetTransportByIdAsync(Guid id);
    Task<GetTransportRepositoryModel> GetTransportByNumberAsync(string number);
}
