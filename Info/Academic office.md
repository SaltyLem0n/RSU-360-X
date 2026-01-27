Academic office



ผมทำตัวอย่างโค้ด EF Core สำหรับ ASP.NET Core MVC ให้ครบชุดตาม ER-Diagram นี้นะครับ
(ตาราง: [hr].[personnel], [ev].[teaching_document_2_1], [ev].[textbook_2_2])
หมายเหตุสำคัญ
ในรูปไม่เห็นชื่อ Primary Key ของ teaching_document_2_1 และ textbook_2_2 ผมจะ สมมติ ว่ามีฟิลด์ id (int, identity) เป็น PK
ถ้าของจริงใช้ชื่ออื่น ให้เปลี่ยนชื่อใน model และ HasKey() ตามจริงได้เลยครับ

1. appsettings.json (connection string)
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SQL_SERVER;Database=YOUR_DB_NAME;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}

2. Program.cs (หรือ Startup.cs)
using Microsoft.EntityFrameworkCore;
using YourApp.Data;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

3. Entity Classes (Models)
3.1 Personnel (ตรงกลาง)
namespace YourApp.Models
{
    public class Personnel
    {
        public int EmpId { get; set; }          // emp_id
        public string EmpFname { get; set; }    // emp_fname
        public string EmpLname { get; set; }    // emp_lname
        public string EmpEmail { get; set; }    // emp_email
        public DateTime? EmpDob { get; set; }   // emp_dob
        public int? EmpAge { get; set; }        // emp_age
        public string EmpHEducation { get; set; }   // emp_h_education
        public DateTime? EmpStartDate { get; set; } // emp_start_date
        public string EmpType { get; set; }         // emp_type
        public string EmpPos { get; set; }          // emp_pos
        public int? EmpDeptId { get; set; }         // emp_dept_id
        public string EmpDepartment { get; set; }   // emp_department
        public int? EmpFacultyId { get; set; }      // emp_faculty_id
        public string EmpFaculty { get; set; }      // emp_faculty
        public int? EmpOfficeId { get; set; }       // emp_office_id
        public string EmpOffice { get; set; }       // emp_office
        public string EmpAcademicPos { get; set; }  // emp_academic_pos
        public string EmpLineWork { get; set; }     // emp_line_work

        // Navigation
        public ICollection<TeachingDocument> TeachingDocuments { get; set; }
        public ICollection<Textbook> Textbooks { get; set; }
    }
}
3.2 Teaching Document (ซ้าย)
namespace YourApp.Models
{
    public class TeachingDocument
    {
        // *** สมมติให้มี PK ชื่อ Id ***
        public int Id { get; set; }              // (PK - แก้ให้ตรงของจริง)

        public string Subject { get; set; }      // subject
        public string TeachingMaterial { get; set; } // teaching_material
        public DateTime? DayMonthYear { get; set; }  // day_month_year
        public string Type { get; set; }         // type
        public string CoProducer { get; set; }   // co_producer
        public string AcadYear { get; set; }     // acad_year
        public string Reason { get; set; }       // reason
        public string Status { get; set; }       // status

        public int? ApprovedEmpId { get; set; }  // approved_emp_id
        public int PersonnelEmpId { get; set; }  // personnel_emp_id  (FK -> personnel.emp_id)

        // Navigation
        public Personnel Personnel { get; set; }
        public Personnel ApprovedBy { get; set; } // ถ้า approved_emp_id ก็ FK มาที่ personnel เช่นกัน
    }
}
3.3 Textbook (ขวา)
namespace YourApp.Models
{
    public class Textbook
    {
        // *** สมมติให้มี PK ชื่อ Id ***
        public int Id { get; set; }                  // (PK - แก้ให้ตรงของจริง)

        public string NameOfWork { get; set; }       // name_of_work
        public string TeachingBook { get; set; }     // teaching_book
        public string Type { get; set; }             // type
        public DateTime? DayMonthYear { get; set; }  // day_month_year
        public string AcadYear { get; set; }         // acad_year
        public string Reason { get; set; }           // reason
        public string Status { get; set; }           // status

        public int? ApprovedEmpId { get; set; }      // approved_emp_id
        public int PersonnelEmpId { get; set; }      // personnel_emp_id

        // Navigation
        public Personnel Personnel { get; set; }
        public Personnel ApprovedBy { get; set; }
    }
}

4. DbContext + Mapping (Fluent API)
using Microsoft.EntityFrameworkCore;
using YourApp.Models;

