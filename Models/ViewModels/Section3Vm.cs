namespace RSU_360_X.Models.ViewModels
{
    public class AcademicServicesViewModel
    {
        public string? LecturerId { get; set; }
        public string? AcademicYear { get; set; }
        public string? SummaryComments { get; set; }

        public string? EmpFname { get; set; }
        public string? EmpLname { get; set; }
        public string? EmpFaculty { get; set; }

        public List<ProjectWithIncome> ProjectsWithIncome { get; set; } = new();
        public List<QualityAssuranceProject> QualityAssuranceProjects { get; set; } = new();
        public List<SpeakerActivity> SpeakerActivities { get; set; } = new();
        public List<ExpertActivity> ExpertActivities { get; set; } = new();
        public List<ArtsAndCultureActivity> ArtsAndCultureActivities { get; set; } = new();
        public List<OtherTask> OtherTasks { get; set; } = new();
        public List<AdminWork> AdminWork { get; set; } = new();
        public List<SupportTask> SupportTasks { get; set; } = new();
        public List<PersonnelDevelopmentItem> PersonnelDevelopments { get; set; } = new();
    }

    public class ProjectWithIncome
    {
        public string? Id { get; set; }
        public string? ProjectName { get; set; }
        public string? RecipientAgency { get; set; } // Map to ServiceApplicant
        public string? ServiceType { get; set; } // Map to TypeOfWork
        public string? RoleInProject { get; set; } // Not in DB
        public string? StartDate { get; set; } // Map to ProjectStartDate
        public decimal? UniversityAllocation { get; set; } // Map to Fund (try parse)
        public string? PaymentDate { get; set; } // Not in DB
        public string? Remarks { get; set; } // Map to Note
    }

    public class QualityAssuranceProject
    {
        public string? Id { get; set; }
        public string? ProjectName { get; set; }
        public string? RecipientAgency { get; set; } // Map to ServiceApplicant
        public string? RoleInProject { get; set; } // Not in DB
        public string? Duty { get; set; } // Map to OperatingResult (Maybe?) or TypeOfWork? QA usually has specific fields. IDK. Will check DB or map to best guess. 3.2 has OperatingResult.
        public string? StartDate { get; set; } // Map to ProjectStartDate
        public string? Summary { get; set; } // Map to ? Maybe Note? 3.2 db has ProjectName, ServiceApplicant, ProjectType, ProjectStartDate, ProjectEndDate, OperatingResult. No Summary.
    }

    public class SpeakerActivity
    {
        public string? Id { get; set; }
        public string? ActivityName { get; set; } // Map to ProjectName
        public string? Role { get; set; } // Map to Role
        public string? Agency { get; set; } // Map to ServiceRecipientAgency
        public string? Type { get; set; } // Map to ? Maybe not in DB.
        public string? StartDate { get; set; } // Map to StartDate
    }

    public class ExpertActivity
    {
        public string? Id { get; set; }
        public string? JournalName { get; set; } // Map to JournalConferenceName
        public string? ArticleName { get; set; } // Map to ArticleTitle
        public string? Agency { get; set; } // Map to ServiceApplicant
        public string? Type { get; set; } // Not in DB
        public string? StartDate { get; set; } // Map to ProjectStartDate
    }

    public class ArtsAndCultureActivity
    {
        public string? Id { get; set; }
        public string? EventName { get; set; } // Map to EventName
        public string? Level { get; set; } // Not in DB? 4 has EventName, StartDate, EndDate, Role. No Level?
        public DateTime? StartDateTime { get; set; } // Map to StartDate
        public DateTime? EndDateTime { get; set; } // Map to EndDate
    }

    public class OtherTask
    {
        public string? Id { get; set; }
        public string? TaskName { get; set; } // Map to AssignedTask
        public string? AssignedBy { get; set; } // Map to AssignedBy
        public DateTime? StartDateTime { get; set; } // Map to StartDate
        public DateTime? EndDateTime { get; set; } // Map to EndDate
    }

    public class AdminWork
    {
        public string? Id { get; set; }
        public string? TaskName { get; set; } // Map to TaskName
    }

    public class SupportTask
    {
        public string? Id { get; set; }
        public string? TaskName { get; set; } // Map to TaskName? 7 has TaskName? DB 7 has nothing? Check SupportTask7 in DB.
    }

    public class PersonnelDevelopmentItem
    {
        public string? Id { get; set; }
        public string? TopicName { get; set; }
        public string? Type { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Organizers { get; set; }
        public string? Status { get; set; }
    }
}
