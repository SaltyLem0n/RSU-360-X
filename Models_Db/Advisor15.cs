using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class Advisor15
{
    public string Activity { get; set; } = null!;

    public int NumberOfStudent { get; set; }

    public decimal Percentage { get; set; }

    public int AcadYear { get; set; }

    public string PersonnelEmpId { get; set; } = null!;

    public int Id { get; set; }

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
