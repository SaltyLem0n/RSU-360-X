using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class TeachingLoadDetails11
{
    public string SubjectCode { get; set; } = null!;

    public int AcadYear { get; set; }

    /// <summary>
    /// TS = Term Summer, T1 = Term 1, T2 = Term 2
    /// </summary>
    public string AcadSemester { get; set; } = null!;

    public string SubjectSection { get; set; } = null!;

    public string SubjectName { get; set; } = null!;

    /// <summary>
    /// LEC = lecture, LAB = laboratory, THS = project/dissertation/thesis
    /// </summary>
    public string TeachingType { get; set; } = null!;

    public DateOnly DayMonthYear { get; set; }

    public decimal Credit { get; set; }

    public int NumberOfStudents { get; set; }

    public string PersonnelEmpId { get; set; } = null!;

    public virtual TeachingLoad11 TeachingLoad11 { get; set; } = null!;
}
