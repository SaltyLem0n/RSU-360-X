using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSU_360_X.Models_Db;
using RSU_360_X.ViewModels;
using System.Globalization;

namespace RSU_360_X.Controllers
{
    public class PatentController : Controller
    {
        private readonly EvDbContext _db;

        public PatentController(EvDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Index", "Login");

            return View(); // Views/Patent/Index.cshtml
        }

        // ✅ List (for Stored Submissions table)
        [HttpGet]
        [Route("staff/patent/list", Name = "Staff_Patent_List")]
        public async Task<IActionResult> List()
        {
            var empId = HttpContext.Session.GetString("EmpId");
            if (string.IsNullOrWhiteSpace(empId))
                return Unauthorized("EmpId missing in session.");

            var items = await _db.Patent26s
                .AsNoTracking()
                .Where(x => x.PersonnelEmpId == empId)
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    name_of_work = x.NameOfWork,
                    copyright_number = x.CopyrightNumber,
                    type = x.Type,
                    day_month_year = x.DayMonthYear.ToString("yyyy-MM-dd"),
                    status = x.Status
                })
                .ToListAsync();

            return Json(items);
        }

        // ✅ Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit([FromForm] PatentVm vm)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return Unauthorized("Not logged in.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid form data.");

            var empId = HttpContext.Session.GetString("EmpId");
            if (string.IsNullOrWhiteSpace(empId))
                return Unauthorized("EmpId missing in session.");

            // ✅ Parse exact yyyy-MM-dd (prevents Buddhist calendar -> 1483)
            if (!DateOnly.TryParseExact(vm.DayMonthYear, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var day))
                return BadRequest($"Invalid date: {vm.DayMonthYear}");

            // ✅ Ensure FK exists
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
                var entity = new Patent26
                {
                    NameOfWork = Cut(vm.NameOfWork, 45),
                    CopyrightNumber = Cut(vm.CopyrightNumber, 45),
                    Type = Cut(vm.Type, 45),
                    DayMonthYear = day,

                    AcadYear = DateTime.Now.Year,
                    Reason = "-",
                    Status = "W",
                    ApprovedEmpId = "-",
                    PersonnelEmpId = empId
                };

                _db.Patent26s.Add(entity);
                await _db.SaveChangesAsync();

                return Ok("Saved");
            }
            catch (DbUpdateException ex)
            {
                var msg = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, msg);
            }
        }

        // Optional debug
        [HttpGet]
        public IActionResult TestSession()
        {
            var empId = HttpContext.Session.GetString("EmpId");
            var userId = HttpContext.Session.GetString("UserId");
            return Content($"UserId={userId ?? "NULL"} | EmpId={empId ?? "NULL"}");
        }
    }
}
