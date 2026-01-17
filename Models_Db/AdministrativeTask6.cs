using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class AdministrativeTask6
{
    public int AcadYear { get; set; }

    public string TaskName { get; set; } = null!;

    public string TaskDetail { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public int Id { get; set; }

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
