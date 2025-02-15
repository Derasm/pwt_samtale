namespace API.Data;

/// <summary>
/// Since I went with a generic Repository for each DB instead of dedicated per table, generic Unit of Work is better.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly PwtDbContext _context;
    private readonly Dictionary<Type, Object> _repositories = new(); // to keep a single instance of each repository.

    public UnitOfWork(PwtDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Returns a single instance of the specified entity type
    /// This ensures there's only a single instance of each of the repositories.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <returns>specified repository</returns>
    public IGenericRepositoryInterface<T> Repository<T>() where T : class
    {
        //If the _repositories does not contain a Repo with the given type (Varer,Beholdning etc), add it. 
        if (!_repositories.ContainsKey(typeof(T)))
        {
            _repositories[typeof(T)] = new PWT_Test_Repository<T>(_context);
        }
        //return the specified repository.
        return (IGenericRepositoryInterface<T>)_repositories[typeof(T)];
    }
    /// <summary>
    /// Saves all pending changes across all repositories
    /// </summary>
    /// <returns>bool</returns>
    public async Task<bool> CompleteAsync()
    {
        //simple int to bool conversion.
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}