using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class TeachingEvaluationDetails14
{
    public string SubjectCode { get; set; } = null!;

    public int AcadYear { get; set; }

    public string AcadSemester { get; set; } = null!;

    public string SubjectSection { get; set; } = null!;

    public decimal Score { get; set; }

    public int Id { get; set; }

    public virtual TeachingEvaluation14 TeachingEvaluation14 { get; set; } = null!;
}
