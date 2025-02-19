using API.Db.Entities;
using API.Interfaces;
using Azure.Core;
using Store.Shared.DTO;

namespace API.Services;

public class VareDTOService : IVareDTOService
{
    private readonly IVareService _vareService;
    private readonly IBeholdningService _beholdningService;
    private readonly ILogger<VareDTOService> _logger;

    public VareDTOService(IVareService vareService, IBeholdningService beholdningService, ILogger<VareDTOService> logger)
    {
        _vareService = vareService;
        _beholdningService = beholdningService;
        _logger = logger;
    }
    /// <summary>
    /// Returns all Varer as FullVareDTO with quantities. 
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<FullVareDTO>> GetAllFullWithStockAsync()
    {
        var products = await _vareService.GetAllAsync();
        var quantities = await _beholdningService.GetAllAsync();
        //Converting stock to dict for faster lookup as we dont need anything but ean and quant
        //key is ean, value is quant
        //There are duplicates in the Beholdning table, so we sum up the duplicates and add them in where they fit. 
        var stockDict = quantities.GroupBy(s =>s.ean).
            ToDictionary(g => g.Key, 
                g => g.Sum(s => s.InventoryQuantity));
        //we loop over products, for each product we call ToFUllVareDto, then we change the Quantity of the product to the matching one from dict. 
        var result = products.Select(varer =>
        {
            var dto = ToFullVareDto(varer);
            dto.Quantity = stockDict.GetValueOrDefault(varer.EAN, 0);
            return dto;
        });
        return result;
    }

    public async Task<IEnumerable<BasicVareDTO>> GetAllBasicWithStockAsync()
    {
        var products = await _vareService.GetAllAsync();
        var quantities = await _beholdningService.GetAllAsync();
        
        var stockDict = quantities.GroupBy(s =>s.ean).
            ToDictionary(g => g.Key, 
                g => g.Sum(s => s.InventoryQuantity));
        
        var result = products.Select(varer =>
        {
            BasicVareDTO dto = ToBasicVareDto(varer);
            //Make a match with dict on ean, return as int if found else return 0
            dto.Quantity = stockDict.GetValueOrDefault(varer.EAN, 0);
            dto.ItemGroupName = varer.ItemGroupName;
            return dto;
        });
        return result;
    }
    
    public async Task<FullVareDTO?> GetFullWithStockAsync(string ean)
    {
        var product = await _vareService.GetByEanAsync(ean);
        var quantity = await _beholdningService.GetByEanAsync(ean);
        if (product == null ||string.IsNullOrEmpty(quantity))
        {
            return null;
        }
        var dto = ToFullVareDto(product);
        dto.Quantity = Convert.ToInt32(quantity);
        return dto;
    }

    public async Task<BasicVareDTO?> GetBasicWithStockAsync(string ean)
    {
        var product = await _vareService.GetByEanAsync(ean);
        var quantity = await _beholdningService.GetByEanAsync(ean);
        if (product == null ||string.IsNullOrEmpty(quantity))
        {
            return null;
        }
        var dto = ToBasicVareDto(product);
        dto.Quantity = Convert.ToInt32(quantity);
        return dto;
    }

    public async Task<FullVareDTO?> GetByEanAsync(string EAN)
    {
        var product = await _vareService.GetByEanAsync(EAN);
        if (product == null)
        {

            return null;
        }
        return ToFullVareDto(product);
    }
    /// <summary>
    /// Create a new Varer (Product) from the FullVareDTO.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> Create(FullVareDTO entity)
    {
        //convert to regular Vare, insert using _vareService
        var vare = ToEntity(entity);
        var changes = await _vareService.Create(vare);
        return changes;
    }
    
    
    #region Mappers
    
    /// <summary>
    /// Takes VareDTO object, returns Varer.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Identical Varer object</returns>
    public Varer ToEntity(FullVareDTO dto)
    {
        var entity = new Varer()
        {
            SupplierNo = Random.Shared.Next(0, 100), //Just fake data. Would normally be done through a mapping to a lookup table
            //ItemGroupId = dto.ItemGroupId, //would also be set from lookup table
            ItemGroupName = dto.ItemGroupName,
            //StyleNo =  _context.Varer.FindAsync(dto.ItemGroupId).Result.StyleNo, - again, lookup table. 
            ItemDescription = dto.ItemDescription,
            Size = dto.Size,
            length = dto.Length,
            EAN = "1234567891011", // This would be done with lookup on EAN db or similar
            ColorCodeName = dto.ColorCodeName,
            Season = dto.Season,
            CostPrice = dto.CostPrice,
            CostPriceCurrency = dto.CostPriceCurrency,
            SuggestedRetailPrice = dto.SuggestedRetailPrice,
            IsNOOS = 0,
            URL = null,
            egnevarer = Convert.ToInt32(dto.Egnevarer)
        };
        return entity;
    }
    
    public BasicVareDTO ToBasicVareDto(Varer varer)
    {
        return new BasicVareDTO
        {
            EAN = varer.EAN,
            ItemDescription = varer.ItemDescription,
            SuggestedRetailPrice = varer.SuggestedRetailPrice
        };
    }

    public  FullVareDTO ToFullVareDto(Varer varer)
    {
        return new FullVareDTO
        {
            SupplierNo = varer.SupplierNo,
            ItemGroupId = varer.ItemGroupId,
            ItemGroupName = varer.ItemGroupName,
            StyleNo = varer.StyleNo,
            ItemDescription = varer.ItemDescription,
            Size = varer.Size,
            Length = varer.length,
            EAN = varer.EAN,
            ColorCodeName = varer.ColorCodeName,
            Season = varer.Season,
            CostPrice = varer.CostPrice,
            CostPriceCurrency = varer.CostPriceCurrency,
            SuggestedRetailPrice = varer.SuggestedRetailPrice,
            IsNOOS = varer.IsNOOS,
            URL = varer.URL,
            Egnevarer = varer.egnevarer
        };
    }


    #endregion
}