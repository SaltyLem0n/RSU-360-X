using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSU_360_X.Models_Db;
using RSU_360_X.Models.ViewModels;

namespace RSU_360_X.Controllers
{
    [Route("staff/evaluationform/section-3")]
    public sealed class Section3Controller : Controller
    {
        private readonly EvDbContext _context;

        public Section3Controller(EvDbContext context)
        {
            _context = context;
        }

        // --- LOAD DATA ---
        private async Task<AcademicServicesViewModel> LoadViewModelAsync()
        {
            var id = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(id)) throw new UnauthorizedAccessException();

            var acadYear = (DateTime.Now.Year + 543).ToString(); // 2569

            // 1. Get Summary Comments & Personnel
            var summary = await _context.Section3Summaries
                .FirstOrDefaultAsync(s => s.PersonnelEmpId == id && s.AcadYear == int.Parse(acadYear));
            var personnel = await _context.Personnel.FirstOrDefaultAsync(p => p.EmpId == id);

            var vm = new AcademicServicesViewModel
            {
                LecturerId = id,
                AcademicYear = acadYear,
                SummaryComments = summary?.SummaryComments ?? "",
                EmpFname = personnel?.EmpFname ?? "",
                EmpLname = personnel?.EmpLname ?? "",
                EmpFaculty = personnel?.EmpFaculty ?? "",

                // --- 3.1 Academic Service (Projects with Income) ---
                ProjectsWithIncome = (await _context.AcademicService31s
                    .Where(x => x.PersonnelEmpId == id && x.AcadYear == int.Parse(acadYear))
                    .ToListAsync())
                    .Select(x => new ProjectWithIncome
                    {
                        Id = x.Id.ToString(),
                        ProjectName = x.ProjectName ?? "",
                        RecipientAgency = x.ServiceApplicant ?? "", // ServiceApplicant -> RecipientAgency
                        ServiceType = x.TypeOfWork ?? "", // TypeOfWork -> ServiceType
                        RoleInProject = "", // Not in DB
                        StartDate = x.ProjectStartDate != DateOnly.MinValue ? x.ProjectStartDate.ToString("yyyy-MM-dd") : "",
                        UniversityAllocation = decimal.TryParse(x.Fund, out var d) ? d : 0, // Fund (string) -> decimal
                        PaymentDate = "", // Not in DB
                        Remarks = x.Note ?? "" // Note -> Remarks
                    }).ToList(),

                // --- 3.2 QA ---
                QualityAssuranceProjects = await _context.AcademicServiceQa32s
                    .Where(x => x.PersonnelEmpId == id && x.AcadYear == int.Parse(acadYear))
                    .Select(x => new QualityAssuranceProject
                    {
                        Id = x.Id.ToString(),
                        ProjectName = x.ProjectName ?? "",
                        RecipientAgency = x.ServiceApplicant ?? "",
                        RoleInProject = "", // Not in DB
                        Duty = x.OperatingResult ?? "", // OperatingResult -> Duty? (Best guess)
                        StartDate = x.ProjectStartDate != DateOnly.MinValue ? x.ProjectStartDate.ToString("yyyy-MM-dd") : "",
                        Summary = "" // Not in DB
                    }).ToListAsync(),

                // --- 3.3 Speaker ---
                SpeakerActivities = await _context.InviteSpeaker33s
                    .Where(x => x.PersonnelEmpId == id && x.AcadYear == int.Parse(acadYear))
                    .Select(x => new SpeakerActivity
                    {
                        Id = x.Id.ToString(),
                        ActivityName = x.ProjectName ?? "", // ProjectName -> ActivityName
                        Role = x.Role ?? "",
                        Agency = x.ServiceRecipientAgency ?? "", // ServiceRecipientAgency -> Agency
                        Type = "", // Not in DB
                        StartDate = x.StartDate != DateOnly.MinValue ? x.StartDate.ToString("yyyy-MM-dd") : ""
                    }).ToListAsync(),

                // --- 3.4 Expert/Reviewer ---
                ExpertActivities = await _context.InviteReviewer34s
                    .Where(x => x.PersonnelEmpId == id && x.AcadYear == int.Parse(acadYear))
                    .Select(x => new ExpertActivity
                    {
                        Id = x.Id.ToString(),
                        JournalName = x.JournalConferenceName ?? "", // JournalConferenceName -> JournalName
                        ArticleName = x.ArticleTitle ?? "", // ArticleTitle -> ArticleName
                        Agency = x.ServiceApplicant ?? "", // ServiceApplicant -> Agency
                        Type = "", // Not in DB
                        StartDate = x.ProjectStartDate != DateOnly.MinValue ? x.ProjectStartDate.ToString("yyyy-MM-dd") : ""
                    }).ToListAsync(),

                // --- 4. Arts & Culture ---
                ArtsAndCultureActivities = await _context.CultureActivity4s
                    .Where(x => x.PersonnelEmpId == id && x.AcadYear == int.Parse(acadYear))
                    .Select(x => new ArtsAndCultureActivity
                    {
                        Id = x.Id.ToString(),
                        EventName = x.EventName ?? "",
                        Level = "", // Not in DB
                        StartDateTime = x.StartDate.ToDateTime(TimeOnly.MinValue), // DateOnly -> DateTime
                        EndDateTime = x.EndDate.ToDateTime(TimeOnly.MinValue)
                    }).ToListAsync(),

                // --- 5. Other Tasks ---
                OtherTasks = await _context.AssignTask5s
                    .Where(x => x.PersonnelEmpId == id && x.AcadYear == int.Parse(acadYear))
                    .Select(x => new OtherTask
                    {
                        Id = x.Id.ToString(),
                        TaskName = x.AssignedTask ?? "", // AssignedTask -> TaskName
                        AssignedBy = x.AssignedBy ?? "",
                        StartDateTime = x.StartDate.ToDateTime(TimeOnly.MinValue),
                        EndDateTime = x.EndDate.ToDateTime(TimeOnly.MinValue)
                    }).ToListAsync(),

                // --- 6. Admin Work ---
                AdminWork = await _context.AdministrativeTask6s
                    .Where(x => x.PersonnelEmpId == id && x.AcadYear == int.Parse(acadYear))
                    .Select(x => new AdminWork
                    {
                        Id = x.Id.ToString(),
                        TaskName = x.TaskName ?? ""
                    }).ToListAsync(),

                // --- 7. Support Tasks ---
                SupportTasks = await _context.SupportTask7s
                    .Where(x => x.PersonnelEmpId == id && x.AcadYear == int.Parse(acadYear))
                    .Select(x => new SupportTask
                    {
                        Id = x.Id.ToString(),
                        TaskName = x.DocumentName ?? ""
                    }).ToListAsync(),

                // --- 8. Personnel Development ---
                PersonnelDevelopments = await _context.PersonnelDevelopment8s
                    .Where(x => x.PersonnelEmpId == id && x.AcadYear == int.Parse(acadYear))
                    .Select(x => new PersonnelDevelopmentItem
                    {
                        Id = x.Id.ToString(),
                        TopicName = x.TopicName ?? "",
                        Type = x.Type ?? "",
                        StartDate = x.StartDate != DateOnly.MinValue ? x.StartDate.ToString("yyyy-MM-dd") : "",
                        EndDate = x.EndDate != DateOnly.MinValue ? x.EndDate.ToString("yyyy-MM-dd") : "",
                        Organizers = x.Organizers ?? "",
                        Status = x.Status ?? "A"
                    }).ToListAsync(),
            };

