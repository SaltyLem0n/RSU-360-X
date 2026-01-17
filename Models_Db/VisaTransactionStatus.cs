using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class VisaTransactionStatus
{
    public int Ids { get; set; }

    public string TrId { get; set; } = null!;

    public DateOnly TrDate { get; set; }

    /// <summary>
    /// S = Submitted, V = Under review, A = Approved document, P = Pickup document, C = Completed  
    /// </summary>
    public string VisaStatus { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public string PersonnelEmpId { get; set; } = null!;

    public virtual Personnel PersonnelEmp { get; set; } = null!;

    public virtual VisaTransaction Tr { get; set; } = null!;
}
