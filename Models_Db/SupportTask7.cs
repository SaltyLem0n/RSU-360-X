using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class SupportTask7
{
    public int Id { get; set; }

    public string DocumentNo { get; set; } = null!;

    public string DocumentName { get; set; } = null!;

    public string TaskDetail { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int AcadYear { get; set; }

    /// <summary>
    /// A = Active, N = Non-Active
    /// </summary>
    public string Status { get; set; } = null!;

    public string ApprovedEmpId { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
