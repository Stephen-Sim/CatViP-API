using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class Transaction
{
    public long Id { get; set; }

    public DateTime DateTime { get; set; }

    public long StatusId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual TransactionStatus Status { get; set; } = null!;
}
