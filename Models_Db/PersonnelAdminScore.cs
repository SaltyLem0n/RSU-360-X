using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class PersonnelAdminScore
{
    public string TrId { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public DateOnly UpdatedDate { get; set; }

    public decimal AdminScore61 { get; set; }

    public decimal AdminScore62 { get; set; }

    public virtual PersonnelScore? PersonnelScore { get; set; }
}
