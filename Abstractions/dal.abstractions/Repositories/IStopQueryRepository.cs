using dal.abstractions.Models;

namespace dal.abstractions.Repositories;

public interface IStopQueryRepository
{
    Task<StopRepositoryModel?> GetStopByName(string name);
}
