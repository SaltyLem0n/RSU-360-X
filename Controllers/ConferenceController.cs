using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSU_360_X.Models_Db;
using RSU_360_X.ViewModels;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;

namespace RSU_360_X.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly EvDbContext _db;
        public ConferenceController(EvDbContext db) => _db = db;

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpGet]
        [Route("staff/conference/list", Name = "Staff_Conference_List")]
        public async Task<IActionResult> List()
        {
            var empId = HttpContext.Session.GetString("EmpId");
            if (string.IsNullOrWhiteSpace(empId))
                return Unauthorized("EmpId missing in session.");

            var items = await _db.AcademicArticle24s
                .AsNoTracking()
                .Where(x => x.PersonnelEmpId == empId)
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    meeting_name = x.MeetingName,
                    article_title = x.ArticleTitle,
                    authors = x.Authors,
                    day_month_year = x.DayMonthYear.ToString("yyyy-MM-dd"),
                    publish_year = x.PublishYear,
                    country = x.Country,
                    status = x.Status
                })
                .ToListAsync();

            return Json(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExtractFromPdf(IFormFile pdf)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return Unauthorized("Not logged in.");

            if (pdf == null || pdf.Length == 0)
                return BadRequest("No PDF uploaded.");

            var isPdfByName = (Path.GetExtension(pdf.FileName) ?? "").Equals(".pdf", StringComparison.OrdinalIgnoreCase);
            var isPdfByType = string.Equals(pdf.ContentType, "application/pdf", StringComparison.OrdinalIgnoreCase);

            if (!isPdfByName && !isPdfByType)
                return BadRequest("File must be a PDF.");

            if (pdf.Length > 15 * 1024 * 1024)
                return BadRequest("PDF too large (max 15MB).");

            try
            {
                using var ms = new MemoryStream();
                pdf.CopyTo(ms);
                ms.Position = 0;

                var raw = ExtractAllTextFromPdf(ms, 15);
                var (abs, keys) = ExtractAbstractKeywords(raw);

                return Json(new
                {
                    @abstract = abs,
                    keywords = keys
                });
            }
            catch
            {
                return BadRequest("Failed to read PDF.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit([FromForm] ConferenceVm vm)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return Unauthorized("Not logged in.");

            if (!ModelState.IsValid)
                return BadRequest("Invalid form data.");

            var empId = HttpContext.Session.GetString("EmpId");
            if (string.IsNullOrWhiteSpace(empId))
                return Unauthorized("EmpId missing in session.");

            var empExists = await _db.Personnel.AsNoTracking().AnyAsync(p => p.EmpId == empId);
            if (!empExists)
                return BadRequest($"EmpId '{empId}' not found in hr.personnel.");

            if (!DateOnly.TryParseExact(vm.DayMonthYear, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var dayMonthYear))
                return BadRequest($"Invalid date: {vm.DayMonthYear}");

            static string OneLine(string? s, int max)
            {
                s = (s ?? "").Trim();
                s = Regex.Replace(s, @"\s+", " ");
                return s.Length <= max ? s : s.Substring(0, max);
            }

            var entity = new AcademicArticle24
            {
                MeetingName = OneLine(vm.MeetingName, 45),
                ArticleTitle = OneLine(vm.ArticleTitle, 45),
                Authors = OneLine(vm.Authors, 45),
                DayMonthYear = dayMonthYear,
                PublishYear = vm.PublishYear,
                Country = OneLine(vm.Country, 45),
                Abstract = OneLine(vm.Abstract, 4000),
                Keywords = OneLine(vm.Keywords, 1000),

                PaperFormatType = "-",
                PaperFormatLevel = "-",

                AcadYear = DateTime.Now.Year,
                Reason = "-",
                Status = "W",
                ApprovedEmpId = "-",
                PersonnelEmpId = empId
            };

            _db.AcademicArticle24s.Add(entity);
            await _db.SaveChangesAsync();

            return Ok("Saved");
        }

        private static string ExtractAllTextFromPdf(Stream pdfStream, int maxPages)
        {
            var sb = new StringBuilder();
            using var doc = PdfDocument.Open(pdfStream);

            int count = 0;
            foreach (var page in doc.GetPages())
            {
                sb.AppendLine(page.Text);
                sb.AppendLine("\n---PAGE---\n");
                count++;
                if (count >= maxPages) break;
            }

            return sb.ToString();
        }

        private static string NormalizePdfText(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";

            var t = text.Replace('\u00A0', ' ')
                        .Replace("\r", "\n");

            t = Regex.Replace(t, @"-\s*\n\s*", "");
            t = Regex.Replace(t, @"([A-Z]{2,})([A-Z][a-z])", "$1 $2");
            t = Regex.Replace(t, @"([a-z])([A-Z])", "$1 $2");
            t = Regex.Replace(t, @"(\d)([A-Z])", "$1 $2");
            t = Regex.Replace(t, @"([’'""\)])([A-Z])", "$1 $2");
            t = Regex.Replace(t, @"[ \t]+", " ");
            t = Regex.Replace(t, @"\n{3,}", "\n\n");

            return t.Trim();
        }

        private static int IndexOfIgnoreCase(string s, string value, int startIndex = 0)
        {
            if (startIndex < 0) startIndex = 0;
            return s.IndexOf(value, startIndex, StringComparison.OrdinalIgnoreCase);
        }

        private static int FindEarliestStop(string t, int startIndex)
        {
            if (startIndex < 0) startIndex = 0;

            string[] stops =
            {
                "Introduction", "1 Introduction", "1. Introduction", "I. Introduction", "I Introduction",
                "Background", "Methods", "Methodology", "Materials and Methods",
                "Results", "Discussion", "Conclusion", "References", "Acknowledgements", "Acknowledgments"
            };

            int best = -1;
            foreach (var s in stops)
            {
                int idx = IndexOfIgnoreCase(t, s, startIndex);
                if (idx >= 0 && (best < 0 || idx < best)) best = idx;
            }

            return best;
        }

        private static (string? Abstract, string? Keywords) ExtractAbstractKeywords(string rawText)
        {
            var t = NormalizePdfText(rawText);
            if (string.IsNullOrWhiteSpace(t)) return (null, null);

            int absLabel = IndexOfIgnoreCase(t, "Abstract");

            string? abs = null;
            if (absLabel >= 0)
            {
                int absStart = absLabel + "Abstract".Length;
                if (absStart < t.Length && (t[absStart] == ':' || t[absStart] == '-' || t[absStart] == '—')) absStart++;
                while (absStart < t.Length && char.IsWhiteSpace(t[absStart])) absStart++;

                int kwLabel = IndexOfIgnoreCase(t, "Keywords", absStart);
                int absStop = kwLabel >= 0 ? kwLabel : t.Length;

                abs = SafeSlice(t, absStart, absStop);
                abs = CleanAbstract(abs);
                if (string.IsNullOrWhiteSpace(abs)) abs = null;
            }

            string? kw = null;
            int kwLabelAny = IndexOfIgnoreCase(t, "Keywords");
            if (kwLabelAny >= 0)
            {
                int kwStart = kwLabelAny + "Keywords".Length;
                if (kwStart < t.Length && (t[kwStart] == ':' || t[kwStart] == '-' || t[kwStart] == '—')) kwStart++;
                while (kwStart < t.Length && char.IsWhiteSpace(t[kwStart])) kwStart++;

                int kwStop = t.Length;
                int secStop = FindEarliestStop(t, kwStart);
                if (secStop >= 0) kwStop = Math.Min(kwStop, secStop);

                kw = SafeSlice(t, kwStart, kwStop);
                kw = kw.Replace("\n", " ");
                kw = Regex.Replace(kw, @"\s{2,}", " ").Trim();

                int introIdx = kw.IndexOf("Introduction", StringComparison.OrdinalIgnoreCase);
                if (introIdx >= 0) kw = kw.Substring(0, introIdx).Trim();

                if (kw.Length > 1000) kw = kw.Substring(0, 1000);
                if (string.IsNullOrWhiteSpace(kw)) kw = null;
            }

            return (abs, kw);
        }

        private static string SafeSlice(string s, int start, int end)
        {
            if (start < 0) start = 0;
            if (end < start) end = start;
            if (end > s.Length) end = s.Length;
            return s.Substring(start, end - start).Trim();
        }

        private static string CleanAbstract(string s)
        {
            s = s.Trim();
            s = Regex.Replace(s, @"\n{3,}", "\n\n");
            s = Regex.Replace(s, @"[ \t]{2,}", " ");
            if (s.Length > 4000) s = s.Substring(0, 4000);
            return s.Trim();
        }
    }
}
