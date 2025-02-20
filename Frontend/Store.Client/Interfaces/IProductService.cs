using Store.Shared.DTO;

namespace Store.Client.Services;

public interface IProductService
{
    Task<List<BasicVareDTO>> GetAllBasicVare();
}