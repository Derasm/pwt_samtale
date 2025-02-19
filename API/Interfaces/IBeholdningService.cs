using API.Db.Entities;

namespace API.Interfaces;

public interface IBeholdningService
{
    Task<IEnumerable<Beholdning>> GetAllAsync();
    Task<string> GetByEanAsync(string EAN);
    Task<bool> Update(string ean, int stock);
    void Delete(string ean);
}