namespace YourApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<TeachingDocument> TeachingDocuments { get; set; }
        public DbSet<Textbook> Textbooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ===== personnel (hr) =====
            modelBuilder.Entity<Personnel>(entity =>
            {
                entity.ToTable("personnel", "hr");

                entity.HasKey(e => e.EmpId);

                entity.Property(e => e.EmpId).HasColumnName("emp_id");
                entity.Property(e => e.EmpFname).HasColumnName("emp_fname").HasMaxLength(100);
                entity.Property(e => e.EmpLname).HasColumnName("emp_lname").HasMaxLength(100);
                entity.Property(e => e.EmpEmail).HasColumnName("emp_email").HasMaxLength(200);
                entity.Property(e => e.EmpDob).HasColumnName("emp_dob");
                entity.Property(e => e.EmpAge).HasColumnName("emp_age");
                entity.Property(e => e.EmpHEducation).HasColumnName("emp_h_education").HasMaxLength(200);
                entity.Property(e => e.EmpStartDate).HasColumnName("emp_start_date");
                entity.Property(e => e.EmpType).HasColumnName("emp_type").HasMaxLength(50);
                entity.Property(e => e.EmpPos).HasColumnName("emp_pos").HasMaxLength(100);
                entity.Property(e => e.EmpDeptId).HasColumnName("emp_dept_id");
                entity.Property(e => e.EmpDepartment).HasColumnName("emp_department").HasMaxLength(200);
                entity.Property(e => e.EmpFacultyId).HasColumnName("emp_faculty_id");
                entity.Property(e => e.EmpFaculty).HasColumnName("emp_faculty").HasMaxLength(200);
                entity.Property(e => e.EmpOfficeId).HasColumnName("emp_office_id");
                entity.Property(e => e.EmpOffice).HasColumnName("emp_office").HasMaxLength(200);
                entity.Property(e => e.EmpAcademicPos).HasColumnName("emp_academic_pos").HasMaxLength(200);
                entity.Property(e => e.EmpLineWork).HasColumnName("emp_line_work").HasMaxLength(200);

                entity.HasMany(e => e.TeachingDocuments)
                      .WithOne(td => td.Personnel)
                      .HasForeignKey(td => td.PersonnelEmpId)
                      .HasConstraintName("FK_TeachingDocument_Personnel");

                entity.HasMany(e => e.Textbooks)
                      .WithOne(t => t.Personnel)
                      .HasForeignKey(t => t.PersonnelEmpId)
                      .HasConstraintName("FK_Textbook_Personnel");
            });

            // ===== teaching_document_2_1 (ev) =====
            modelBuilder.Entity<TeachingDocument>(entity =>
            {
                entity.ToTable("teaching_document_2_1", "ev");

                // *** เปลี่ยนชื่อ/ฟิลด์ใน HasKey ถ้าของจริงไม่ใช่ "id" ***
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Subject).HasColumnName("subject").HasMaxLength(500);
                entity.Property(e => e.TeachingMaterial).HasColumnName("teaching_material").HasMaxLength(500);
                entity.Property(e => e.DayMonthYear).HasColumnName("day_month_year");
                entity.Property(e => e.Type).HasColumnName("type").HasMaxLength(50);
                entity.Property(e => e.CoProducer).HasColumnName("co_producer").HasMaxLength(200);
                entity.Property(e => e.AcadYear).HasColumnName("acad_year").HasMaxLength(20);
                entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(500);
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(50);
                entity.Property(e => e.ApprovedEmpId).HasColumnName("approved_emp_id");
                entity.Property(e => e.PersonnelEmpId).HasColumnName("personnel_emp_id");

                // FK -> personnel.emp_id (เจ้าของผลงาน)
                entity.HasOne(d => d.Personnel)
                      .WithMany(p => p.TeachingDocuments)
                      .HasForeignKey(d => d.PersonnelEmpId)
                      .OnDelete(DeleteBehavior.Restrict);

                // FK -> personnel.emp_id (ผู้อนุมัติ) – ถ้ามีจริง
                entity.HasOne(d => d.ApprovedBy)
                      .WithMany()
                      .HasForeignKey(d => d.ApprovedEmpId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ===== textbook_2_2 (ev) =====
            modelBuilder.Entity<Textbook>(entity =>
            {
                entity.ToTable("textbook_2_2", "ev");

                entity.HasKey(e => e.Id);       // แก้ให้ตรงของจริง
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NameOfWork).HasColumnName("name_of_work").HasMaxLength(500);
                entity.Property(e => e.TeachingBook).HasColumnName("teaching_book").HasMaxLength(500);
                entity.Property(e => e.Type).HasColumnName("type").HasMaxLength(50);
                entity.Property(e => e.DayMonthYear).HasColumnName("day_month_year");
                entity.Property(e => e.AcadYear).HasColumnName("acad_year").HasMaxLength(20);
                entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(500);
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(50);
                entity.Property(e => e.ApprovedEmpId).HasColumnName("approved_emp_id");
                entity.Property(e => e.PersonnelEmpId).HasColumnName("personnel_emp_id");

                entity.HasOne(d => d.Personnel)
                      .WithMany(p => p.Textbooks)
                      .HasForeignKey(d => d.PersonnelEmpId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ApprovedBy)
                      .WithMany()
                      .HasForeignKey(d => d.ApprovedEmpId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

5. ตัวอย่าง Controller สำหรับอ่านข้อมูล
5.1 TeachingDocumentsController
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourApp.Data;

namespace YourApp.Controllers
{
    public class TeachingDocumentsController : Controller
    {
        private readonly AppDbContext _context;

        public TeachingDocumentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /TeachingDocuments
        public async Task<IActionResult> Index()
        {
            var docs = await _context.TeachingDocuments
                .Include(t => t.Personnel)   // join hr.personnel
                .Include(t => t.ApprovedBy)  // ผู้อนุมัติ (ถ้าต้องการ)
                .ToListAsync();

            return View(docs);
        }

        // GET: /TeachingDocuments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var doc = await _context.TeachingDocuments
                .Include(t => t.Personnel)
                .Include(t => t.ApprovedBy)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (doc == null)
                return NotFound();

            return View(doc);
        }
    }
}
5.2 TextbooksController (ตัวอย่างคล้ายกัน)
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourApp.Data;

namespace YourApp.Controllers
{
    public class TextbooksController : Controller
    {
        private readonly AppDbContext _context;

        public TextbooksController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Textbooks
                .Include(t => t.Personnel)
                .Include(t => t.ApprovedBy)
                .ToListAsync();

            return View(items);
        }
    }
}
จากตรงนี้คุณสามารถเพิ่ม Action อื่น ๆ (Create/Edit/Delete) ด้วย scaffolding ของ Visual Studio หรือเขียนเองต่อได้เลยครับ
ถ้าต้องการให้ผมช่วยเขียน CRUD ครบชุด หรือปรับชื่อ field/PK ให้ตรงกับของจริง บอกได้เลย ผมจะเขียนต่อให้ละเอียดมากขึ้นครับ 💻
You said:
"ให้ EF สร้าง model และ DbContext ให้เอง แล้วเอา pattern ของ Controller/Query ด้านบนไปใช้ได้เลยครับ ถ้าต้องการให้ผมช่วยต่อ เช่น scaffold View / เขียนหน้า UI สำหรับ academic office" ช่วยเขียนให้หน่อย
ChatGPT said:
Thought for 39s
โอเค เดี๋ยวผมเขียนตัวอย่าง Controller + View (UI ฝั่ง academic office) ให้เลย
สมมติว่า EF Core ได้สร้าง DbContext และ Model ต่าง ๆ ไว้แล้วชื่อประมาณนี้
public class AppDbContext : DbContext
{
    public DbSet<Personnel> Personnels { get; set; }
    public DbSet<TeachingDocument> TeachingDocuments { get; set; }
    public DbSet<Textbook> Textbooks { get; set; }
}
ถ้าชื่อจริงของ DbSet / class ต่างกันนิดหน่อย เปลี่ยนให้ตรงของโปรเจกต์จริงได้เลยครับ

1) ViewModel สำหรับหน้า Academic Office Dashboard
Models/AcademicOfficeOverviewViewModel.cs
namespace YourApp.Models
{
    public class AcademicOfficeOverviewViewModel
    {
        public int EmpId { get; set; }
        public string FullName { get; set; }
        public string Faculty { get; set; }

        public int TeachingDocCount { get; set; }
        public int TextbookCount { get; set; }
    }
}

2) AcademicOfficeController
สร้าง controller ใหม่ชื่อ AcademicOfficeController
Controllers/AcademicOfficeController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourApp.Data;
using YourApp.Models;

