
using API.Models.DTO;

namespace API.Data;

public interface IVareService
{
    Task<IEnumerable<Varer>> GetAllAsync();
    Task<Varer> GetByEanAsync(string EAN);
    Task<bool> Create(VareDTO entity);
    Task<bool> Update(Varer entity);
    Task<bool>Update(VareDTO dto);
    void Delete(Varer entity);
}