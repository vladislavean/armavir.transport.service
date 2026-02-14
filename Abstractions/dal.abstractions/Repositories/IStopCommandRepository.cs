namespace dal.abstractions.Repositories;

public interface IStopCommandRepository
{ 
    Task CreateStop(string name);
    Task CreateStopBatchAsync(IEnumerable<string> names);
}