namespace YourApp.Controllers
{
    public class AcademicOfficeController : Controller
    {
        private readonly AppDbContext _context;

        public AcademicOfficeController(AppDbContext context)
        {
            _context = context;
        }

        // ========== Dashboard รวมข้อมูลอาจารย์ ==========
        // GET: /AcademicOffice
        public async Task<IActionResult> Index()
        {
            var model = await _context.Personnels
                .Select(p => new AcademicOfficeOverviewViewModel
                {
                    EmpId = p.EmpId,
                    FullName = p.EmpFname + " " + p.EmpLname,
                    Faculty = p.EmpFaculty,
                    TeachingDocCount = p.TeachingDocuments.Count(),
                    TextbookCount = p.Textbooks.Count()
                })
                .OrderBy(p => p.Faculty)
                .ThenBy(p => p.FullName)
                .ToListAsync();

            return View(model);
        }

        // ========== รายการเอกสารการสอนของอาจารย์แต่ละคน ==========
        // GET: /AcademicOffice/TeachingDocuments/123
        public async Task<IActionResult> TeachingDocuments(int empId)
        {
            var personnel = await _context.Personnels
                .FirstOrDefaultAsync(p => p.EmpId == empId);

            if (personnel == null) return NotFound();

            ViewBag.PersonnelName = personnel.EmpFname + " " + personnel.EmpLname;
            ViewBag.Faculty = personnel.EmpFaculty;
            ViewBag.EmpId = empId;

            var docs = await _context.TeachingDocuments
                .Where(t => t.PersonnelEmpId == empId)
                .OrderByDescending(t => t.AcadYear)
                .ThenByDescending(t => t.DayMonthYear)
                .ToListAsync();

            return View(docs);
        }

