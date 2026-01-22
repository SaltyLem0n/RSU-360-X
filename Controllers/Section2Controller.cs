using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSU_360_X.Models.ViewModels;
using RSU_360_X.Models_Db;
using Microsoft.EntityFrameworkCore;

namespace RSU_360_X.Controllers
{
    [Route("staff/evaluationform/section-2")]
    public sealed class Section2Controller : Controller
    {
        private readonly EvDbContext _context;

        public Section2Controller(EvDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            // 1. Get Current Employee ID (Lecturer or Staff)
            var id = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index", "Login");

            // 2. Fetch Employee Profile
            var personnel = await _context.Personnel.FirstOrDefaultAsync(p => p.EmpId == id);

            if (personnel == null)
            {
                return RedirectToAction("Error", "Home", new { message = "Employee profile not found." });
            }

            // 3. Automatic Age Calculation
            if (personnel.EmpDob != default(DateOnly))
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                var age = today.Year - personnel.EmpDob.Year;
                // Adjust if birthday hasn't occurred yet this year
                if (personnel.EmpDob > today.AddYears(-age)) age--;

                // Update in-memory for display only if the calculated age is reasonable
                // (e.g. > 18). If calculated age is too low (bad DOB data) and DB has a valid age, keep DB age.
                if (age >= 18 || personnel.EmpAge < 18)
                {
                    personnel.EmpAge = age;
                }
            }

            // 4. Map SQL Entity -> ViewModel
            var vm = new Section2Vm
            {
                EmpId = personnel.EmpId,
                AcademicYear = (DateTime.Now.Year + 543).ToString(), // Thai Year context

                // Read-Only Personal Details (Exact Match)
                EmpFname = personnel.EmpFname,
                EmpLname = personnel.EmpLname,
                EmpAge = personnel.EmpAge,
                EmpHEducation = personnel.EmpHEducation ?? "-",
                EmpStartDate = personnel.EmpStartDate,
                EmpPos = personnel.EmpPos ?? "-",
                EmpDepartment = personnel.EmpDepartment ?? "-",
                EmpFaculty = personnel.EmpFaculty ?? "-",
                EmpAcademicPos = personnel.EmpAcademicPos ?? "-",
                // EmpPos is used for both Position and AdminPosition in VM logic if needed, 
                // but strictly following DB model names now.

                // Map Work Focus (String -> Boolean)
                IsTeachingFocused = personnel.EmpWorkType == "Teaching",
                IsResearchFocused = personnel.EmpWorkType == "Research",
                IsAdministrationFocused = personnel.EmpWorkType == "Administrative",

                // Map the Research Year
                ResearchApprovedYearAcademic = personnel.ResearchApprovedYearAcademic
            };

            // Default to 'Teaching' if no focus is set in the database
            if (!vm.IsTeachingFocused && !vm.IsResearchFocused && !vm.IsAdministrationFocused)
            {
                vm.IsTeachingFocused = true;
            }

            return View(ViewPath, vm);
        }

        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Section2Vm vm)
        {
            var id = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index", "Login");

            if (!ModelState.IsValid) return View(ViewPath, vm);

            // 1. Fetch Employee Record
            var personnel = await _context.Personnel.FirstOrDefaultAsync(p => p.EmpId == id);
            if (personnel == null) return NotFound();

            // 2. Update Work Focus Logic
            if (vm.IsTeachingFocused)
            {
                personnel.EmpWorkType = "Teaching";
                personnel.ResearchApprovedYearAcademic = null; // Clear if not research
            }
            else if (vm.IsResearchFocused)
            {
                personnel.EmpWorkType = "Research";
                personnel.ResearchApprovedYearAcademic = vm.ResearchApprovedYearAcademic;
            }
            else if (vm.IsAdministrationFocused)
            {
                personnel.EmpWorkType = "Administrative";
                personnel.ResearchApprovedYearAcademic = null;
            }

            // 3. Save Changes to Database
            _context.Personnel.Update(personnel);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Personal information updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        private static readonly string ViewPath = "~/Views/Section2/Index.cshtml";
    }
}
