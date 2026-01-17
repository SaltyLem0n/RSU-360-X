using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class TeachingEvaluation14
{
    public string SubjectCode { get; set; } = null!;

    public string SubjectName { get; set; } = null!;

    public string AcadSemester { get; set; } = null!;

    public DateOnly DayMonthYear { get; set; }

    public decimal Score { get; set; }

    public int AcadYear { get; set; }

    /// <summary>
    /// A = Active, N = Non active
    /// </summary>
    public string Status { get; set; } = null!;

    public string ApprovedEmpId { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public virtual Personnel PersonnelEmp { get; set; } = null!;

    public virtual ICollection<TeachingEvaluationDetails14> TeachingEvaluationDetails14s { get; set; } = new List<TeachingEvaluationDetails14>();
}
