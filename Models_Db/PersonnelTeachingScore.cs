using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class PersonnelTeachingScore
{
    public string TrId { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public DateOnly UpdatedDate { get; set; }

    public decimal TeachingLoadScore112 { get; set; }

    public decimal StudentEvalScore13 { get; set; }

    public decimal ColleagueEvalScore14 { get; set; }

    public decimal AdvisorScore15 { get; set; }

    public virtual PersonnelScore? PersonnelScore { get; set; }
}
