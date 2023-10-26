using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class CatCaseReportType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CatCaseReport> CatCaseReports { get; set; } = new List<CatCaseReport>();
}
