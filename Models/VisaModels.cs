using System;
using System.Collections.Generic;

namespace RSU_360_X.Models
{
    public class VisaSubmission
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string StudentId { get; set; } = "";
        public string? StudentName { get; set; }
        public string? Passport { get; set; }
        public string? Country { get; set; }
        public string? VisaType { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "draft"; // draft, submitted, approved, needs_fix, rejected, pickup, completed
        
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        
        public List<FileRef> Files { get; set; } = new List<FileRef>();
        public List<HistoryEvent> History { get; set; } = new List<HistoryEvent>();

        // Additional Info
        public string? StaffNotes { get; set; }
        public DateTime? PickupAppointmentAt { get; set; }
        public DateTime? PickedUpAt { get; set; }
    }

    public class FileRef
    {
        public string Name { get; set; } = "";
        public string Url { get; set; } = "";
        public string Category { get; set; } = "general"; // New: general, passport, stamp, tm30, receipt
    }

    public class HistoryEvent
    {
        public DateTime At { get; set; }
        public string Event { get; set; } = ""; 
        public string? Note { get; set; }
    }

    public class VisaIndexVm
    {
        public string StudentId { get; set; } = "";
        public string Passport { get; set; } = "";
        public string Country { get; set; } = "";
        public string Status { get; set; } = "draft";
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public List<FileRef> Files { get; set; } = new List<FileRef>();
        public string? StaffNotes { get; set; }

        // --- NEW PROPERTIES ---
        public List<VisaSubmission> HistoryList { get; set; } = new List<VisaSubmission>();

        // Helper to determine active step (1-4)
        public int CurrentStep
        {
            get
            {
                return Status switch
                {
                    "draft" => 1,
                    "submitted" => 2,
                    "needs_fix" => 2,
                    "approved" => 3,
                    "pickup" => 4,
                    "completed" => 5,
                    "rejected" => 5, // End state
                    _ => 1
                };
            }
        }
    }
}