        // ========== รายการตำราของอาจารย์แต่ละคน ==========
        // GET: /AcademicOffice/Textbooks/123
        public async Task<IActionResult> Textbooks(int empId)
        {
            var personnel = await _context.Personnels
                .FirstOrDefaultAsync(p => p.EmpId == empId);

            if (personnel == null) return NotFound();

            ViewBag.PersonnelName = personnel.EmpFname + " " + personnel.EmpLname;
            ViewBag.Faculty = personnel.EmpFaculty;
            ViewBag.EmpId = empId;

            var books = await _context.Textbooks
                .Where(t => t.PersonnelEmpId == empId)
                .OrderByDescending(t => t.AcadYear)
                .ThenByDescending(t => t.DayMonthYear)
                .ToListAsync();

            return View(books);
        }

        // ========== ตัวอย่าง Action อนุมัติเอกสารการสอน ==========
        // สมมติว่า academic office เป็นคนอนุมัติ เปลี่ยน status เป็น "Approved"
        // แล้วบันทึก approved_emp_id = รหัสพนักงานของผู้อนุมัติ (ตอนนี้ขอรับจาก parameter ง่าย ๆ ก่อน)
        [HttpPost]
        public async Task<IActionResult> ApproveTeachingDocument(int id, int approvedEmpId)
        {
            var doc = await _context.TeachingDocuments.FindAsync(id);
            if (doc == null) return NotFound();

            doc.Status = "Approved";
            doc.ApprovedEmpId = approvedEmpId;
            await _context.SaveChangesAsync();

            // กลับไปหน้า list ของอาจารย์คนนั้น
            return RedirectToAction(nameof(TeachingDocuments), new { empId = doc.PersonnelEmpId });
        }

        // ========== ตัวอย่าง Action อนุมัติ Textbook ==========
        [HttpPost]
        public async Task<IActionResult> ApproveTextbook(int id, int approvedEmpId)
        {
            var book = await _context.Textbooks.FindAsync(id);
            if (book == null) return NotFound();

            book.Status = "Approved";
            book.ApprovedEmpId = approvedEmpId;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Textbooks), new { empId = book.PersonnelEmpId });
        }
    }
}
หมายเหตุ
ในระบบจริง ถ้ามี Login อยู่แล้ว ให้ดึง approvedEmpId จาก User ที่ login (เช่น Claims) แทนการส่งผ่าน form
ถ้าระบบต้องการ status อื่น ๆ (“Pending”, “Reject”) ก็เพิ่ม action เหมือนกันได้เลย

3) Views สำหรับ Academic Office
3.1 Views/AcademicOffice/Index.cshtml
@model IEnumerable<YourApp.Models.AcademicOfficeOverviewViewModel>

@{
    ViewData["Title"] = "Academic Office Dashboard";
}

<h2>Academic Office Dashboard</h2>

