using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace API.Data;

public partial class ProductData
{
    public int IDENT { get; set; }

    public string Kampagnenavn { get; set; } = null!;

    public string? Valutakode { get; set; }

    public string KAEDE { get; set; } = null!;

    public DateTime? FRADATO { get; set; }

    public DateTime? TILDATO { get; set; }
}
