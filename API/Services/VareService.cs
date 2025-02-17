using API.Data;
using API.Models.DTO;
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
        return await _context.Varer.FirstOrDefaultAsync(x => x.EAN == EAN);
    }

    public async Task<bool> Create(VareDTO dto)
    {
        var entity = ToEntity(dto);
        await _context.Varer.AddAsync(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Varer entity)
    {
        var changes = 0;
        //First find
        var foundElement = await _context.Varer.FindAsync(entity.EAN);
        if (foundElement != null)
        {
            //update
            _context.Varer.Update(foundElement);
            changes = await _context.SaveChangesAsync();
        }
        return changes > 0;
    }

    public async Task<bool> Update(VareDTO dto)
    {
        var entity = ToEntity(dto);
        return await Update(entity);
        
    }

    //this should have some kind of safeguard. It doesn't right now, but it definitely should have. 
    public void Delete(Varer entity)
    {
        throw new NotImplementedException();
    }
    #region Mappers
    /// <summary>
    /// Takes Varer entity, returns DTO
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Identical DTO object</returns>
    private VareDTO ToDTO(Varer entity)
    {
        VareDTO dto = new VareDTO()
        {
            SupplierNo = entity.SupplierNo,
            ItemGroupId = entity.ItemGroupId,
            ItemGroupName = entity.ItemGroupName,
            StyleNo = entity.StyleNo,
            ItemDescription = entity.ItemDescription,
            Size = entity.Size,
            Length = entity.length,
            EAN = entity.EAN,
            ColorCodeName = entity.ColorCodeName,
            Season = entity.Season,
            CostPrice = entity.CostPrice,
            CostPriceCurrency = entity.CostPriceCurrency,
            SuggestedRetailPrice = entity.SuggestedRetailPrice,
            IsNOOS = entity.IsNOOS,
            URL = entity.URL,
            egnevarer = Convert.ToBoolean(entity.egnevarer)
            
        };
        return dto;
    }
    /// <summary>
    /// Takes VareDTO object, returns Varer.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Identical Varer object</returns>
    private Varer ToEntity(VareDTO dto)
    {
        var entity = new Varer()
        {
            SupplierNo = dto.SupplierNo,
            ItemGroupId = dto.ItemGroupId,
            ItemGroupName = dto.ItemGroupName,
            StyleNo = dto.StyleNo,
            ItemDescription = dto.ItemDescription,
            Size = dto.Size,
            length = dto.Length,
            EAN = dto.EAN,
            ColorCodeName = dto.ColorCodeName,
            Season = dto.Season,
            CostPrice = dto.CostPrice,
            CostPriceCurrency = dto.CostPriceCurrency,
            SuggestedRetailPrice = dto.SuggestedRetailPrice,
            IsNOOS = dto.IsNOOS,
            URL = dto.URL,
            egnevarer = Convert.ToInt32(dto.egnevarer)
        };
        return entity;
    }
    #endregion
    
    
}