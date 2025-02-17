using System;
using System.Collections.Generic;

namespace API.Data;

public partial class ShopInventory
{
    public int HOVED_Ident { get; set; }

    public int IDENT { get; set; }

    public string VARENUMMER { get; set; } = null!;

    public decimal? PRIS { get; set; }
}
