using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO;

public class VareDTO
{
    public int? SupplierNo { get; set; }

    public int? ItemGroupId { get; set; }

    public string? ItemGroupName { get; set; }

    public string? StyleNo { get; set; }
    [Required]
    
    public string? ItemDescription { get; set; }
    [Required]
    [RegularExpression("^(2XS|XS|S|M|L|XL|2XL|3XL|4XL|5XL)$", 
        ErrorMessage = "Invalid size. Allowed values: 2XS, XS, S, M, L, XL, 2XL, 3XL, 4XL, 5XL.")]
    public string? Size { get; set; }

    public string? Length { get; set; }

    public string? EAN { get; set; }

    public string? ColorCodeName { get; set; }

    public string? Season { get; set; }

    public decimal? CostPrice { get; set; }

    public string? CostPriceCurrency { get; set; }

    public decimal? SuggestedRetailPrice { get; set; }

    public int? IsNOOS { get; set; }

    public string? URL { get; set; }

    public bool? egnevarer { get; set; } 
}