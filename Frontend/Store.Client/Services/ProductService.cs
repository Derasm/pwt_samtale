using System.Net.Http.Json;
using Store.Shared.DTO;

namespace Store.Client.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BasicVareDTO>> GetAllBasicVare()
    {
       var result = await _httpClient.GetFromJsonAsync<List<BasicVareDTO>>("/products/basic");
       return result ?? [];
        
    }
}