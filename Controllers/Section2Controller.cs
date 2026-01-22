using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSU_360_X.Models_Db;
using RSU_360_X.Models.ViewModels;

namespace RSU_360_X.Controllers
{
    [Route("staff/evaluationform/section-2")]
    public class Section2Controller : Controller
    {
        private readonly EvDbContext _db;

        public Section2Controller(EvDbContext db)
        {
            _db = db;
        }

        private async Task<Section2AcademicVm> LoadVmAsync()
        {
            var empId = HttpContext.Session.GetString("EmpId")
                        ?? HttpContext.Session.GetString("UserId");

            if (string.IsNullOrWhiteSpace(empId))
                throw new UnauthorizedAccessException("EmpId missing in session.");

            var acadYear = DateTime.Now.Year;

            var personnel = await _db.Personnel.AsNoTracking()
                .FirstOrDefaultAsync(p => p.EmpId == empId);

            var vm = new Section2AcademicVm
            {
                EmpId = empId,
                AcadYear = acadYear,
                EmpFname = personnel?.EmpFname ?? "",
                EmpLname = personnel?.EmpLname ?? "",
                EmpFaculty = personnel?.EmpFaculty ?? ""
            };


            vm.TeachingMaterials = await _db.TeachingDocument21s
                .AsNoTracking()
                .Where(x => x.PersonnelEmpId == empId && x.AcadYear == acadYear && x.Status == "A")
                .OrderByDescending(x => x.Id)
                .Select(x => new TeachingMaterialItem
                {
                    Id = x.Id,
                    Subject = x.Subject,
                    TeachingMaterial = x.TeachingMaterial,
                    Type = x.Type,
                    CoProducer = x.CoProducer,
                    DayMonthYear = x.DayMonthYear.ToString("yyyy-MM-dd"),
                })
                .ToListAsync();

            vm.Books = await _db.Textbook22s
                .AsNoTracking()
                .Where(x => x.PersonnelEmpId == empId && x.AcadYear == acadYear && x.Status == "A")
                .OrderByDescending(x => x.Id)
                .Select(x => new BookItem
                {
                    Id = x.Id,
                    NameOfWork = x.NameOfWork,
                    TeachingBook = x.TeachingBook,
                    Type = x.Type,
                    DayMonthYear = x.DayMonthYear.ToString("yyyy-MM-dd"),
                })
                .ToListAsync();

            vm.ResearchGrants = await _db.ResearchGrant23s
                .AsNoTracking()
                .Where(x => x.PersonnelEmpId == empId && x.AcadYear == acadYear && x.Status == "A")
                .OrderByDescending(x => x.Id)
                .Select(x => new ResearchGrantItem
                {
                    Id = x.Id,
                    ResearchTopic = x.ResearchTopic,
                    Position = x.Position,
                    Sponsor = x.Sponsor,
                    NumberOfYear = x.NumberOfYear,
                    ContactPeriod = x.ContactPeriod,
                })
                .ToListAsync();


            vm.Conferences = new List<ConferenceItem>();


            vm.Journals = new List<JournalItem>();

            vm.Patents = await _db.Patent26s
                .AsNoTracking()
                .Where(x => x.PersonnelEmpId == empId && x.AcadYear == acadYear && x.Status == "A")
                .OrderByDescending(x => x.Id)
                .Select(x => new PatentItem
                {
                    Id = x.Id,
                    NameOfWork = x.NameOfWork,
                    CopyrightNumber = x.CopyrightNumber,
                    Type = x.Type,
                    DayMonthYear = x.DayMonthYear.ToString("yyyy-MM-dd"),
                })
                .ToListAsync();

            vm.Creations = await _db.CreativeWork27s
                .AsNoTracking()
                .Where(x => x.PersonnelEmpId == empId && x.AcadYear == acadYear && x.Status == "A")
                .OrderByDescending(x => x.Id)
                .Select(x => new CreationItem
                {
                    Id = x.Id,
                    QualityLevel = x.QualityLevel,
                    Type = x.Type,
                    Description = x.Description,
                    DayMonthYear = x.DayMonthYear.ToString("yyyy-MM-dd"),
                })
                .ToListAsync();

            return vm;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var vm = await LoadVmAsync();
                return View("~/Views/Section2/Index.cshtml", vm);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
