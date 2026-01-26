using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSU_360_X.Models_Db;
using RSU_360_X.ViewModels;

namespace RSU_360_X.Controllers
{
    public class ResearchGrantController : Controller
    {
        private readonly EvDbContext _db;

        public ResearchGrantController(EvDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Index", "Login");

            return View(); // Views/ResearchGrant/Index.cshtml
        }

        // ✅ List stored records for current EmpId
        [HttpGet]
        [Route("staff/research-grant/list", Name = "Staff_ResearchGrant_List")]
        public async Task<IActionResult> List()
        {
            var empId = HttpContext.Session.GetString("EmpId");
            if (string.IsNullOrWhiteSpace(empId))
                return Unauthorized("EmpId missing in session.");

            var items = await _db.ResearchGrant23s
                .AsNoTracking()
                .Where(x => x.PersonnelEmpId == empId)
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    research_topic = x.ResearchTopic,
                    position = x.Position,
                    sponsor = x.Sponsor,
                    number_of_year = x.NumberOfYear,
                    contact_period = x.ContactPeriod,
                    status = x.Status
                })
                .ToListAsync();

            return Json(items);
        }

        // ✅ Submit (ONLY ONE)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit([FromForm] ResearchGrantVm vm)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return Unauthorized("Not logged in.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid form data.");

            var empId = HttpContext.Session.GetString("EmpId");
            if (string.IsNullOrWhiteSpace(empId))
                return Unauthorized("EmpId missing in session.");

            // FK check
            var empExists = await _db.Personnel.AsNoTracking().AnyAsync(p => p.EmpId == empId);
            if (!empExists)
                return BadRequest($"EmpId '{empId}' not found in hr.personnel.");

            static string Cut(string? s, int max)
            {
                s = (s ?? "").Trim();
                return s.Length <= max ? s : s.Substring(0, max);
            }

            try
            {
                var entity = new ResearchGrant23
                {
                    ResearchTopic = Cut(vm.ResearchTopic, 45),
                    Position = Cut(vm.Position, 45),
                    Sponsor = Cut(vm.Sponsor, 45),
                    NumberOfYear = Cut(vm.NumberOfYear, 45),
                    ContactPeriod = Cut(vm.ContactPeriod, 45),

                    AcadYear = DateTime.Now.Year,
                    Reason = "-",
                    Status = "W",
                    ApprovedEmpId = "-",
                    PersonnelEmpId = empId
                };

                _db.ResearchGrant23s.Add(entity);
                await _db.SaveChangesAsync();

                return Ok("Saved");
            }
            catch (DbUpdateException ex)
            {
                var msg = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, msg);
            }
        }

        // optional debug
        [HttpGet]
        public IActionResult TestSession()
        {
            var empId = HttpContext.Session.GetString("EmpId");
            var userId = HttpContext.Session.GetString("UserId");
            return Content($"UserId={userId ?? "NULL"} | EmpId={empId ?? "NULL"}");
        }
    }
}
