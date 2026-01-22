using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSU_360_X.Models_Db;
using RSU_360_X.ViewModels;

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

        // ✅ Submit record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit([FromForm] TeachingMaterialVm vm)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return Unauthorized("Not logged in.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid form data.");

            var empId = HttpContext.Session.GetString("EmpId");
            if (string.IsNullOrWhiteSpace(empId))
                return Unauthorized("EmpId missing in session.");

            // ✅ Ensure FK exists in hr.personnel
            var empExists = await _db.Personnel.AsNoTracking().AnyAsync(p => p.EmpId == empId);
            if (!empExists)
                return BadRequest($"EmpId '{empId}' not found in hr.personnel.");

            // ✅ Avoid string too long error
            static string Cut(string? s, int max)
            {
                s = (s ?? "").Trim();
                return s.Length <= max ? s : s.Substring(0, max);
            }

            try
            {
                var entity = new TeachingDocument21
                {
                    Subject = Cut(vm.Subject, 45),
                    TeachingMaterial = Cut(vm.TeachingMaterial, 45),

                    // ✅ FIX: DateOnly save directly
                    DayMonthYear = vm.DayMonthYear,

                    Type = Cut(vm.Type, 45),
                    CoProducer = string.IsNullOrWhiteSpace(vm.CoProducer) ? "-" : Cut(vm.CoProducer, 45),

                    AcadYear = DateTime.Now.Year,
                    Reason = "-",
                    Status = "W",          // ✅ Default Pending
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
