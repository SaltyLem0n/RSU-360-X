using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RSU_360_X.Models;
using RSU_360_X.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace RSU_360_X.Controllers
{
    [Route("visa")]
    public class VisaController : Controller
    {
        private readonly IVisaRepository _visaRepo;
        private readonly IContactRepository _contactRepo;
        private readonly IStudentProfileRepository _studentRepo;
        private readonly IWebHostEnvironment _env;

        public VisaController(
            IVisaRepository visaRepo, 
            IContactRepository contactRepo, 
            IStudentProfileRepository studentRepo, 
            IWebHostEnvironment env)
        {
            _visaRepo = visaRepo;
            _contactRepo = contactRepo;
            _studentRepo = studentRepo;
            _env = env;
        }

        private string? StudentId => HttpContext.Session.GetString("UserId") ?? HttpContext.Session.GetString("Username");

        [HttpGet]
        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(StudentId)) return RedirectToAction("Index", "Login");

            // 1. Load ALL submissions for this student
            var allSubmissions = await _visaRepo.GetAllAsync();
            var mySubmissions = allSubmissions
                                .Where(x => x.StudentId == StudentId)
                                .OrderByDescending(x => x.SubmittedAt)
                                .ToList();

            var latest = mySubmissions.FirstOrDefault();

            // ... (Check Email Modal Logic) ...
            bool showModal = await _contactRepo.NeedsEmailUpdateAsync(StudentId);
            ViewBag.ShowEmailModal = showModal;

            // --- NEW: Fetch Profile Data ---
            var profile = await _studentRepo.GetProfileAsync(StudentId);

            var vm = new VisaIndexVm
            {
                StudentId = StudentId,
                Status = latest?.Status ?? "draft",
                Files = latest?.Files ?? new List<FileRef>(),
                IssueDate = latest?.IssueDate,
                ExpiryDate = latest?.ExpiryDate,
                StaffNotes = latest?.StaffNotes,
                HistoryList = mySubmissions,

                // Logic: Use data from submission if exists, otherwise Auto-fill from Profile
                Passport = !string.IsNullOrEmpty(latest?.Passport) ? latest.Passport : (profile?.Passport ?? ""),
                Country = !string.IsNullOrEmpty(latest?.Country) ? latest.Country : (profile?.Nationality ?? "")
            };

            // Allow edit if no submission, or if status is 'draft'/'needs_fix'.
            // If status is 'rejected' or 'completed', user should be able to start a *new* one (handled by Submit logic + View)
            ViewBag.AllowEdit = latest == null || latest.Status == "draft" || latest.Status == "needs_fix";
            
            return View("~/Views/Visa/Index.cshtml", vm);
        }

        [HttpPost]
        [Route("update-email")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmail(string email)
        {
            if (string.IsNullOrEmpty(StudentId)) return RedirectToAction("Index", "Login");
            
            if (!string.IsNullOrEmpty(email))
            {
                await _contactRepo.UpdateEmailAsync(StudentId, email);
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("")]
        [Route("index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(
            string passportNumber,
            string country,
            string visaType,
            DateTime? issueDate,
            DateTime? expiryDate,
            List<IFormFile> passportPages,
            List<IFormFile> visaStamps,
            IFormFile tm30,
            IFormFile tuitionReceipt
        )
        {
            if (string.IsNullOrEmpty(StudentId)) return RedirectToAction("Index", "Login");

            // 1. Get ALL submissions to determine if we update existing or create new
            var allSubmissions = await _visaRepo.GetAllAsync();
            var mySubmissions = allSubmissions.Where(x => x.StudentId == StudentId).OrderByDescending(x => x.SubmittedAt).ToList();
            var latest = mySubmissions.FirstOrDefault();

            VisaSubmission sub;

            // Define "Final" states where we should start a NEW application
            var finalStates = new[] { "rejected", "completed", "cancelled" };

            if (latest != null && !finalStates.Contains(latest.Status))
            {
                // Update EXISTING (Draft, NeedsFix, Submitted, etc.)
                sub = latest;
            }
            else
            {
                // Create NEW
                var profile = await _studentRepo.GetProfileAsync(StudentId);
                sub = new VisaSubmission 
                { 
                    StudentId = StudentId,
                    StudentName = profile?.DisplayName ?? HttpContext.Session.GetString("DisplayName")
                };
            }

            // Update basic info
            sub.Passport = passportNumber;
            sub.Country = country;
            sub.VisaType = visaType;
            sub.IssueDate = issueDate;
            sub.ExpiryDate = expiryDate;
            sub.Status = "submitted"; 
            sub.SubmittedAt = DateTime.UtcNow; // Update submitted time for fresh sort
            
            sub.History.Add(new HistoryEvent { At = DateTime.UtcNow, Event = "submitted", Note = "Submitted via Dashboard" });

            // --- Helper Function to Save Files ---
            async Task SaveFilesAsync(List<IFormFile> files, string category)
            {
                if (files == null || files.Count == 0) return;
                
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "visa", StudentId, category);
                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = $"{Guid.NewGuid().ToString("N")[..8]}_{file.FileName}";
                        var filePath = Path.Combine(uploadPath, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        sub.Files.Add(new FileRef 
                        { 
                            Name = file.FileName, 
                            Url = $"/uploads/visa/{StudentId}/{category}/{fileName}",
                            Category = category 
                        });
                    }
                }
            }

            // Save all categories
            await SaveFilesAsync(passportPages, "passport");
            await SaveFilesAsync(visaStamps, "stamp");
            if (tm30 != null) await SaveFilesAsync(new List<IFormFile> { tm30 }, "tm30");
            if (tuitionReceipt != null) await SaveFilesAsync(new List<IFormFile> { tuitionReceipt }, "receipt");

            await _visaRepo.SaveAsync(sub);
            return RedirectToAction("Index");
        }
    }
}
