using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class AssignTask5
{
    public string AssignedTask { get; set; } = null!;

    public string AssignedBy { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int AcadYear { get; set; }

    public string PersonnelEmpId { get; set; } = null!;

    public int Id { get; set; }

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
