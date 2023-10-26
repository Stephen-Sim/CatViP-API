using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class PostReportStatus
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PostReport> PostReports { get; set; } = new List<PostReport>();
}