<table class="table table-striped table-bordered">
    <thead>
        <tr>
            <th>Emp ID</th>
            <th>Name</th>
            <th>Faculty</th>
            <th>Teaching Documents</th>
            <th>Textbooks</th>
            <th>Actions</th>
        </tr>
    </thead>
    <tbody>
    @foreach (var item in Model)
    {
        <tr>
            <td>@item.EmpId</td>
            <td>@item.FullName</td>
            <td>@item.Faculty</td>
            <td class="text-center">@item.TeachingDocCount</td>
            <td class="text-center">@item.TextbookCount</td>
            <td>
                <a asp-controller="AcademicOffice"
                   asp-action="TeachingDocuments"
                   asp-route-empId="@item.EmpId"
                   class="btn btn-sm btn-primary">
                    Teaching Docs
                </a>
                <a asp-controller="AcademicOffice"
                   asp-action="Textbooks"
                   asp-route-empId="@item.EmpId"
                   class="btn btn-sm btn-secondary">
                    Textbooks
                </a>
            </td>
        </tr>
    }
    </tbody>
</table>

3.2 Views/AcademicOffice/TeachingDocuments.cshtml
@model IEnumerable<YourApp.Models.TeachingDocument>

@{
    ViewData["Title"] = "Teaching Documents";
    var empName = ViewBag.PersonnelName as string;
    var faculty = ViewBag.Faculty as string;
    var empId = (int)ViewBag.EmpId;
}

<h2>Teaching Documents</h2>

<div class="mb-3">
    <strong>Lecturer:</strong> @empName (@empId) <br />
    <strong>Faculty:</strong> @faculty
</div>

<a asp-action="Index" class="btn btn-link">&laquo; Back to Dashboard</a>

<table class="table table-striped table-bordered mt-3">
    <thead>
        <tr>
            <th>Subject</th>
            <th>Teaching Material</th>
            <th>Acad Year</th>
            <th>Date</th>
            <th>Type</th>
            <th>Status</th>
            <th>Reason</th>
            <th style="width:180px;">Actions</th>
        </tr>
    </thead>
    <tbody>
    @foreach (var doc in Model)
    {
        <tr>
            <td>@doc.Subject</td>
            <td>@doc.TeachingMaterial</td>
            <td>@doc.AcadYear</td>
            <td>@doc.DayMonthYear?.ToString("dd/MM/yyyy")</td>
            <td>@doc.Type</td>
            <td>@doc.Status</td>
            <td>@doc.Reason</td>
            <td>
                <!-- ลิงก์ไปหน้า CRUD ของ TeachingDocumentsController ที่ EF scaffold ให้ -->
                <a asp-controller="TeachingDocuments"
                   asp-action="Details"
                   asp-route-id="@doc.Id"
                   class="btn btn-sm btn-info">
                    View
                </a>
                <a asp-controller="TeachingDocuments"
                   asp-action="Edit"
                   asp-route-id="@doc.Id"
                   class="btn btn-sm btn-warning">
                    Edit
                </a>

                <!-- ปุ่ม Approve (ส่ง approvedEmpId ง่าย ๆ เป็น hidden field) -->
                <form asp-action="ApproveTeachingDocument"
                      asp-controller="AcademicOffice"
                      method="post"
                      class="d-inline">
                    <input type="hidden" name="id" value="@doc.Id" />
                    <input type="hidden" name="approvedEmpId" value="999" /> @* ใส่ emp id ของ staff จริงทีหลัง *@
                    <button type="submit"
                            class="btn btn-sm btn-success"
                            onclick="return confirm('Approve this document?');">
                        Approve
                    </button>
                </form>
            </td>
        </tr>
    }
    </tbody>
</table>

3.3 Views/AcademicOffice/Textbooks.cshtml
@model IEnumerable<YourApp.Models.Textbook>

@{
    ViewData["Title"] = "Textbooks";
    var empName = ViewBag.PersonnelName as string;
    var faculty = ViewBag.Faculty as string;
    var empId = (int)ViewBag.EmpId;
}

<h2>Textbooks</h2>

<div class="mb-3">
    <strong>Lecturer:</strong> @empName (@empId) <br />
    <strong>Faculty:</strong> @faculty
</div>

<a asp-action="Index" class="btn btn-link">&laquo; Back to Dashboard</a>

