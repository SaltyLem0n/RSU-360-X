using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSU_360_X.Models_Db;
using RSU_360_X.ViewModels;
using System.Globalization;

namespace RSU_360_X.Controllers
{
    public class TeachingMaterialController : Controller
    {
        private readonly EvDbContext _db;

        public TeachingMaterialController(EvDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // ✅ List stored records (only own EmpId)
        [HttpGet]
        [Route("staff/teaching-material/list", Name = "Staff_TeachingMaterial_List")]
        public async Task<IActionResult> List()
        {
            var empId = HttpContext.Session.GetString("EmpId");
            if (string.IsNullOrWhiteSpace(empId))
                return Unauthorized("EmpId missing in session.");

            var items = await _db.TeachingDocument21s
                .AsNoTracking()
                .Where(x => x.PersonnelEmpId == empId)
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    subject = x.Subject,
                    teaching_material = x.TeachingMaterial,
                    day_month_year = x.DayMonthYear.ToString("yyyy-MM-dd"),
                    type = x.Type,
                    co_producer = x.CoProducer,
                    status = x.Status
                })
                .ToListAsync();

            return Json(items);
        }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit()
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return Unauthorized("Not logged in.");

        var empId = HttpContext.Session.GetString("EmpId");
        if (string.IsNullOrWhiteSpace(empId))
            return Unauthorized("EmpId missing in session.");

        var subject = (Request.Form["Subject"].ToString() ?? "").Trim();
        var teachingMaterial = (Request.Form["TeachingMaterial"].ToString() ?? "").Trim();
        var type = (Request.Form["Type"].ToString() ?? "").Trim();
        var coProducer = (Request.Form["CoProducer"].ToString() ?? "").Trim();
        var dateStr = (Request.Form["DayMonthYear"].ToString() ?? "").Trim(); // yyyy-MM-dd

        if (string.IsNullOrWhiteSpace(subject) ||
            string.IsNullOrWhiteSpace(teachingMaterial) ||
            string.IsNullOrWhiteSpace(type) ||
            string.IsNullOrWhiteSpace(dateStr))
            return BadRequest("Missing required fields.");

        var allowedTypes = new[] { "new", "modify" };
        if (!allowedTypes.Contains(type))
            return BadRequest("Invalid type value.");

        // ✅ prevents 1483
        if (!DateOnly.TryParseExact(dateStr, "yyyy-MM-dd",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out var dayMonthYear))
            return BadRequest($"Invalid date format: {dateStr}");

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
            var entity = new TeachingDocument21
            {
                Subject = Cut(subject, 45),
                TeachingMaterial = Cut(teachingMaterial, 45),
                DayMonthYear = dayMonthYear,
                Type = Cut(type, 45),
                CoProducer = string.IsNullOrWhiteSpace(coProducer) ? "-" : Cut(coProducer, 45),

                AcadYear = DateTime.Now.Year,
                Reason = "-",
                Status = "W",
                ApprovedEmpId = "-",
                PersonnelEmpId = empId
            };

            _db.TeachingDocument21s.Add(entity);
            await _db.SaveChangesAsync();

            return Ok("Saved");
        }
        catch (DbUpdateException ex)
        {
            var msg = ex.InnerException?.Message ?? ex.Message;
            return StatusCode(500, msg);
        }
    }


    // Debug session
    [HttpGet]
        public IActionResult TestSession()
        {
            var empId = HttpContext.Session.GetString("EmpId");
            var userId = HttpContext.Session.GetString("UserId");
            return Content($"UserId={userId ?? "NULL"} | EmpId={empId ?? "NULL"}");
        }
    }
}
