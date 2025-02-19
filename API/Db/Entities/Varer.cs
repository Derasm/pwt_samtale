﻿namespace API.Db.Entities;

public partial class Varer
{
    public int? SupplierNo { get; set; }

    public int? ItemGroupId { get; set; }

    public string? ItemGroupName { get; set; }

    public string? StyleNo { get; set; }

    public string? ItemDescription { get; set; }

    public string? Size { get; set; }

    public string? length { get; set; }

    public string? EAN { get; set; }

    public string? ColorCodeName { get; set; }

    public string? Season { get; set; }

    public decimal? CostPrice { get; set; }

    public string? CostPriceCurrency { get; set; }

    public decimal? SuggestedRetailPrice { get; set; }

    public int? IsNOOS { get; set; }

    public string? URL { get; set; }

    public int? egnevarer { get; set; }
}
