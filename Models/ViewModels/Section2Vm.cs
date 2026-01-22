using System;

namespace RSU_360_X.Models.ViewModels
{
    public class Section2Vm
    {
        public string? EmpId { get; set; } // Was LecturerId
        public string? AcademicYear { get; set; }

        // Read-Only Fields matching DB Model
        public string? EmpFname { get; set; } // Was FirstName
        public string? EmpLname { get; set; } // Was LastName
        public int EmpAge { get; set; } // Was AgeYears
        public string? EmpHEducation { get; set; } // Was HighestDegree
        public DateOnly EmpStartDate { get; set; } // Was StartDate
        public string? EmpPos { get; set; } // Used for Position & AdminPosition
        public string? EmpDepartment { get; set; } // Was Department
        public string? EmpFaculty { get; set; } // Was FacultyOrCenter
        public string? EmpAcademicPos { get; set; } // Was AcademicRank

        // Logic Fields (Checkboxes)
        public bool IsTeachingFocused { get; set; }
        public bool IsResearchFocused { get; set; }
        public bool IsAdministrationFocused { get; set; }

        // New Input Field
        public string? ResearchApprovedYearAcademic { get; set; } // Was ResearchApprovedAcademicYear
    }
}
