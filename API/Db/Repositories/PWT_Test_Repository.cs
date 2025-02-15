using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class PWT_Test_Repository<T> : IGenericRepositoryInterface<T> where T : class
{
    protected readonly PwtDbContext _context;
    private readonly DbSet<T> _dbSet;
    
    public PWT_Test_Repository(PwtDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
    /// <summary>
    /// Using Async here as EF will block the thread otherwise.
    /// This also allows Unit of Work pattern by allowing batch commits.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}