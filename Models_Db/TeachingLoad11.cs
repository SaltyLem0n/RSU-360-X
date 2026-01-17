using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class TeachingLoad11
{
    public int AcadYear { get; set; }

    public string AcadSemester { get; set; } = null!;

    public decimal TotalCreditLecture { get; set; }

    public decimal TotalCreditLab { get; set; }

    public decimal TotalCreditThesis { get; set; }

    public decimal TotalCredit { get; set; }

    /// <summary>
    /// A = Active, N = Non active
    /// </summary>
    public string Status { get; set; } = null!;

    public string ApprovedEmpId { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public virtual Personnel PersonnelEmp { get; set; } = null!;

    public virtual ICollection<TeachingLoadDetails11> TeachingLoadDetails11s { get; set; } = new List<TeachingLoadDetails11>();
}
