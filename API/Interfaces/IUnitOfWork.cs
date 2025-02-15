namespace API.Data;

public interface IUnitOfWork : IDisposable
{
    IGenericRepositoryInterface<T> Repository<T>() where T : class;
    Task<bool> CompleteAsync();
}