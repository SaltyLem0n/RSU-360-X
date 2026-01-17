using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class AcademicServiceQa32
{
    public string ProjectName { get; set; } = null!;

    public string ProjectType { get; set; } = null!;

    public string ServiceApplicant { get; set; } = null!;

    public DateOnly ProjectStartDate { get; set; }

    public DateOnly ProjectEndDate { get; set; }

    public string OperatingResult { get; set; } = null!;

    public int AcadYear { get; set; }

    /// <summary>
    /// A = Active, N = Non active
    /// </summary>
    public string Status { get; set; } = null!;

    public string ApprovedEmpId { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public int Id { get; set; }

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
