using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class InviteReviewer34
{
    public string JournalConferenceName { get; set; } = null!;

    public string ArticleTitle { get; set; } = null!;

    public string ServiceApplicant { get; set; } = null!;

    public DateOnly ProjectStartDate { get; set; }

    public int AcadYear { get; set; }

    /// <summary>
    /// A = Active, N = Non Active
    /// </summary>
    public string Status { get; set; } = null!;

    public string ApprovedEmpId { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public int Id { get; set; }

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
