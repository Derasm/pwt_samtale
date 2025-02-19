using API.Data;
using API.Db.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class BeholdningService : IBeholdningService
{
    private readonly PwtDbContext _context;

    public BeholdningService(PwtDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Beholdning>> GetAllAsync()
    {
        return await _context.Beholdning.ToListAsync();
    }

    public async Task<string> GetByEanAsync(string EAN)
    {
        var item = await _context.Beholdning.SingleOrDefaultAsync(x => x.ean == EAN);
        return item?.ean;
    }

    public async Task<bool> Update(string ean, int stock)
    {
        var changes = 0;
        //First find
        var foundElement = await _context.Beholdning.FindAsync(ean);
        if (foundElement != null)
        {
            foundElement.InventoryQuantity = stock;
            //update
            _context.Beholdning.Update(foundElement);
            changes = await _context.SaveChangesAsync();
        }
        return changes > 0;
    }
    [Obsolete("Do not delete rows from the code. Use SQL scripts that can be tracked, like Liquibase.")]
    public void Delete(string ean)
    {
        throw new NotImplementedException();
    }
}