using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Description { get; set; } = null!;

    public long SellerId { get; set; }

    public long ProductTypeId { get; set; }

    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ProductType ProductType { get; set; } = null!;

    public virtual User Seller { get; set; } = null!;
}
