using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class InviteSpeaker33
{
    public string ProjectName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string ServiceRecipientAgency { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public int AcadYear { get; set; }

    /// <summary>
    /// A - Active, N = Non active
    /// </summary>
    public string Status { get; set; } = null!;

    public string ApprovedEmpId { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public int Id { get; set; }

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
