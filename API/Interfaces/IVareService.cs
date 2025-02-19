
using API.Db.Entities;

namespace API.Interfaces;

public interface IVareService
{
    Task<IEnumerable<Varer>> GetAllAsync();
    Task<Varer?> GetByEanAsync(string EAN);
    Task<bool> Update(Varer entity);
    void Delete(Varer entity);
    Task<bool> Create(Varer entity);
}