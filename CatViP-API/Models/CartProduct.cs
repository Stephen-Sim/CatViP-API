using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class CartProduct
{
    public long Id { get; set; }

    public int Quantity { get; set; }

    public long CartId { get; set; }

    public long ProductId { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
