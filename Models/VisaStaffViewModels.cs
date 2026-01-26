using System;
using System.Collections.Generic;

namespace RSU_360_X.Models
{
    public class VisaReviewIndexVm
    {
        public List<VisaSubmission> Applications { get; set; } = new List<VisaSubmission>();
        public int PendingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        
        public int TotalCount { get; set; }
        public int CompletedCount { get; set; }
    }
}
