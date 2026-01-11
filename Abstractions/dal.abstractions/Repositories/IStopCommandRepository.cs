namespace dal.abstractions.Repositories;

public interface IStopCommandRepository
{
    public Task CreateStop(string name);
}