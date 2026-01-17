using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class VisaTransaction
{
    /// <summary>
    /// transaction id = INT12680000001 (INT: Inter + mm + current year + XXXXXXX)
    /// </summary>
    public string TrId { get; set; } = null!;

    public int AcadYear { get; set; }

    public string StudentId { get; set; } = null!;

    public string StudentFname { get; set; } = null!;

    public string StudentDname { get; set; } = null!;

    public string StudentLname { get; set; } = null!;

    public string SchoolEmail { get; set; } = null!;

    public string StudentEmail { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

    public string StudentCitizen { get; set; } = null!;

    public string VisaType { get; set; } = null!;

    public DateOnly VisaIssueDate { get; set; }

    public DateOnly VisaExpiryDate { get; set; }

    public DateOnly? VisaNextExpiryDate { get; set; }

    /// <summary>
    /// pickup date = current date + 10 days
    /// </summary>
    public DateOnly VisaDocPickup { get; set; }

    /// <summary>
    /// I = In progress, A = approved, R = Rejected
    /// </summary>
    public string Status { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public DateOnly UpdatedDate { get; set; }

    public virtual ICollection<VisaTransactionStatus> VisaTransactionStatuses { get; set; } = new List<VisaTransactionStatus>();
}
