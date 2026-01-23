using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RSU_360_X.Models_Db.DbContexts;

public partial class VisaDbContext : DbContext
{
    public VisaDbContext()
    {
    }

    public VisaDbContext(DbContextOptions<VisaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<VisaTransaction> VisaTransactions { get; set; }

    public virtual DbSet<VisaTransactionStatus> VisaTransactionStatuses { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

            // Decouple Personnel: Ignore the navigation property
            entity.Ignore(e => e.PersonnelEmp);

            entity.HasOne(d => d.Tr).WithMany(p => p.VisaTransactionStatuses)
                .HasForeignKey(d => d.TrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_visa_transaction_status_visa_transaction");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
