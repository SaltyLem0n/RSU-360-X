using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class ResearchGrant23
{
    public string ResearchTopic { get; set; } = null!;

    public string Position { get; set; } = null!;

    public string Sponsor { get; set; } = null!;

    public string NumberOfYear { get; set; } = null!;

    public string ContactPeriod { get; set; } = null!;

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
