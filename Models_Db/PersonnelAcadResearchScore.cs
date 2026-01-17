using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class PersonnelAcadResearchScore
{
    public string TrId { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public DateOnly UpdatedDate { get; set; }

    public decimal TeachingMeterialScore21 { get; set; }

    public decimal BookScore22 { get; set; }

    public decimal AcadResearchScore23 { get; set; }

    public decimal AcadResearchScore24 { get; set; }

    public decimal AcadResearchScore25 { get; set; }

    public decimal AcadResearchScore26 { get; set; }

    public decimal AcadResearchScore27 { get; set; }

    public decimal AcadResearchScore28 { get; set; }

    public decimal AcadResearchScore29 { get; set; }

    public decimal AcadResearchScore210 { get; set; }

    public decimal PatentsScore211 { get; set; }

    public decimal PatentsScore212 { get; set; }

    public decimal ResearchGrantsScore213 { get; set; }

    public decimal ResearchGrantsScore214 { get; set; }

    public decimal ResearchGrantsScore215 { get; set; }

    public decimal ResearchGrantsScore216 { get; set; }

    public decimal CreativeWorkScore217 { get; set; }

    public decimal CreativeWorkScore218 { get; set; }

    public decimal CreativeWorkScore219 { get; set; }

    public decimal CreativeWorkScore220 { get; set; }

    public decimal CreativeWorkScore221 { get; set; }

    public virtual PersonnelScore? PersonnelScore { get; set; }
}
