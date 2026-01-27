using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSU_360_X.Models_Db;
using RSU_360_X.ViewModels;
using System.Globalization;

namespace RSU_360_X.Controllers
{
    public class BooksController : Controller
    {
        private readonly EvDbContext _db;
        public BooksController(EvDbContext db) => _db = db;

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Index", "Login");

            return View(); // Views/Books/Index.cshtml
        }

        // ✅ JS uses this route name
        [HttpGet]
        [Route("staff/books/list", Name = "Staff_Books_List")]
        public async Task<IActionResult> List()
        {
            var empId = HttpContext.Session.GetString("EmpId");
            if (string.IsNullOrWhiteSpace(empId))
                return Unauthorized("EmpId missing in session.");

            var items = await _db.Textbook22s
                .AsNoTracking()
                .Where(x => x.PersonnelEmpId == empId)
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    name_of_work = x.NameOfWork,
                    teaching_book = x.TeachingBook,
                    type = x.Type,
                    day_month_year = x.DayMonthYear.ToString("yyyy-MM-dd"),
                    status = x.Status
                })
                .ToListAsync();

            return Json(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit([FromForm] BooksVm vm)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return Unauthorized("Not logged in.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid form data.");

            var empId = HttpContext.Session.GetString("EmpId");
            if (string.IsNullOrWhiteSpace(empId))
                return Unauthorized("EmpId missing in session.");

            // ✅ Parse exact yyyy-MM-dd to avoid 1483 issue
            if (!DateOnly.TryParseExact(vm.DayMonthYear, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var dayMonthYear))
                return BadRequest($"Invalid date: {vm.DayMonthYear}");

            // ✅ FK check
            var empExists = await _db.Personnel.AsNoTracking().AnyAsync(p => p.EmpId == empId);
            if (!empExists)
                return BadRequest($"EmpId '{empId}' not found in hr.personnel.");

            static string Cut(string? s, int max)
            {
                s = (s ?? "").Trim();
                return s.Length <= max ? s : s.Substring(0, max);
            }

            var entity = new Textbook22
            {
                NameOfWork = Cut(vm.NameOfWork, 45),
                TeachingBook = Cut(vm.TeachingBook, 45),
                Type = Cut(vm.Type, 45),
                DayMonthYear = dayMonthYear,

                AcadYear = DateTime.Now.Year,
                Reason = "-",
                Status = "W",
                ApprovedEmpId = "-",
                PersonnelEmpId = empId
            };

            _db.Textbook22s.Add(entity);
            await _db.SaveChangesAsync();

            return Ok("Saved");
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
