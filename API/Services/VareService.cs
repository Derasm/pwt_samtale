
using API.Data;
using API.Db.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class VareService : IVareService 
{
    private readonly PwtDbContext _context;

    public VareService(PwtDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Varer>> GetAllAsync()
    {
        return await _context.Varer.ToListAsync();
    }

    public async Task<Varer?> GetByEanAsync(string EAN)
    {
        var product =  await _context.Varer.FirstOrDefaultAsync(x => x.EAN == EAN);
        return product;
    }

    public async Task<bool> Update(Varer entity)
    {
        var changes = 0;
        //First find
        var foundElement = await _context.Varer.FindAsync(entity.EAN);
        if (foundElement != null)
        {
            //update - dirty method, leaves all implementation of update logic to EF. For non-complex objects, is ok
            foundElement = entity;
            _context.Varer.Update(foundElement);
            changes = await _context.SaveChangesAsync();
        }
        return changes > 0;
    }

    //this should have some kind of safeguard. It doesn't right now, but it definitely should have. 
    public void Delete(Varer entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Create(Varer entity)
    {
        await _context.Varer.AddAsync(entity);
        var changes = await _context.SaveChangesAsync();
        return changes > 0;
    }
}