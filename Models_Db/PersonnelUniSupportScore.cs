using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class PersonnelUniSupportScore
{
    public string TrId { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public DateOnly UpdatedDate { get; set; }

    public decimal UniSupport81 { get; set; }

    public decimal UniSupport82 { get; set; }

    public virtual PersonnelScore? PersonnelScore { get; set; }
}
