using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class PersonnelScore
{
    /// <summary>
    /// transaction id = EVS12680000001 (ev + mm + current year + XXXXXXX)
    /// </summary>
    public string TrId { get; set; } = null!;

    public string EmpId { get; set; } = null!;

    public int AcadYear { get; set; }

    public DateOnly CreatedDate { get; set; }

    public DateOnly UpdatedDate { get; set; }

    public decimal TeachingScore { get; set; }

    public decimal AcadResearchScore { get; set; }

    public decimal AcadServiceScore { get; set; }

    public decimal ArtCultureScore { get; set; }

    public decimal OtherTaskScore { get; set; }

    public decimal AdministrativeScore { get; set; }

    public decimal VirtuesEthicsScore { get; set; }

    public decimal UniversitySupportScore { get; set; }

    public decimal TotalScore { get; set; }

    /// <summary>
    /// A = Active transaction 
    /// N = Non active transaction 
    /// E = End evaluation process
    /// </summary>
    public string Status { get; set; } = null!;

    public virtual Personnel Emp { get; set; } = null!;

    public virtual ICollection<PersonnelApprovalStatus> PersonnelApprovalStatuses { get; set; } = new List<PersonnelApprovalStatus>();

    public virtual PersonnelAcadResearchScore Tr { get; set; } = null!;

    public virtual PersonnelAdminScore Tr1 { get; set; } = null!;

    public virtual PersonnelArtCultureScore Tr2 { get; set; } = null!;

    public virtual PersonnelOtherTaskScore Tr3 { get; set; } = null!;

    public virtual PersonnelTeachingScore Tr4 { get; set; } = null!;

    public virtual PersonnelUniSupportScore Tr5 { get; set; } = null!;

    public virtual PersonnelVirturesEthicsScore Tr6 { get; set; } = null!;

    public virtual PersonnelAcadServiceScore TrNavigation { get; set; } = null!;
}
