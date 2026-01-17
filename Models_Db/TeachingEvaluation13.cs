using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class TeachingEvaluation13
{
    public string SubjectCode { get; set; } = null!;

    public string SubjectName { get; set; } = null!;

    /// <summary>
    /// LEC = lecture, LAB = laboratory, THS = Thesis/project/dissertation
    /// </summary>
    public string TeachingType { get; set; } = null!;

    public DateOnly DayMonthYear { get; set; }

    public decimal Score { get; set; }

    public int AcadYear { get; set; }

    /// <summary>
    /// TS = Summer term, T1 = Semester 1, T2 = Semester 2
    /// </summary>
    public string AcadSemester { get; set; } = null!;

    /// <summary>
    /// A = Active, N = Non active
    /// </summary>
    public string Status { get; set; } = null!;

    public string ApprovedEmpId { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public int Id { get; set; }

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
