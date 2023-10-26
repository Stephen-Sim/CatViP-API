using System;
using System.Collections.Generic;

namespace CatViP_API.Models;

public partial class CatCaseReport
{
    public long Id { get; set; }

    public string Description { get; set; } = null!;

    public long UserId { get; set; }

    public long? CatId { get; set; }

    public long CatCaseReportTypeId { get; set; }

    public virtual Cat? Cat { get; set; }

    public virtual ICollection<CatCaseReportImage> CatCaseReportImages { get; set; } = new List<CatCaseReportImage>();

    public virtual CatCaseReportType CatCaseReportType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