<table class="table table-striped table-bordered mt-3">
    <thead>
        <tr>
            <th>Name of Work</th>
            <th>Teaching Book</th>
            <th>Acad Year</th>
            <th>Date</th>
            <th>Type</th>
            <th>Status</th>
            <th>Reason</th>
            <th style="width:180px;">Actions</th>
        </tr>
    </thead>
    <tbody>
    @foreach (var book in Model)
    {
        <tr>
            <td>@book.NameOfWork</td>
            <td>@book.TeachingBook</td>
            <td>@book.AcadYear</td>
            <td>@book.DayMonthYear?.ToString("dd/MM/yyyy")</td>
            <td>@book.Type</td>
            <td>@book.Status</td>
            <td>@book.Reason</td>
            <td>
                <a asp-controller="Textbooks"
                   asp-action="Details"
                   asp-route-id="@book.Id"
                   class="btn btn-sm btn-info">
                    View
                </a>
                <a asp-controller="Textbooks"
                   asp-action="Edit"
                   asp-route-id="@book.Id"
                   class="btn btn-sm btn-warning">
                    Edit
                </a>

                <form asp-action="ApproveTextbook"
                      asp-controller="AcademicOffice"
                      method="post"
                      class="d-inline">
                    <input type="hidden" name="id" value="@book.Id" />
                    <input type="hidden" name="approvedEmpId" value="999" />
                    <button type="submit"
                            class="btn btn-sm btn-success"
                            onclick="return confirm('Approve this textbook?');">
                        Approve
                    </button>
                </form>
            </td>
        </tr>
    }
    </tbody>
</table>

4) การใช้ร่วมกับ Scaffold ของ EF
ใช้คำสั่ง Scaffold-DbContext (หรือ dotnet ef dbcontext scaffold) ให้ EF สร้าง DbContext + Models + Controllers + Views (CRUD) สำหรับ
TeachingDocuments
Textbooks
Personnels (ถ้าต้องการแก้ไขข้อมูลบุคลากร)
เพิ่ม AcademicOfficeController + Views ทั้ง 3 ไฟล์ด้านบนเข้าไป
หน้า AcademicOffice จะทำหน้าที่เป็น Dashboard กลาง สำหรับ office
ส่วนหน้าที่ scaffold มาแล้วจะเป็นฟอร์ม CRUD รายละเอียด

ถ้าคุณส่งตัวอย่างชื่อ class/DbSet จริง ๆ มาให้ (เช่น TeachingDocument_2_1, Textbook_2_2) ผมสามารถปรับชื่อทั้งหมดใน Controller/View ให้ตรงกับของจริงแบบเป๊ะ ๆ ให้ได้อีกชุดหนึ่งครับ 🙂
You said:
"ถ้าอยากให้ผมช่วยต่อ เช่น เพิ่มฟังก์ชัน เพิ่ม/แก้ไข/ลบ รายการผลงาน (CRUD) หรือเขียน query เฉพาะ เช่น แสดงสรุปจำนวน เอกสารประกอบการสอน / หนังสือ ต่อปี และ export เป็น Excel บอกได้เลยครับ เดี๋ยวผมเขียนโค้ดต่อให้ครบชุด" ใช่ครับ
ChatGPT said:
Thought for 1m 10s
จัดให้ครบชุดเลยนะครับ:
CRUD สำหรับ TeachingDocument และ Textbook
หน้า สรุปจำนวนเอกสาร/หนังสือต่อปี
ปุ่ม Export เป็น Excel (.xlsx)
ผมจะสมมติชื่อคลาส/DbSet ตามนี้ (ถ้าของจริงต่างกัน เปลี่ยนชื่อให้ตรงได้เลยนะครับ)
public class AppDbContext : DbContext
{
    public DbSet<Personnel> Personnels { get; set; }
    public DbSet<TeachingDocument> TeachingDocuments { get; set; }
    public DbSet<Textbook> Textbooks { get; set; }
}

1. ViewModel สำหรับรายงานต่อปี
Models/SummaryByYearViewModel.cs
namespace YourApp.Models
{
    public class SummaryByYearViewModel
    {
        public string AcadYear { get; set; }
        public int TeachingDocumentCount { get; set; }
        public int TextbookCount { get; set; }
    }
}

2. TeachingDocumentsController (CRUD เต็ม)
Controllers/TeachingDocumentsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YourApp.Data;
using YourApp.Models;

namespace YourApp.Controllers
{
    public class TeachingDocumentsController : Controller
    {
        private readonly AppDbContext _context;

        public TeachingDocumentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TeachingDocuments
        public async Task<IActionResult> Index()
        {
            var query = _context.TeachingDocuments
                .Include(t => t.Personnel)
                .Include(t => t.ApprovedBy);

            return View(await query.ToListAsync());
        }

        // GET: TeachingDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var doc = await _context.TeachingDocuments
                .Include(t => t.Personnel)
                .Include(t => t.ApprovedBy)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (doc == null) return NotFound();

            return View(doc);
        }

