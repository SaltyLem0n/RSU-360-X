using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class PersonnelVirturesEthicsScore
{
    public string TrId { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public DateOnly UpdatedDate { get; set; }

    public decimal VirtuesEthicsScore71 { get; set; }

    public decimal VirtuesEthicsScore72 { get; set; }

    public decimal VirtuesEthicsScore73 { get; set; }

    public decimal VirtuesEthicsScore74 { get; set; }

    public decimal VirtuesEthicsScore75 { get; set; }

    public virtual PersonnelScore? PersonnelScore { get; set; }
}
