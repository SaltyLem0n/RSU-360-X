using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSU_360_X.Models_Db;
using RSU_360_X.ViewModels;

namespace RSU_360_X.Controllers
{
    public class CreationController : Controller
    {
        private readonly EvDbContext _db;

        public CreationController(EvDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Index", "Login");

            return View(); // Views/Creation/Index.cshtml
        }

        // ✅ Used by JS: @Url.RouteUrl("Staff_Academic_Creation_List")
        [HttpGet]
        [Route("staff/creation/list", Name = "Staff_Academic_Creation_List")]
        public async Task<IActionResult> List()
        {
            var empId = HttpContext.Session.GetString("EmpId");
            if (string.IsNullOrWhiteSpace(empId))
                return Unauthorized("EmpId missing in session.");

            var items = await _db.CreativeWork27s
                .AsNoTracking()
                .Where(x => x.PersonnelEmpId == empId)
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    qualityLevel = x.QualityLevel,
                    type = x.Type,
                    day_month_year = x.DayMonthYear.ToString("yyyy-MM-dd"),
                    description = x.Description,
                    status = x.Status
                })
                .ToListAsync();

            return Json(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitCreation([FromForm] CreativeWorkVm vm)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return Unauthorized("Not logged in.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid form data.");

            var empId = HttpContext.Session.GetString("EmpId");
            if (string.IsNullOrWhiteSpace(empId))
                return Unauthorized("EmpId missing in session.");

            // FK check
            var empExists = await _db.Personnel
                .AsNoTracking()
                .AnyAsync(p => p.EmpId == empId);

            if (!empExists)
                return BadRequest($"EmpId '{empId}' not found in hr.personnel.");

            static string Cut(string? s, int max)
            {
                s = (s ?? "").Trim();
                return s.Length <= max ? s : s.Substring(0, max);
            }

            try
            {
                var entity = new CreativeWork27
                {
                    QualityLevel = Cut(vm.QualityLevel, 45),
                    Type = Cut(vm.Type, 45),
                    DayMonthYear = vm.DayMonthYear,
                    Description = Cut(vm.Description, 45),

                    AcadYear = DateTime.Now.Year,
                    Reason = "-",
                    Status = "W",          // Pending by default
                    ApprovedEmpId = "-",
                    PersonnelEmpId = empId
                };

                _db.CreativeWork27s.Add(entity);
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