        // GET: TeachingDocuments/Create
        public IActionResult Create()
        {
            ViewData["PersonnelEmpId"] =
                new SelectList(_context.Personnels, "EmpId", "EmpFname");
            ViewData["ApprovedEmpId"] =
                new SelectList(_context.Personnels, "EmpId", "EmpFname");

            return View();
        }

        // POST: TeachingDocuments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Subject,TeachingMaterial,DayMonthYear,Type,CoProducer,AcadYear,Reason,Status,ApprovedEmpId,PersonnelEmpId")]
            TeachingDocument teachingDocument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teachingDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PersonnelEmpId"] =
                new SelectList(_context.Personnels, "EmpId", "EmpFname", teachingDocument.PersonnelEmpId);
            ViewData["ApprovedEmpId"] =
                new SelectList(_context.Personnels, "EmpId", "EmpFname", teachingDocument.ApprovedEmpId);

            return View(teachingDocument);
        }

        // GET: TeachingDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var doc = await _context.TeachingDocuments.FindAsync(id);
            if (doc == null) return NotFound();

            ViewData["PersonnelEmpId"] =
                new SelectList(_context.Personnels, "EmpId", "EmpFname", doc.PersonnelEmpId);
            ViewData["ApprovedEmpId"] =
                new SelectList(_context.Personnels, "EmpId", "EmpFname", doc.ApprovedEmpId);

            return View(doc);
        }

        // POST: TeachingDocuments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Subject,TeachingMaterial,DayMonthYear,Type,CoProducer,AcadYear,Reason,Status,ApprovedEmpId,PersonnelEmpId")]
            TeachingDocument teachingDocument)
        {
            if (id != teachingDocument.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teachingDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeachingDocumentExists(teachingDocument.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["PersonnelEmpId"] =
                new SelectList(_context.Personnels, "EmpId", "EmpFname", teachingDocument.PersonnelEmpId);
            ViewData["ApprovedEmpId"] =
                new SelectList(_context.Personnels, "EmpId", "EmpFname", teachingDocument.ApprovedEmpId);

            return View(teachingDocument);
        }

        // GET: TeachingDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var doc = await _context.TeachingDocuments
                .Include(t => t.Personnel)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (doc == null) return NotFound();

            return View(doc);
        }

        // POST: TeachingDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doc = await _context.TeachingDocuments.FindAsync(id);
            if (doc != null)
            {
                _context.TeachingDocuments.Remove(doc);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TeachingDocumentExists(int id)
        {
            return _context.TeachingDocuments.Any(e => e.Id == id);
        }
    }
}
สำหรับ TextbooksController สามารถ copy controller นี้ไปอีกไฟล์ แล้ว
เปลี่ยนชื่อ class เป็น TextbooksController
เปลี่ยน type จาก TeachingDocument เป็น Textbook
เปลี่ยน DbSet เป็น _context.Textbooks
ก็จะได้ CRUD ครบชุดเหมือนกันครับ

3. Controller สำหรับ “รายงานสรุปต่อปี + Export Excel”
3.1 ติดตั้ง ClosedXML
ในโปรเจกต์จริง ให้เพิ่ม package:
dotnet add package ClosedXML
หรือผ่าน NuGet UI ใน Visual Studio:
ค้นหา ClosedXML แล้วติดตั้ง
3.2 AcademicReportsController
Controllers/AcademicReportsController.cs
using System.IO;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourApp.Data;
using YourApp.Models;

namespace YourApp.Controllers
{
    public class AcademicReportsController : Controller
    {
        private readonly AppDbContext _context;

        public AcademicReportsController(AppDbContext context)
        {
            _context = context;
        }

        // ========== แสดงสรุปจำนวนเอกสารประกอบการสอน / หนังสือ ต่อปี ==========
        // GET: /AcademicReports/SummaryByYear
        public async Task<IActionResult> SummaryByYear()
        {
            // รวมปีทั้งหมดที่มีใน teaching_document และ textbook
            var allYearsQuery =
                _context.TeachingDocuments.Select(t => t.AcadYear)
                    .Union(_context.Textbooks.Select(b => b.AcadYear))
                    .Distinct()
                    .OrderBy(y => y);

            var allYears = await allYearsQuery.ToListAsync();

            var result = new List<SummaryByYearViewModel>();

            foreach (var year in allYears)
            {
                var tdCount = await _context.TeachingDocuments
                    .CountAsync(t => t.AcadYear == year);

                var tbCount = await _context.Textbooks
                    .CountAsync(b => b.AcadYear == year);

                result.Add(new SummaryByYearViewModel
                {
                    AcadYear = year,
                    TeachingDocumentCount = tdCount,
                    TextbookCount = tbCount
                });
            }

            return View(result);
        }

