using System;
using System.Collections.Generic;

namespace RSU_360_X.Models.ViewModels
{
    public class Section2AcademicVm
    {
        public string? EmpId { get; set; }
        public string? EmpFname { get; set; }
        public string? EmpLname { get; set; }
        public string? EmpFaculty { get; set; }
        public int AcadYear { get; set; }

        public List<TeachingMaterialItem> TeachingMaterials { get; set; } = new();
        public List<BookItem> Books { get; set; } = new();
        public List<ResearchGrantItem> ResearchGrants { get; set; } = new();
        public List<ConferenceItem> Conferences { get; set; } = new();
        public List<JournalItem> Journals { get; set; } = new();
        public List<PatentItem> Patents { get; set; } = new();
        public List<CreationItem> Creations { get; set; } = new();
    }

    public class TeachingMaterialItem
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? TeachingMaterial { get; set; }
        public string? Type { get; set; }
        public string? CoProducer { get; set; }
        public string? DayMonthYear { get; set; } 
        public string? Status { get; set; }  
    }

    public class BookItem
    {
        public int Id { get; set; }
        public string? NameOfWork { get; set; }
        public string? TeachingBook { get; set; }
        public string? Type { get; set; }
        public string? DayMonthYear { get; set; } 
        public string? Status { get; set; }    
    }

    public class ResearchGrantItem
    {
        public int Id { get; set; }
        public string? ResearchTopic { get; set; }
        public string? Position { get; set; }
        public string? Sponsor { get; set; }
        public string? NumberOfYear { get; set; }
        public string? ContactPeriod { get; set; }
        public string? Status { get; set; }     
    }

    public class ConferenceItem
    {
        public int Id { get; set; }
        public string? MeetingName { get; set; }
        public string? ArticleTitle { get; set; }
        public string? Authors { get; set; }
        public string? DayMonthYear { get; set; }
        public int PublishYear { get; set; }
        public string? Country { get; set; }
        public string? Status { get; set; }
    }


    public class JournalItem
    {
        public int Id { get; set; }
        public string? ArticleTitle { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public int YearPublication { get; set; }
        public int MonthPublication { get; set; }
        public string? Doi { get; set; }
        public string? Status { get; set; }
    }

    public class PatentItem
    {
        public int Id { get; set; }
        public string? NameOfWork { get; set; }
        public string? CopyrightNumber { get; set; }
        public string? Type { get; set; }
        public string? DayMonthYear { get; set; } 
        public string? Status { get; set; }    
    }

    public class CreationItem
    {
        public int Id { get; set; }
        public string? QualityLevel { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? DayMonthYear { get; set; } 
        public string? Status { get; set; }   
    }
}
