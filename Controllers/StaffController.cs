using Microsoft.AspNetCore.Mvc;
using System;
using RSU_360_X.Models;
using RSU_360_X.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RSU_360_X.Controllers
{
    [Route("staff/visa")]
    public class StaffController : Controller
    {
        private readonly IVisaRepository _visaRepo;

        public StaffController(IVisaRepository visaRepo)
        {
            _visaRepo = visaRepo;
        }

        [HttpGet("")] // Handles /staff/visa
        [HttpGet("review")] // Handles /staff/visa/review
        public async Task<IActionResult> VisaReview()
        {
            // Fetch all submissions from the JSON repository
            var allSubmissions = await _visaRepo.GetAllAsync();
            
            // Calculate statistics for the dashboard cards
            ViewBag.Total = allSubmissions.Count;
            ViewBag.Approved = allSubmissions.Count(x => x.Status == "approved");
            ViewBag.Pending = allSubmissions.Count(x => x.Status == "submitted" || x.Status == "needs_fix" || x.Status == "draft");
            ViewBag.Rejected = allSubmissions.Count(x => x.Status == "rejected");
            ViewBag.Completed = allSubmissions.Count(x => x.Status == "completed");

            // Order by most recent
            var model = allSubmissions.OrderByDescending(x => x.SubmittedAt).ToList();
            
            return View("~/Views/Staff/VisaReview.cshtml", model);
        }

        [HttpGet("detail/{studentId}")]
        public async Task<IActionResult> VisaDetail(string studentId)
        {
            var all = await _visaRepo.GetAllAsync();
            var submission = all.FirstOrDefault(x => x.StudentId == studentId);
            
            if (submission == null) return NotFound();

            return View("~/Views/Staff/VisaDetail.cshtml", submission);
        }

        [HttpPost("approve/{studentId}")]
        public async Task<IActionResult> Approve(string studentId)
        {
            var all = await _visaRepo.GetAllAsync();
            var sub = all.FirstOrDefault(x => x.StudentId == studentId);
            if (sub != null) {
                sub.Status = "approved";
                sub.History.Add(new HistoryEvent { At = DateTime.UtcNow, Event = "approved", Note = "Approved by Staff" });
                await _visaRepo.SaveAsync(sub);
            }
            return RedirectToAction("VisaDetail", new { studentId });
        }

        [HttpPost("reject/{studentId}")]
        public async Task<IActionResult> Reject(string studentId, string reason)
        {
            var all = await _visaRepo.GetAllAsync();
            var sub = all.FirstOrDefault(x => x.StudentId == studentId);
            if (sub != null) {
                sub.Status = "rejected";
                sub.StaffNotes = reason;
                sub.History.Add(new HistoryEvent { At = DateTime.UtcNow, Event = "rejected", Note = reason });
                await _visaRepo.SaveAsync(sub);
            }
            return RedirectToAction("VisaReview");
        }

        [HttpGet("pickup/{studentId}")]
        public async Task<IActionResult> VisaPickup(string studentId)
        {
            var all = await _visaRepo.GetAllAsync();
            var submission = all.FirstOrDefault(x => x.StudentId == studentId);
            
            if (submission == null) return NotFound();

            return View("~/Views/Staff/VisaPickup.cshtml", submission);
        }

        [HttpPost("pickup/{studentId}")]
        public async Task<IActionResult> SavePickup(string studentId, DateTime pickupTime, string reference)
        {
            var all = await _visaRepo.GetAllAsync();
            var sub = all.FirstOrDefault(x => x.StudentId == studentId);
            
            if (sub != null) {
                sub.PickedUpAt = pickupTime;
                sub.Status = "pickup"; // Ensure this is "pickup"
                sub.History.Add(new HistoryEvent { 
                    At = DateTime.UtcNow, 
                    Event = "picked_up", 
                    Note = $"Physical pickup recorded. Ref: {reference}" 
                });
                await _visaRepo.SaveAsync(sub);
            }
            return RedirectToAction("VisaDetail", new { studentId });
        }

        [HttpGet("reject/{studentId}")]
        public async Task<IActionResult> VisaRejectForm(string studentId)
        {
            var all = await _visaRepo.GetAllAsync();
            var submission = all.FirstOrDefault(x => x.StudentId == studentId);
            
            if (submission == null) return NotFound();

            return View("~/Views/Staff/VisaReject.cshtml", submission);
        }

        [HttpPost("reject-confirm/{studentId}")]
        public async Task<IActionResult> ConfirmReject(string studentId, string reason)
        {
            var all = await _visaRepo.GetAllAsync();
            var sub = all.FirstOrDefault(x => x.StudentId == studentId);
            
            if (sub != null) {
                sub.Status = "rejected";
                sub.StaffNotes = reason; // Save the rejection reason
                sub.History.Add(new HistoryEvent { 
                    At = DateTime.UtcNow, 
                    Event = "rejected", 
                    Note = reason 
                });
                await _visaRepo.SaveAsync(sub);
            }
            return RedirectToAction("VisaReview");
        }

        [HttpGet("approve/{studentId}")]
        public async Task<IActionResult> VisaApproveForm(string studentId)
        {
            var all = await _visaRepo.GetAllAsync();
            var submission = all.FirstOrDefault(x => x.StudentId == studentId);
            
            if (submission == null) return NotFound();

            return View("~/Views/Staff/VisaApprove.cshtml", submission);
        }

        [HttpPost("approve-confirm/{studentId}")]
        public async Task<IActionResult> ConfirmApprove(string studentId, DateTime pickupAppointment, string note)
        {
            var all = await _visaRepo.GetAllAsync();
            var sub = all.FirstOrDefault(x => x.StudentId == studentId);
            
            if (sub != null) {
                sub.Status = "approved";
                sub.PickupAppointmentAt = pickupAppointment;
                sub.StaffNotes = note;
                sub.History.Add(new HistoryEvent { 
                    At = DateTime.UtcNow, 
                    Event = "approved", 
                    Note = $"Approved with pickup scheduled for {pickupAppointment}. Note: {note}" 
                });
                await _visaRepo.SaveAsync(sub);
            }
            return RedirectToAction("VisaReview");
        }

        [HttpGet("complete/{studentId}")]
        public async Task<IActionResult> VisaCompleteForm(string studentId)
        {
            var all = await _visaRepo.GetAllAsync();
            var sub = all.FirstOrDefault(x => x.StudentId == studentId);
            
            // Safety check: Only allow access if status is exactly "pickup"
            if (sub == null || sub.Status != "pickup") {
                return RedirectToAction("VisaDetail", new { studentId });
            }

            return View("~/Views/Staff/VisaComplete.cshtml", sub);
        }

        [HttpPost("complete-confirm/{studentId}")]
        public async Task<IActionResult> ConfirmComplete(string studentId, DateTime issueDate, DateTime expiryDate)
        {
            var all = await _visaRepo.GetAllAsync();
            var sub = all.FirstOrDefault(x => x.StudentId == studentId);
            if (sub != null) {
                sub.Status = "completed"; // Final status
                sub.IssueDate = issueDate;
                sub.ExpiryDate = expiryDate;
                sub.History.Add(new HistoryEvent { At = DateTime.UtcNow, Event = "completed", Note = "New Visa dates recorded." });
                await _visaRepo.SaveAsync(sub);
            }
            return RedirectToAction("VisaReview");
        }
    }
}
