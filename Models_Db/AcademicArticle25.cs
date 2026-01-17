using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class AcademicArticle25
{
    public string ArticleTitle { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Publisher { get; set; } = null!;

    public int YearPublication { get; set; }

    public int MonthPublication { get; set; }

    public string Abstract { get; set; } = null!;

    public string Keywords { get; set; } = null!;

    public string Doi { get; set; } = null!;

    /// <summary>
    /// NTN = ระดับชาติ, INT = นานาชาติ
    /// </summary>
    public string PaperFormatLevel { get; set; } = null!;

    /// <summary>
    /// Scopus/ Web of Science / TCI 1/ TCI 2 / other
    /// </summary>
    public string PublisherDatabase { get; set; } = null!;

    /// <summary>
    /// Q1, Q2, Q3, Q4, and None
    /// </summary>
    public string PublisherCiteIndex { get; set; } = null!;

    public int AcadYear { get; set; }

    public string Reason { get; set; } = null!;

    /// <summary>
    /// W = Waiting to Approve, 
    /// A = Approved, R = Rejected
    /// </summary>
    public string Status { get; set; } = null!;

    public string ApprovedEmpId { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public int Id { get; set; }

    public virtual Personnel PersonnelEmp { get; set; } = null!;
}
