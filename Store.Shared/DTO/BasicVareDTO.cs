namespace Store.Shared.DTO;

public class BasicVareDTO
{
    public string? EAN { get; set; }
    public string? ItemDescription { get; set; }
    public decimal? SuggestedRetailPrice { get; set; }
    public int? Quantity { get; set; }
    public string? ItemGroupName { get; set; }
}