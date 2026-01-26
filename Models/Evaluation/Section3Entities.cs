using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSU_360_X.Models.Evaluation
{
    // 3.1 Academic Service (Projects with Income)
    [Table("academic_service_3_1", Schema = "ev")]
    public class AcademicService31
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("emp_id")] public string EmpId { get; set; } = "";
        [Column("acad_year")] public string AcadYear { get; set; } = "";

        [Column("project_name")] public string? ProjectName { get; set; }
        [Column("recipient_agency")] public string? RecipientAgency { get; set; }
        [Column("service_type")] public string? ServiceType { get; set; }
        [Column("role_in_project")] public string? RoleInProject { get; set; }
        [Column("start_date")] public DateTime? StartDate { get; set; }
        [Column("uni_allocation")] public decimal? UniAllocation { get; set; }
        [Column("payment_date")] public DateTime? PaymentDate { get; set; }
        [Column("remarks")] public string? Remarks { get; set; }
    }

    // 3.2 QA
    [Table("academic_service_qa_3_2", Schema = "ev")]
    public class AcademicServiceQa32
    {
        [Key][Column("id")] public int Id { get; set; }
        [Column("emp_id")] public string EmpId { get; set; } = "";
        [Column("acad_year")] public string AcadYear { get; set; } = "";

        [Column("project_name")] public string? ProjectName { get; set; }
        [Column("recipient_agency")] public string? RecipientAgency { get; set; }
        [Column("role_in_project")] public string? RoleInProject { get; set; }
        [Column("duty")] public string? Duty { get; set; }
        [Column("start_date")] public DateTime? StartDate { get; set; }
        [Column("end_date")] public DateTime? EndDate { get; set; }
        [Column("summary")] public string? Summary { get; set; }
    }

    // 3.3 Invite Speaker
    [Table("invite_speaker_3_3", Schema = "ev")]
    public class InviteSpeaker33
    {
        [Key][Column("id")] public int Id { get; set; }
        [Column("emp_id")] public string EmpId { get; set; } = "";
        [Column("acad_year")] public string AcadYear { get; set; } = "";

        [Column("activity_name")] public string? ActivityName { get; set; }
        [Column("role")] public string? Role { get; set; }
        [Column("agency")] public string? Agency { get; set; }
        [Column("type")] public string? Type { get; set; } // Internal/External
        [Column("start_date")] public DateTime? StartDate { get; set; }
        [Column("end_date")] public DateTime? EndDate { get; set; }
    }

    // 3.4 Invite Reviewer/Expert
    [Table("invite_reviewer_3_4", Schema = "ev")]
    public class InviteReviewer34
    {
        [Key][Column("id")] public int Id { get; set; }
        [Column("emp_id")] public string EmpId { get; set; } = "";
        [Column("acad_year")] public string AcadYear { get; set; } = "";

        [Column("journal_name")] public string? JournalName { get; set; }
        [Column("article_name")] public string? ArticleName { get; set; }
        [Column("agency")] public string? Agency { get; set; }
        [Column("type")] public string? Type { get; set; } // National/International
        [Column("start_date")] public DateTime? StartDate { get; set; }
        [Column("end_date")] public DateTime? EndDate { get; set; }
    }

    // 4. Culture Activity
    [Table("culture_activity_4", Schema = "ev")]
    public class CultureActivity4
    {
        [Key][Column("id")] public int Id { get; set; }
        [Column("emp_id")] public string EmpId { get; set; } = "";
        [Column("acad_year")] public string AcadYear { get; set; } = "";

        [Column("event_name")] public string? EventName { get; set; }
        [Column("level")] public string? Level { get; set; }
        [Column("start_date_time")] public DateTime? StartDateTime { get; set; }
        [Column("end_date_time")] public DateTime? EndDateTime { get; set; }
    }

    // 5. Assign Task
    [Table("assign_task_5", Schema = "ev")]
    public class AssignTask5
    {
        [Key][Column("id")] public int Id { get; set; }
        [Column("emp_id")] public string EmpId { get; set; } = "";
        [Column("acad_year")] public string AcadYear { get; set; } = "";

        [Column("task_name")] public string? TaskName { get; set; }
        [Column("assigned_by")] public string? AssignedBy { get; set; }
        [Column("start_date_time")] public DateTime? StartDateTime { get; set; }
        [Column("end_date_time")] public DateTime? EndDateTime { get; set; }
    }

    // 6. Administrative Task
    [Table("administrative_task_6", Schema = "ev")]
    public class AdministrativeTask6
    {
        [Key][Column("id")] public int Id { get; set; }
        [Column("emp_id")] public string EmpId { get; set; } = "";
        [Column("acad_year")] public string AcadYear { get; set; } = "";

        [Column("task_name")] public string? TaskName { get; set; }
    }

    // 7. Support Task
    [Table("support_task_7", Schema = "ev")]
    public class SupportTask7
    {
        [Key][Column("id")] public int Id { get; set; }
        [Column("emp_id")] public string EmpId { get; set; } = "";
        [Column("acad_year")] public string AcadYear { get; set; } = "";

        [Column("task_name")] public string? TaskName { get; set; }
    }

    // 8. Personnel Development
    [Table("personnel_development_8", Schema = "ev")]
    public class PersonnelDevelopment8
    {
        [Key][Column("id")] public int Id { get; set; }
        [Column("personnel_emp_id")] public string PersonnelEmpId { get; set; } = "";
        [Column("acad_year")] public int AcadYear { get; set; }

        [Column("topic_name")] public string? TopicName { get; set; }
        [Column("type")] public string? Type { get; set; }
        [Column("start_date")] public DateOnly StartDate { get; set; }
        [Column("end_date")] public DateOnly EndDate { get; set; }
        [Column("organizers")] public string? Organizers { get; set; }
        [Column("status")] public string Status { get; set; } = "A";
        [Column("approved_emp_id")] public string ApprovedEmpId { get; set; } = "-";
    }
}
