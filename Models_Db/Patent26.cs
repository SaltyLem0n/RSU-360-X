using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class Patent26
{
    public string NameOfWork { get; set; } = null!;

    public string CopyrightNumber { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateOnly DayMonthYear { get; set; }

    public int AcadYear { get; set; }

    public string Reason { get; set; } = null!;

    /// <summary>
    /// W = Waiting for Approved, A = Approved, R = Rejected
    /// </summary>
    public string Status { get; set; } = null!;

    public string ApprovedEmpId { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public int Id { get; set; }

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
