using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class PersonnelApprovalStatus
{
    public int Id { get; set; }

    /// <summary>
    /// transaction id = EV520005068001 (ev + personnel_emp_id + acad year + XXX)
    /// </summary>
    public string TrId { get; set; } = null!;

    public string EmpIdApproval { get; set; } = null!;

    public DateOnly ApprovalDate { get; set; }

    public string EmpNoted { get; set; } = null!;

    /// <summary>
    /// S = submitted
    /// , A = approve
    /// , 
    /// R = reject
    /// </summary>
    public string Status { get; set; } = null!;

    public virtual PersonnelScore Tr { get; set; } = null!;
}
