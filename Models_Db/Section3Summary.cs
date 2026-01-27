using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class Section3Summary
{
    public int Id { get; set; }

    public int AcadYear { get; set; }

    public string PersonnelEmpId { get; set; } = null!;

    public string? SummaryComments { get; set; }

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