        // ========== Export Excel ==========
        // GET: /AcademicReports/ExportSummaryByYear
        public async Task<IActionResult> ExportSummaryByYear()
        {
            var allYearsQuery =
                _context.TeachingDocuments.Select(t => t.AcadYear)
                    .Union(_context.Textbooks.Select(b => b.AcadYear))
                    .Distinct()
                    .OrderBy(y => y);

            var allYears = await allYearsQuery.ToListAsync();

            var result = new List<SummaryByYearViewModel>();

            foreach (var year in allYears)
            {
                var tdCount = await _context.TeachingDocuments
                    .CountAsync(t => t.AcadYear == year);

                var tbCount = await _context.Textbooks
                    .CountAsync(b => b.AcadYear == year);

                result.Add(new SummaryByYearViewModel
                {
                    AcadYear = year,
                    TeachingDocumentCount = tdCount,
                    TextbookCount = tbCount
                });
            }

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("SummaryByYear");

            // Header
            ws.Cell(1, 1).Value = "Academic Year";
            ws.Cell(1, 2).Value = "Teaching Documents";
            ws.Cell(1, 3).Value = "Textbooks";

            var row = 2;
            foreach (var item in result)
            {
                ws.Cell(row, 1).Value = item.AcadYear;
                ws.Cell(row, 2).Value = item.TeachingDocumentCount;
                ws.Cell(row, 3).Value = item.TextbookCount;
                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var fileName = $"SummaryByYear_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
    }
}

4. Views
4.1 SummaryByYear.cshtml
Views/AcademicReports/SummaryByYear.cshtml
@model IEnumerable<YourApp.Models.SummaryByYearViewModel>

@{
    ViewData["Title"] = "Summary by Academic Year";
}

<h2>Summary by Academic Year</h2>

<p>
    <a asp-action="ExportSummaryByYear"
       class="btn btn-success">
        Export to Excel
    </a>
</p>

<table class="table table-striped table-bordered">
    <thead>
        <tr>
            <th>Academic Year</th>
            <th>Teaching Documents</th>
            <th>Textbooks</th>
        </tr>
    </thead>
    <tbody>
    @foreach (var item in Model)
    {
        <tr>
            <td>@item.AcadYear</td>
            <td class="text-center">@item.TeachingDocumentCount</td>
            <td class="text-center">@item.TextbookCount</td>
        </tr>
    }
    </tbody>
</table>

5. ตัวอย่าง Index View ของ TeachingDocuments (ใช้กับ CRUD)
Views/TeachingDocuments/Index.cshtml (โครงคล้าย scaffold)
@model IEnumerable<YourApp.Models.TeachingDocument>

@{
    ViewData["Title"] = "Teaching Documents";
}

<h2>Teaching Documents</h2>

<p>
    <a asp-action="Create" class="btn btn-primary">Create New</a>
</p>

<table class="table table-striped table-bordered">
    <thead>
        <tr>
            <th>Subject</th>
            <th>Teaching Material</th>
            <th>Acad Year</th>
            <th>Date</th>
            <th>Type</th>
            <th>Status</th>
            <th>Owner</th>
            <th style="width:220px;">Actions</th>
        </tr>
    </thead>
    <tbody>
    @foreach (var item in Model)
    {
        <tr>
            <td>@item.Subject</td>
            <td>@item.TeachingMaterial</td>
            <td>@item.AcadYear</td>
            <td>@item.DayMonthYear?.ToString("dd/MM/yyyy")</td>
            <td>@item.Type</td>
            <td>@item.Status</td>
            <td>@item.Personnel?.EmpFname @item.Personnel?.EmpLname</td>
            <td>
                <a asp-action="Details" asp-route-id="@item.Id" class="btn btn-sm btn-info">Details</a>
                <a asp-action="Edit" asp-route-id="@item.Id" class="btn btn-sm btn-warning">Edit</a>
                <a asp-action="Delete" asp-route-id="@item.Id" class="btn btn-sm btn-danger">Delete</a>
            </td>
        </tr>
    }
    </tbody>
</table>

แค่นี้ก็ได้
CRUD สำหรับผลงาน (เอกสารประกอบการสอน / หนังสือ)
หน้ารายงานสรุปจำนวนต่อปี
ปุ่ม Export เป็น Excel เรียบร้อย

