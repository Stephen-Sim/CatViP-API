using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class Cart
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long? TransactionId { get; set; }

    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    public virtual Transaction? Transaction { get; set; }

    public virtual User User { get; set; } = null!;
}
