using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class AcademicArticle24
{
    public string MeetingName { get; set; } = null!;

    public string ArticleTitle { get; set; } = null!;

    public string Authors { get; set; } = null!;

    public DateOnly DayMonthYear { get; set; }

    public int PublishYear { get; set; }

    public string Country { get; set; } = null!;

    public string Abstract { get; set; } = null!;

    public string Keywords { get; set; } = null!;

    /// <summary>
    /// ABST = abstract conference paper, FULL = full conference paper
    /// </summary>
    public string PaperFormatType { get; set; } = null!;

    /// <summary>
    /// NTN = ระดับชาติ, INT = นานาชาติ
    /// </summary>
    public string PaperFormatLevel { get; set; } = null!;

    public int AcadYear { get; set; }

    public string Reason { get; set; } = null!;

    /// <summary>
    /// W = Waiting to Approve, A = Approved, R = Rejected
    /// </summary>
    public string Status { get; set; } = null!;

    public string ApprovedEmpId { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public int Id { get; set; }

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
