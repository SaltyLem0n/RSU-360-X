using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class DataCentricUser
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    /// <summary>
    /// A = Active, N = Non-active
    /// </summary>
    public string Status { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