            return vm;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try { return View(ViewPath, await LoadViewModelAsync()); }
            catch (UnauthorizedAccessException) { return RedirectToAction("Index", "Login"); }
        }

        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AcademicServicesViewModel vm)
        {
            var id = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index", "Login");

            var acadYear = (DateTime.Now.Year + 543).ToString(); // 2569
            var summary = await _context.Section3Summaries
                .FirstOrDefaultAsync(s => s.PersonnelEmpId == id && s.AcadYear == int.Parse(acadYear));

            if (summary == null)
            {
                summary = new Section3Summary
                {
                    PersonnelEmpId = id,
                    AcadYear = int.Parse(acadYear),
                    SummaryComments = vm.SummaryComments ?? ""
                };
                _context.Section3Summaries.Add(summary);
            }
            else
            {
                summary.SummaryComments = vm.SummaryComments ?? "";
            }

            await _context.SaveChangesAsync();
            return Redirect("/staff/evaluationform/section4/evaluation-table1");
        }

        // --- API ACTIONS (Examples) ---

        // 3.1
        [HttpPost("api/projectswithincome")]
        public async Task<IActionResult> AddProject([FromBody] ProjectWithIncome item)
        {
            var id = GetUserId(); if (id == null) return ApiUnauthorized();
            var sqlItem = new AcademicService31
            {
                PersonnelEmpId = id,
                AcadYear = int.Parse(GetYear()),
                ProjectName = item.ProjectName ?? "",
                ServiceApplicant = item.RecipientAgency ?? "",
                TypeOfWork = item.ServiceType ?? "",
                // RoleInProject missing
                ProjectStartDate = DateOnly.Parse(item.StartDate ?? DateTime.Now.ToString("yyyy-MM-dd")),
                Fund = item.UniversityAllocation.ToString() ?? "0",
                Note = item.Remarks ?? "",
                Status = "A",
                ApprovedEmpId = "-"
            };
            _context.AcademicService31s.Add(sqlItem);
            await _context.SaveChangesAsync();
            item.Id = sqlItem.Id.ToString();
            return Ok(new { success = true, item });
        }

        [HttpDelete("api/projectswithincome/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var item = await _context.AcademicService31s.FindAsync(id);
            if (item != null) { _context.AcademicService31s.Remove(item); await _context.SaveChangesAsync(); }
            return Ok(new { success = true });
        }

        // --- Helpers ---
        private string? GetUserId() => HttpContext.Session.GetString("UserId");
        private string GetYear() => (DateTime.Now.Year + 543).ToString();
        private IActionResult ApiUnauthorized() => Unauthorized(new { success = false, redirect = "/login" });
        private static readonly string ViewPath = "~/Views/Section3/Index.cshtml";
    }
}
