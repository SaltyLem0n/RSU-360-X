using System;
using System.Collections.Generic;

namespace RSU_360_X.Models_Db;

public partial class Personnel
{
    public string EmpId { get; set; } = null!;

    public string EmpFname { get; set; } = null!;

    public string EmpLname { get; set; } = null!;

    /// <summary>
    /// Only school email (@rsu.ac.th)
    /// </summary>
    public string EmpEmail { get; set; } = null!;

    public DateOnly EmpDob { get; set; }

    public int EmpAge { get; set; }

    /// <summary>
    /// PhD / Master / Bachelor
    /// </summary>
    public string EmpHEducation { get; set; } = null!;

    public DateOnly EmpStartDate { get; set; }

    /// <summary>
    /// L = lecturers, S = staffs
    /// </summary>
    public string EmpType { get; set; } = null!;

    public string? EmpWorkType { get; set; }

    /// <summary>
    /// Head of Program, Director, Dean, Associated Dean
    /// </summary>
    public string EmpPos { get; set; } = null!;

    public string EmpDeptId { get; set; } = null!;

    public string EmpDepartment { get; set; } = null!;

    public string EmpFacultyId { get; set; } = null!;

    public string EmpFaculty { get; set; } = null!;

    public string EmpOfficeId { get; set; } = null!;

    public string EmpOffice { get; set; } = null!;

    public string EmpAcademicPos { get; set; } = null!;

    public string EmpLineWork { get; set; } = null!;

    public virtual ICollection<AcademicArticle24> AcademicArticle24s { get; set; } = new List<AcademicArticle24>();

    public virtual ICollection<AcademicArticle25> AcademicArticle25s { get; set; } = new List<AcademicArticle25>();

    public virtual ICollection<AcademicService31> AcademicService31s { get; set; } = new List<AcademicService31>();

    public virtual ICollection<AcademicServiceQa32> AcademicServiceQa32s { get; set; } = new List<AcademicServiceQa32>();

    public virtual ICollection<AdministrativeTask6> AdministrativeTask6s { get; set; } = new List<AdministrativeTask6>();

    public virtual ICollection<Advisor15> Advisor15s { get; set; } = new List<Advisor15>();

    public virtual ICollection<AssignTask5> AssignTask5s { get; set; } = new List<AssignTask5>();

    public virtual ICollection<CreativeWork27> CreativeWork27s { get; set; } = new List<CreativeWork27>();

    public virtual ICollection<CultureActivity4> CultureActivity4s { get; set; } = new List<CultureActivity4>();

    public virtual DataCentricUser? DataCentricUser { get; set; }

    public virtual ICollection<InviteReviewer34> InviteReviewer34s { get; set; } = new List<InviteReviewer34>();

    public virtual ICollection<InviteSpeaker33> InviteSpeaker33s { get; set; } = new List<InviteSpeaker33>();

    public virtual ICollection<Patent26> Patent26s { get; set; } = new List<Patent26>();

    public virtual ICollection<PersonnelScore> PersonnelScores { get; set; } = new List<PersonnelScore>();

    public virtual ICollection<ResearchGrant23> ResearchGrant23s { get; set; } = new List<ResearchGrant23>();

    public virtual ICollection<SupportTask7> SupportTask7s { get; set; } = new List<SupportTask7>();

    public virtual ICollection<TeachingDocument21> TeachingDocument21s { get; set; } = new List<TeachingDocument21>();

    public virtual ICollection<TeachingEvaluation13> TeachingEvaluation13s { get; set; } = new List<TeachingEvaluation13>();

    public virtual ICollection<TeachingEvaluation14> TeachingEvaluation14s { get; set; } = new List<TeachingEvaluation14>();

    public virtual ICollection<TeachingLoad11> TeachingLoad11s { get; set; } = new List<TeachingLoad11>();

    public virtual ICollection<Textbook22> Textbook22s { get; set; } = new List<Textbook22>();

    public string? ResearchApprovedYearAcademic { get; set; }




}
