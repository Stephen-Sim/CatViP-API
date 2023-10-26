using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class PostReport
{
    public long Id { get; set; }

    public byte[] Description { get; set; } = null!;

    public long StatusId { get; set; }

    public DateTime DateTime { get; set; }

    public long UserId { get; set; }

    public virtual PostReportStatus Status { get; set; } = null!;
}
