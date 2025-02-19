using API.Data;
using API.Db.Entities;
using Store.Shared.DTO;

namespace API.Interfaces;

public interface IVareDTOService
{
    Task<IEnumerable<FullVareDTO>> GetAllFullWithStockAsync();
    Task<IEnumerable<BasicVareDTO>> GetAllBasicWithStockAsync();
    Task<FullVareDTO?> GetFullWithStockAsync(string ean);
    Task<BasicVareDTO?> GetBasicWithStockAsync(string ean);
    Task<FullVareDTO?> GetByEanAsync(string EAN);
    Task<bool> Create(FullVareDTO entity);
    Varer ToEntity(FullVareDTO dto);
    BasicVareDTO ToBasicVareDto(Varer varer);
    FullVareDTO ToFullVareDto(Varer varer);
}