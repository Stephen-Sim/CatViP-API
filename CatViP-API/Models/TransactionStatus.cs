using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class TransactionStatus
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
