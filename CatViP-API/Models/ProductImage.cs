using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class ProductImage
{
    public long Id { get; set; }

    public byte[] Image { get; set; } = null!;

    public long ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
