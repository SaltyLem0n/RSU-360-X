using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RSU_360_X.Models_Db;

public partial class EvDbContext : DbContext
{
    public EvDbContext()
    {
    }

    public EvDbContext(DbContextOptions<EvDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicArticle24> AcademicArticle24s { get; set; }

    public virtual DbSet<AcademicArticle25> AcademicArticle25s { get; set; }

    public virtual DbSet<AcademicService31> AcademicService31s { get; set; }

    public virtual DbSet<AcademicServiceQa32> AcademicServiceQa32s { get; set; }

    public virtual DbSet<AdministrativeTask6> AdministrativeTask6s { get; set; }

    public virtual DbSet<Advisor15> Advisor15s { get; set; }

    public virtual DbSet<AssignTask5> AssignTask5s { get; set; }

    public virtual DbSet<CreativeWork27> CreativeWork27s { get; set; }

    public virtual DbSet<CultureActivity4> CultureActivity4s { get; set; }

    public virtual DbSet<DataCentricUser> DataCentricUsers { get; set; }

    public virtual DbSet<InviteReviewer34> InviteReviewer34s { get; set; }

    public virtual DbSet<InviteSpeaker33> InviteSpeaker33s { get; set; }

    public virtual DbSet<Patent26> Patent26s { get; set; }

    public virtual DbSet<Personnel> Personnel { get; set; }

    public virtual DbSet<PersonnelAcadResearchScore> PersonnelAcadResearchScores { get; set; }

    public virtual DbSet<PersonnelAcadServiceScore> PersonnelAcadServiceScores { get; set; }

    public virtual DbSet<PersonnelAdminScore> PersonnelAdminScores { get; set; }

    public virtual DbSet<PersonnelApprovalStatus> PersonnelApprovalStatuses { get; set; }

    public virtual DbSet<PersonnelArtCultureScore> PersonnelArtCultureScores { get; set; }

    public virtual DbSet<PersonnelOtherTaskScore> PersonnelOtherTaskScores { get; set; }

    public virtual DbSet<PersonnelScore> PersonnelScores { get; set; }

    public virtual DbSet<PersonnelTeachingScore> PersonnelTeachingScores { get; set; }

    public virtual DbSet<PersonnelUniSupportScore> PersonnelUniSupportScores { get; set; }

    public virtual DbSet<PersonnelVirturesEthicsScore> PersonnelVirturesEthicsScores { get; set; }

    public virtual DbSet<ResearchGrant23> ResearchGrant23s { get; set; }

    public virtual DbSet<Section3Summary> Section3Summaries { get; set; }

    public virtual DbSet<SupportTask7> SupportTask7s { get; set; }

    public virtual DbSet<TeachingDocument21> TeachingDocument21s { get; set; }

    public virtual DbSet<TeachingEvaluation13> TeachingEvaluation13s { get; set; }

    public virtual DbSet<TeachingEvaluation14> TeachingEvaluation14s { get; set; }

    public virtual DbSet<TeachingEvaluationDetails14> TeachingEvaluationDetails14s { get; set; }

    public virtual DbSet<TeachingLoad11> TeachingLoad11s { get; set; }

    public virtual DbSet<TeachingLoadDetails11> TeachingLoadDetails11s { get; set; }

    public virtual DbSet<Textbook22> Textbook22s { get; set; }

    public virtual DbSet<VisaTransaction> VisaTransactions { get; set; }

    public virtual DbSet<VisaTransactionStatus> VisaTransactionStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=evdb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicArticle24>(entity =>
        {
            entity.ToTable("academic_article_2_4", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_academic_article_2_4_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abstract)
                .HasColumnType("text")
                .HasColumnName("abstract");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.ArticleTitle)
                .HasMaxLength(50)
                .HasColumnName("article_title");
            entity.Property(e => e.Authors)
                .HasMaxLength(50)
                .HasColumnName("authors");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.DayMonthYear).HasColumnName("day_month_year");
            entity.Property(e => e.Keywords)
                .HasColumnType("text")
                .HasColumnName("keywords");
            entity.Property(e => e.MeetingName)
                .HasMaxLength(50)
                .HasColumnName("meeting_name");
            entity.Property(e => e.PaperFormatLevel)
                .HasMaxLength(3)
                .HasComment("NTN = ระดับชาติ, INT = นานาชาติ")
                .HasColumnName("paper_format_level");
            entity.Property(e => e.PaperFormatType)
                .HasMaxLength(4)
                .HasComment("ABST = abstract conference paper, FULL = full conference paper")
                .HasColumnName("paper_format_type");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.PublishYear).HasColumnName("publish_year");
            entity.Property(e => e.Reason)
                .HasDefaultValue("-")
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("W")
                .HasComment("W = Waiting to Approve, A = Approved, R = Rejected")
                .HasColumnName("status");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.AcademicArticle24s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_academic_article_2_4_personnel");
        });

        modelBuilder.Entity<AcademicArticle25>(entity =>
        {
            entity.ToTable("academic_article_2_5", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_academic_article_2_5_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abstract)
                .HasColumnType("text")
                .HasColumnName("abstract");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.ArticleTitle)
                .HasMaxLength(45)
                .HasColumnName("article_title");
            entity.Property(e => e.Author)
                .HasMaxLength(45)
                .HasColumnName("author");
            entity.Property(e => e.Doi)
                .HasColumnType("text")
                .HasColumnName("doi");
            entity.Property(e => e.Keywords)
                .HasColumnType("text")
                .HasColumnName("keywords");
            entity.Property(e => e.MonthPublication).HasColumnName("month_publication");
            entity.Property(e => e.PaperFormatLevel)
                .HasMaxLength(3)
                .HasComment("NTN = ระดับชาติ, INT = นานาชาติ")
                .HasColumnName("paper_format_level");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.Publisher)
                .HasMaxLength(45)
                .HasColumnName("publisher");
            entity.Property(e => e.PublisherCiteIndex)
                .HasMaxLength(45)
                .HasDefaultValue("-")
                .HasComment("Q1, Q2, Q3, Q4, and None")
                .HasColumnName("publisher_cite_index");
            entity.Property(e => e.PublisherDatabase)
                .HasMaxLength(45)
                .HasDefaultValue("-")
                .HasComment("Scopus/ Web of Science / TCI 1/ TCI 2 / other")
                .HasColumnName("publisher_database");
            entity.Property(e => e.Reason)
                .HasDefaultValue("-")
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("W")
                .HasComment("W = Waiting to Approve, \r\nA = Approved, R = Rejected")
                .HasColumnName("status");
            entity.Property(e => e.YearPublication).HasColumnName("year_publication");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.AcademicArticle25s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_academic_article_2_5_personnel");
        });

        modelBuilder.Entity<AcademicService31>(entity =>
        {
            entity.ToTable("academic_service_3_1", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_academic_service_3_1_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.Fund)
                .HasMaxLength(50)
                .HasColumnName("fund");
            entity.Property(e => e.Note)
                .HasMaxLength(50)
                .HasColumnName("note");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(50)
                .HasColumnName("project_name");
            entity.Property(e => e.ProjectStartDate).HasColumnName("project_start_date");
            entity.Property(e => e.ServiceApplicant)
                .HasMaxLength(50)
                .HasColumnName("service_applicant");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .HasComment("A = Active, N = Non active")
                .HasColumnName("status");
            entity.Property(e => e.TypeOfWork)
                .HasMaxLength(50)
                .HasColumnName("type_of_work");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.AcademicService31s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_academic_service_3_1_personnel");
        });

        modelBuilder.Entity<AcademicServiceQa32>(entity =>
        {
            entity.ToTable("academic_service_qa_3_2", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_academic_service_qa_3_2_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.OperatingResult)
                .HasMaxLength(45)
                .HasColumnName("operating_result");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.ProjectEndDate).HasColumnName("project_end_date");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(45)
                .HasColumnName("project_name");
            entity.Property(e => e.ProjectStartDate).HasColumnName("project_start_date");
            entity.Property(e => e.ProjectType)
                .HasMaxLength(45)
                .HasColumnName("project_type");
            entity.Property(e => e.ServiceApplicant)
                .HasMaxLength(45)
                .HasColumnName("service_applicant");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .HasComment("A = Active, N = Non active")
                .HasColumnName("status");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.AcademicServiceQa32s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_academic_service_qa_3_2_personnel");
        });

        modelBuilder.Entity<AdministrativeTask6>(entity =>
        {
            entity.ToTable("administrative_task_6", "ev");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.TaskDetail)
                .HasMaxLength(200)
                .HasColumnName("task_detail");
            entity.Property(e => e.TaskName)
                .HasMaxLength(45)
                .HasColumnName("task_name");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.AdministrativeTask6s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_administrative_task_personnel1");
        });

        modelBuilder.Entity<Advisor15>(entity =>
        {
            entity.ToTable("advisor_1_5", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_advisor_1_5_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.Activity)
                .HasMaxLength(100)
                .HasColumnName("activity");
            entity.Property(e => e.NumberOfStudent).HasColumnName("number_of_student");
            entity.Property(e => e.Percentage)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("percentage");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.Advisor15s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_advisor_1_5_personnel");
        });

        modelBuilder.Entity<AssignTask5>(entity =>
        {
            entity.ToTable("assign_task_5", "ev");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.AssignedBy)
                .HasMaxLength(45)
                .HasColumnName("assigned_by");
            entity.Property(e => e.AssignedTask)
                .HasMaxLength(45)
                .HasColumnName("assigned_task");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.AssignTask5s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_culture_activity_personnel10");
        });

        modelBuilder.Entity<CreativeWork27>(entity =>
        {
            entity.ToTable("creative_work_2_7", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_creative_work_2_7_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.DayMonthYear).HasColumnName("day_month_year");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.QualityLevel)
                .HasMaxLength(45)
                .HasColumnName("quality_level");
            entity.Property(e => e.Reason)
                .HasDefaultValue("-")
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("W")
                .HasComment("W = Waiting for Approved A = approved")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(45)
                .HasColumnName("type");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.CreativeWork27s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_creative_work_2_7_personnel");
        });

        modelBuilder.Entity<CultureActivity4>(entity =>
        {
            entity.ToTable("culture_activity_4", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "fk_culture_activity_personnel1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.EventName)
                .HasMaxLength(45)
                .HasColumnName("event_name");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.Role)
                .HasMaxLength(45)
                .HasColumnName("role");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.CultureActivity4s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_culture_activity_personnel1");
        });

        modelBuilder.Entity<DataCentricUser>(entity =>
        {
            entity.HasKey(e => e.PersonnelEmpId).HasName("PK_data_centric_users_1");

            entity.ToTable("data_centric_users", "hr");

            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .HasComment("A = Active, N = Non-active")
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.PersonnelEmp).WithOne(p => p.DataCentricUser)
                .HasForeignKey<DataCentricUser>(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_data_centric_users_personnel");
        });

        modelBuilder.Entity<InviteReviewer34>(entity =>
        {
            entity.ToTable("invite_reviewer_3_4", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_invite_reviewer_3_4_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.ArticleTitle)
                .HasMaxLength(45)
                .HasColumnName("article_title");
            entity.Property(e => e.JournalConferenceName)
                .HasMaxLength(45)
                .HasComment("")
                .HasColumnName("journal_conference_name");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.ProjectStartDate).HasColumnName("project_start_date");
            entity.Property(e => e.ServiceApplicant)
                .HasMaxLength(45)
                .HasColumnName("service_applicant");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .HasComment("A = Active, N = Non Active")
                .HasColumnName("status");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.InviteReviewer34s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_invite_reviewer_3_4_personnel");
        });

        modelBuilder.Entity<InviteSpeaker33>(entity =>
        {
            entity.ToTable("invite_speaker_3_3", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_invite_speaker_3_3_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(45)
                .HasColumnName("project_name");
            entity.Property(e => e.Role)
                .HasMaxLength(45)
                .HasColumnName("role");
            entity.Property(e => e.ServiceRecipientAgency)
                .HasMaxLength(45)
                .HasColumnName("service_recipient_agency");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .HasComment("A - Active, N = Non active")
                .HasColumnName("status");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.InviteSpeaker33s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_invite_speaker_3_3_personnel");
        });

        modelBuilder.Entity<Patent26>(entity =>
        {
            entity.ToTable("patent_2_6", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_patent_2_6_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.CopyrightNumber)
                .HasMaxLength(45)
                .HasColumnName("copyright_number");
            entity.Property(e => e.DayMonthYear).HasColumnName("day_month_year");
            entity.Property(e => e.NameOfWork)
                .HasMaxLength(45)
                .HasColumnName("name_of_work");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.Reason)
                .HasDefaultValue("-")
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("W")
                .HasComment("W = Waiting for Approved, A = Approved, R = Rejected")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(45)
                .HasColumnName("type");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.Patent26s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_patent_2_6_personnel");
        });

        modelBuilder.Entity<Personnel>(entity =>
        {
            entity.HasKey(e => e.EmpId);

            entity.ToTable("personnel", "hr");

            entity.Property(e => e.EmpId)
                .HasMaxLength(7)
                .HasColumnName("emp_id");
            entity.Property(e => e.EmpAcademicPos)
                .HasMaxLength(45)
                .HasDefaultValue("-")
                .HasColumnName("emp_academic_pos");
            entity.Property(e => e.EmpAge).HasColumnName("emp_age");
            entity.Property(e => e.EmpDepartment)
                .HasMaxLength(45)
                .HasDefaultValue("-")
                .HasColumnName("emp_department");
            entity.Property(e => e.EmpDeptId)
                .HasMaxLength(10)
                .HasDefaultValue("-")
                .HasColumnName("emp_dept_id");
            entity.Property(e => e.EmpDob).HasColumnName("emp_dob");
            entity.Property(e => e.EmpEmail)
                .HasMaxLength(50)
                .HasComment("Only school email (@rsu.ac.th)")
                .HasColumnName("emp_email");
            entity.Property(e => e.EmpFaculty)
                .HasMaxLength(45)
                .HasDefaultValue("-")
                .HasColumnName("emp_faculty");
            entity.Property(e => e.EmpFacultyId)
                .HasMaxLength(10)
                .HasDefaultValue("-")
                .HasColumnName("emp_faculty_id");
            entity.Property(e => e.EmpFname)
                .HasMaxLength(45)
                .HasColumnName("emp_fname");
            entity.Property(e => e.EmpHEducation)
                .HasMaxLength(45)
                .HasComment("PhD / Master / Bachelor")
                .HasColumnName("emp_h_education");
            entity.Property(e => e.EmpLineWork)
                .HasMaxLength(7)
                .HasColumnName("emp_line_work");
            entity.Property(e => e.EmpLname)
                .HasMaxLength(45)
                .HasColumnName("emp_lname");
            entity.Property(e => e.EmpOffice)
                .HasMaxLength(50)
                .HasDefaultValue("-")
                .HasColumnName("emp_office");
            entity.Property(e => e.EmpOfficeId)
                .HasMaxLength(10)
                .HasDefaultValue("-")
                .HasColumnName("emp_office_id");
            entity.Property(e => e.EmpPos)
                .HasMaxLength(45)
                .HasComment("Head of Program, Director, Dean, Associated Dean")
                .HasColumnName("emp_pos");
            entity.Property(e => e.EmpStartDate).HasColumnName("emp_start_date");
            entity.Property(e => e.EmpType)
                .HasMaxLength(1)
                .HasComment("L = lecturers, S = staffs")
                .HasColumnName("emp_type");
            entity.Property(e => e.EmpWorkType)
                .HasMaxLength(50)
                .HasColumnName("emp_work_type");
            entity.Property(e => e.ResearchApprovedYearAcademic)
                .HasMaxLength(10)
                .HasColumnName("research_approved_year_academic");
        });

        modelBuilder.Entity<PersonnelAcadResearchScore>(entity =>
        {
            entity.HasKey(e => e.TrId);

            entity.ToTable("personnel_acad_research_score", "ev");

            entity.Property(e => e.TrId)
                .HasMaxLength(14)
                .HasColumnName("tr_id");
            entity.Property(e => e.AcadResearchScore210)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_research_score_2_10");
            entity.Property(e => e.AcadResearchScore23)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_research_score_2_3");
            entity.Property(e => e.AcadResearchScore24)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_research_score_2_4");
            entity.Property(e => e.AcadResearchScore25)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_research_score_2_5");
            entity.Property(e => e.AcadResearchScore26)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_research_score_2_6");
            entity.Property(e => e.AcadResearchScore27)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_research_score_2_7");
            entity.Property(e => e.AcadResearchScore28)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_research_score_2_8");
            entity.Property(e => e.AcadResearchScore29)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_research_score_2_9");
            entity.Property(e => e.BookScore22)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("book_score_2_2");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CreativeWorkScore217)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("creative_work_score_2_17");
            entity.Property(e => e.CreativeWorkScore218)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("creative_work_score_2_18");
            entity.Property(e => e.CreativeWorkScore219)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("creative_work_score_2_19");
            entity.Property(e => e.CreativeWorkScore220)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("creative_work_score_2_20");
            entity.Property(e => e.CreativeWorkScore221)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("creative_work_score_2_21");
            entity.Property(e => e.PatentsScore211)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("patents_score_2_11");
            entity.Property(e => e.PatentsScore212)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("patents_score_2_12");
            entity.Property(e => e.ResearchGrantsScore213)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("research_grants_score_2_13");
            entity.Property(e => e.ResearchGrantsScore214)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("research_grants_score_2_14");
            entity.Property(e => e.ResearchGrantsScore215)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("research_grants_score_2_15");
            entity.Property(e => e.ResearchGrantsScore216)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("research_grants_score_2_16");
            entity.Property(e => e.TeachingMeterialScore21)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("teaching_meterial_score_2_1");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<PersonnelAcadServiceScore>(entity =>
        {
            entity.HasKey(e => e.TrId);

            entity.ToTable("personnel_acad_service_score", "ev");

            entity.Property(e => e.TrId)
                .HasMaxLength(14)
                .HasColumnName("tr_id");
            entity.Property(e => e.AcadServiceScore31)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_service_score_3_1");
            entity.Property(e => e.AcadServiceScore32)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_service_score_3_2");
            entity.Property(e => e.AcadServiceScore33)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_service_score_3_3");
            entity.Property(e => e.AcadServiceScore34)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_service_score_3_4");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<PersonnelAdminScore>(entity =>
        {
            entity.HasKey(e => e.TrId).HasName("PK_personnel_administrative_score");

            entity.ToTable("personnel_admin_score", "ev");

            entity.Property(e => e.TrId)
                .HasMaxLength(14)
                .HasColumnName("tr_id");
            entity.Property(e => e.AdminScore61)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("admin_score_6_1");
            entity.Property(e => e.AdminScore62)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("admin_score_6_2");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<PersonnelApprovalStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_personnel_approval_status_1");

            entity.ToTable("personnel_approval_status", "ev");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovalDate).HasColumnName("approval_date");
            entity.Property(e => e.EmpIdApproval)
                .HasMaxLength(7)
                .HasColumnName("emp_id_approval");
            entity.Property(e => e.EmpNoted)
                .HasColumnType("text")
                .HasColumnName("emp_noted");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasComment("S = submitted\r\n, A = approve\r, \nR = reject")
                .HasColumnName("status");
            entity.Property(e => e.TrId)
                .HasMaxLength(14)
                .HasComment("transaction id = EV520005068001 (ev + personnel_emp_id + acad year + XXX)")
                .HasColumnName("tr_id");

            entity.HasOne(d => d.Tr).WithMany(p => p.PersonnelApprovalStatuses)
                .HasForeignKey(d => d.TrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_personnel_approval_status_personnel_scoring");
        });

        modelBuilder.Entity<PersonnelArtCultureScore>(entity =>
        {
            entity.HasKey(e => e.TrId);

            entity.ToTable("personnel_art_culture_score", "ev");

            entity.Property(e => e.TrId)
                .HasMaxLength(14)
                .HasColumnName("tr_id");
            entity.Property(e => e.ArtCultureScore41)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("art_culture_score_4_1");
            entity.Property(e => e.ArtCultureScore42)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("art_culture_score_4_2");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<PersonnelOtherTaskScore>(entity =>
        {
            entity.HasKey(e => e.TrId);

            entity.ToTable("personnel_other_task_score", "ev");

            entity.Property(e => e.TrId)
                .HasMaxLength(14)
                .HasColumnName("tr_id");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.OtherTaskScore51)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("other_task_score_5_1");
            entity.Property(e => e.OtherTaskScore52)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("other_task_score_5_2");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<PersonnelScore>(entity =>
        {
            entity.HasKey(e => e.TrId).HasName("PK_personnel_scoring");

            entity.ToTable("personnel_score", "ev");

            entity.Property(e => e.TrId)
                .HasMaxLength(14)
                .HasComment("transaction id = EVS12680000001 (ev + mm + current year + XXXXXXX)")
                .HasColumnName("tr_id");
            entity.Property(e => e.AcadResearchScore)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_research_score");
            entity.Property(e => e.AcadServiceScore)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("acad_service_score");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.AdministrativeScore)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("administrative_score");
            entity.Property(e => e.ArtCultureScore)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("art_culture_score");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.EmpId)
                .HasMaxLength(7)
                .HasColumnName("emp_id");
            entity.Property(e => e.OtherTaskScore)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("other_task_score");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("-")
                .HasComment("A = Active transaction \r\nN = Non active transaction \r\nE = End evaluation process")
                .HasColumnName("status");
            entity.Property(e => e.TeachingScore)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("teaching_score");
            entity.Property(e => e.TotalScore)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("total_score");
            entity.Property(e => e.UniversitySupportScore)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("university_support_score");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.VirtuesEthicsScore)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("virtues_ethics_score");

            entity.HasOne(d => d.Emp).WithMany(p => p.PersonnelScores)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_personnel_scoring_personnel");

            entity.HasOne(d => d.Tr).WithOne(p => p.PersonnelScore)
                .HasForeignKey<PersonnelScore>(d => d.TrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_personnel_score_personnel_acad_research_score");

            entity.HasOne(d => d.TrNavigation).WithOne(p => p.PersonnelScore)
                .HasForeignKey<PersonnelScore>(d => d.TrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_personnel_score_personnel_acad_service_score");

            entity.HasOne(d => d.Tr1).WithOne(p => p.PersonnelScore)
                .HasForeignKey<PersonnelScore>(d => d.TrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_personnel_score_personnel_admin_score");

            entity.HasOne(d => d.Tr2).WithOne(p => p.PersonnelScore)
                .HasForeignKey<PersonnelScore>(d => d.TrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_personnel_score_personnel_art_culture_score");

            entity.HasOne(d => d.Tr3).WithOne(p => p.PersonnelScore)
                .HasForeignKey<PersonnelScore>(d => d.TrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_personnel_score_personnel_other_task_score");

            entity.HasOne(d => d.Tr4).WithOne(p => p.PersonnelScore)
                .HasForeignKey<PersonnelScore>(d => d.TrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_personnel_score_personnel_teaching_score");

            entity.HasOne(d => d.Tr5).WithOne(p => p.PersonnelScore)
                .HasForeignKey<PersonnelScore>(d => d.TrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_personnel_score_personnel_uni_support_score");

            entity.HasOne(d => d.Tr6).WithOne(p => p.PersonnelScore)
                .HasForeignKey<PersonnelScore>(d => d.TrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_personnel_score_personnel_virtures_ethics_score");
        });

        modelBuilder.Entity<PersonnelTeachingScore>(entity =>
        {
            entity.HasKey(e => e.TrId).HasName("PK_teaching_eval_score");

            entity.ToTable("personnel_teaching_score", "ev");

            entity.Property(e => e.TrId)
                .HasMaxLength(14)
                .HasColumnName("tr_id");
            entity.Property(e => e.AdvisorScore15)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("advisor_score_1_5");
            entity.Property(e => e.ColleagueEvalScore14)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("colleague_eval_score_1_4");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.StudentEvalScore13)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("student_eval_score_1_3");
            entity.Property(e => e.TeachingLoadScore112)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("teaching_load_score_1_1_2");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<PersonnelUniSupportScore>(entity =>
        {
            entity.HasKey(e => e.TrId);

            entity.ToTable("personnel_uni_support_score", "ev");

            entity.Property(e => e.TrId)
                .HasMaxLength(14)
                .HasColumnName("tr_id");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.UniSupport81)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("uni_support_8_1");
            entity.Property(e => e.UniSupport82)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("uni_support_8_2");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<PersonnelVirturesEthicsScore>(entity =>
        {
            entity.HasKey(e => e.TrId);

            entity.ToTable("personnel_virtures_ethics_score", "ev");

            entity.Property(e => e.TrId)
                .HasMaxLength(14)
                .HasColumnName("tr_id");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.VirtuesEthicsScore71)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("virtues_ethics_score_7_1");
            entity.Property(e => e.VirtuesEthicsScore72)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("virtues_ethics_score_7_2");
            entity.Property(e => e.VirtuesEthicsScore73)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("virtues_ethics_score_7_3");
            entity.Property(e => e.VirtuesEthicsScore74)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("virtues_ethics_score_7_4");
            entity.Property(e => e.VirtuesEthicsScore75)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("virtues_ethics_score_7_5");
        });

        modelBuilder.Entity<ResearchGrant23>(entity =>
        {
            entity.ToTable("research_grant_2_3", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_research_grant_2_3_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.ContactPeriod)
                .HasMaxLength(45)
                .HasColumnName("contact_period");
            entity.Property(e => e.NumberOfYear)
                .HasMaxLength(45)
                .HasColumnName("number_of_year");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.Position)
                .HasMaxLength(45)
                .HasColumnName("position");
            entity.Property(e => e.Reason)
                .HasDefaultValue("-")
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.ResearchTopic)
                .HasMaxLength(45)
                .HasColumnName("research_topic");
            entity.Property(e => e.Sponsor)
                .HasMaxLength(45)
                .HasColumnName("sponsor");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("W")
                .HasComment("W = Waiting for Approved, A = Approved, R = Rejected")
                .HasColumnName("status");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.ResearchGrant23s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_research_grant_2_3_personnel");
        });

        modelBuilder.Entity<SupportTask7>(entity =>
        {
            entity.HasKey(e => new { e.DocumentNo, e.PersonnelEmpId });

            entity.ToTable("support_task_7", "ev");

            entity.Property(e => e.DocumentNo)
                .HasMaxLength(45)
                .HasColumnName("document_no");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(45)
                .HasColumnName("document_name");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .HasComment("A = Active, N = Non-Active")
                .HasColumnName("status");
            entity.Property(e => e.TaskDetail)
                .HasColumnType("text")
                .HasColumnName("task_detail");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.SupportTask7s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_culture_activity_personnel100");
        });

        modelBuilder.Entity<TeachingDocument21>(entity =>
        {
            entity.ToTable("teaching_document_2_1", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_teaching_document_2_1_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasComment("")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.CoProducer)
                .HasMaxLength(45)
                .HasColumnName("co_producer");
            entity.Property(e => e.DayMonthYear).HasColumnName("day_month_year");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.Reason)
                .HasDefaultValue("-")
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("W")
                .HasComment("W = Waiting for Approved, A = Approved, R = Rejected")
                .HasColumnName("status");
            entity.Property(e => e.Subject)
                .HasMaxLength(45)
                .HasColumnName("subject");
            entity.Property(e => e.TeachingMaterial)
                .HasMaxLength(45)
                .HasColumnName("teaching_material");
            entity.Property(e => e.Type)
                .HasMaxLength(45)
                .HasColumnName("type");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.TeachingDocument21s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_teaching_document_2_1_personnel");
        });

        modelBuilder.Entity<TeachingEvaluation13>(entity =>
        {
            entity.ToTable("teaching_evaluation_1_3", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_teaching_evaluation_1_3_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadSemester)
                .HasMaxLength(2)
                .HasComment("TS = Summer term, T1 = Semester 1, T2 = Semester 2")
                .HasColumnName("acad_semester");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.DayMonthYear).HasColumnName("day_month_year");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.Score)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("score");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .HasComment("A = Active, N = Non active")
                .HasColumnName("status");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(45)
                .HasColumnName("subject_code");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(45)
                .HasColumnName("subject_name");
            entity.Property(e => e.TeachingType)
                .HasMaxLength(3)
                .HasComment("LEC = lecture, LAB = laboratory, THS = Thesis/project/dissertation")
                .HasColumnName("teaching_type");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.TeachingEvaluation13s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_teaching_evaluation_1_3_personnel");
        });

        modelBuilder.Entity<TeachingEvaluation14>(entity =>
        {
            entity.HasKey(e => new { e.SubjectCode, e.AcadSemester, e.AcadYear });

            entity.ToTable("teaching_evaluation_1_4", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_teaching_evaluation_1_4_personnel_emp_id");

            entity.Property(e => e.SubjectCode)
                .HasMaxLength(45)
                .HasColumnName("subject_code");
            entity.Property(e => e.AcadSemester)
                .HasMaxLength(2)
                .HasColumnName("acad_semester");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.DayMonthYear).HasColumnName("day_month_year");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.Score)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("score");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .HasComment("A = Active, N = Non active")
                .HasColumnName("status");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(45)
                .HasColumnName("subject_name");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.TeachingEvaluation14s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_teaching_evaluation_1_4_personnel");
        });

        modelBuilder.Entity<TeachingEvaluationDetails14>(entity =>
        {
            entity.ToTable("teaching_evaluation_details_1_4", "ev");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadSemester)
                .HasMaxLength(2)
                .HasColumnName("acad_semester");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.Score)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("score");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(45)
                .HasColumnName("subject_code");
            entity.Property(e => e.SubjectSection)
                .HasMaxLength(50)
                .HasColumnName("subject_section");

            entity.HasOne(d => d.TeachingEvaluation14).WithMany(p => p.TeachingEvaluationDetails14s)
                .HasForeignKey(d => new { d.SubjectCode, d.AcadSemester, d.AcadYear })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_teaching_evaluation_details_1_4_teaching_evaluation_1_4");
        });

        modelBuilder.Entity<TeachingLoad11>(entity =>
        {
            entity.HasKey(e => new { e.AcadYear, e.AcadSemester }).HasName("PK_teaching_load_1_1_1");

            entity.ToTable("teaching_load_1_1", "ev");

            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.AcadSemester)
                .HasMaxLength(2)
                .HasColumnName("acad_semester");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .HasComment("A = Active, N = Non active")
                .HasColumnName("status");
            entity.Property(e => e.TotalCredit)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("total_credit");
            entity.Property(e => e.TotalCreditLab)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("total_credit_lab");
            entity.Property(e => e.TotalCreditLecture)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("total_credit_lecture");
            entity.Property(e => e.TotalCreditThesis)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("total_credit_thesis");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.TeachingLoad11s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_teaching_load_1_1_personnel1");
        });

        modelBuilder.Entity<TeachingLoadDetails11>(entity =>
        {
            entity.HasKey(e => new { e.SubjectCode, e.AcadYear, e.AcadSemester, e.SubjectSection }).HasName("PK_teaching_load_1_1");

            entity.ToTable("teaching_load_details_1_1", "ev");

            entity.Property(e => e.SubjectCode)
                .HasMaxLength(10)
                .HasColumnName("subject_code");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.AcadSemester)
                .HasMaxLength(2)
                .HasComment("TS = Term Summer, T1 = Term 1, T2 = Term 2")
                .HasColumnName("acad_semester");
            entity.Property(e => e.SubjectSection)
                .HasMaxLength(50)
                .HasColumnName("subject_section");
            entity.Property(e => e.Credit)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("credit");
            entity.Property(e => e.DayMonthYear).HasColumnName("day_month_year");
            entity.Property(e => e.NumberOfStudents).HasColumnName("number_of_students");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .HasColumnName("subject_name");
            entity.Property(e => e.TeachingType)
                .HasMaxLength(3)
                .HasComment("LEC = lecture, LAB = laboratory, THS = project/dissertation/thesis")
                .HasColumnName("teaching_type");

            entity.HasOne(d => d.TeachingLoad11).WithMany(p => p.TeachingLoadDetails11s)
                .HasForeignKey(d => new { d.AcadYear, d.AcadSemester })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_teaching_load_details_1_1_teaching_load_1_1");
        });

        modelBuilder.Entity<Textbook22>(entity =>
        {
            entity.ToTable("textbook_2_2", "ev");

            entity.HasIndex(e => e.PersonnelEmpId, "IX_textbook_2_2_personnel_emp_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.ApprovedEmpId)
                .HasMaxLength(7)
                .HasDefaultValue("-")
                .HasColumnName("approved_emp_id");
            entity.Property(e => e.DayMonthYear).HasColumnName("day_month_year");
            entity.Property(e => e.NameOfWork)
                .HasMaxLength(45)
                .HasColumnName("name_of_work");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.Reason)
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("W")
                .HasComment("W = Waiting for Approved, A = Approved, R = Rejected")
                .HasColumnName("status");
            entity.Property(e => e.TeachingBook)
                .HasMaxLength(45)
                .HasColumnName("teaching_book");
            entity.Property(e => e.Type)
                .HasMaxLength(45)
                .HasColumnName("type");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.Textbook22s)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_textbook_2_2_personnel");
        });

        modelBuilder.Entity<VisaTransaction>(entity =>
        {
            entity.HasKey(e => e.TrId);

            entity.ToTable("visa_transaction", "inter");

            entity.Property(e => e.TrId)
                .HasMaxLength(14)
                .HasComment("transaction id = INT12680000001 (INT: Inter + mm + current year + XXXXXXX)")
                .HasColumnName("tr_id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(50)
                .HasColumnName("passport_number");
            entity.Property(e => e.SchoolEmail)
                .HasMaxLength(50)
                .HasColumnName("school_email");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("I")
                .HasComment("I = In progress, A = approved, R = Rejected")
                .HasColumnName("status");
            entity.Property(e => e.StudentCitizen)
                .HasMaxLength(50)
                .HasColumnName("student_citizen");
            entity.Property(e => e.StudentDname)
                .HasMaxLength(50)
                .HasDefaultValue("-")
                .HasColumnName("student_dname");
            entity.Property(e => e.StudentEmail)
                .HasMaxLength(50)
                .HasColumnName("student_email");
            entity.Property(e => e.StudentFname)
                .HasMaxLength(50)
                .HasColumnName("student_fname");
            entity.Property(e => e.StudentId)
                .HasMaxLength(7)
                .HasColumnName("student_id");
            entity.Property(e => e.StudentLname)
                .HasMaxLength(50)
                .HasColumnName("student_lname");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.VisaDocPickup)
                .HasComment("pickup date = current date + 10 days")
                .HasColumnName("visa_doc_pickup");
            entity.Property(e => e.VisaExpiryDate).HasColumnName("visa_expiry_date");
            entity.Property(e => e.VisaIssueDate).HasColumnName("visa_issue_date");
            entity.Property(e => e.VisaNextExpiryDate).HasColumnName("visa_next_expiry_date");
            entity.Property(e => e.VisaType)
                .HasMaxLength(50)
                .HasColumnName("visa_type");
        });

        modelBuilder.Entity<VisaTransactionStatus>(entity =>
        {
            entity.HasKey(e => e.Ids);

            entity.ToTable("visa_transaction_status", "inter");

            entity.Property(e => e.Ids).HasColumnName("ids");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.Reason)
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.TrDate).HasColumnName("tr_date");
            entity.Property(e => e.TrId)
                .HasMaxLength(14)
                .HasColumnName("tr_id");
            entity.Property(e => e.VisaStatus)
                .HasMaxLength(1)
                .HasComment("S = Submitted, V = Under review, A = Approved document, P = Pickup document, C = Completed  ")
                .HasColumnName("visa_status");

            entity.HasOne(d => d.PersonnelEmp).WithMany(p => p.VisaTransactionStatuses)
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_visa_transaction_status_personnel");

            entity.HasOne(d => d.Tr).WithMany(p => p.VisaTransactionStatuses)
                .HasForeignKey(d => d.TrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_visa_transaction_status_visa_transaction");
        });

        modelBuilder.Entity<Section3Summary>(entity =>
        {
            entity.ToTable("section3_summary", "ev");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcadYear).HasColumnName("acad_year");
            entity.Property(e => e.PersonnelEmpId)
                .HasMaxLength(7)
                .HasColumnName("personnel_emp_id");
            entity.Property(e => e.SummaryComments)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("summary_comments");

            entity.HasOne(d => d.PersonnelEmp)
                .WithMany() // No navigation property back for now unless needed
                .HasForeignKey(d => d.PersonnelEmpId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_section3_summary_personnel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
