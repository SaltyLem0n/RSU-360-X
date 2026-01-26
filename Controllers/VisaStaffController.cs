using Microsoft.AspNetCore.Mvc;
using RSU_360_X.Models;
using RSU_360_X.Services;
using System.Linq;
using System.Threading.Tasks;

namespace RSU_360_X.Controllers
{
    [Route("staff/visa")] // Maps to https://localhost:7256/staff/visa
    public class VisaStaffController : Controller
    {
        private readonly IVisaRepository _visaRepo;

        public VisaStaffController(IVisaRepository visaRepo)
        {
            _visaRepo = visaRepo;
        }

        [HttpGet]
        [Route("")]
        [Route("review")] // Maps to both /staff/visa and /staff/visa/review
        public async Task<IActionResult> Index()
        {
            // 1. Get all data
            var allData = await _visaRepo.GetAllAsync();

            // 2. Prepare ViewModel
            var vm = new VisaReviewIndexVm
            {
                Applications = allData.OrderByDescending(x => x.SubmittedAt).ToList(),
                PendingCount = allData.Count(x => x.Status == "submitted"),
                ApprovedCount = allData.Count(x => x.Status == "approved"),
                RejectedCount = allData.Count(x => x.Status == "rejected" || x.Status == "needs_fix"),
                TotalCount = allData.Count,
                CompletedCount = allData.Count(x => x.Status == "completed" || x.Status == "pickup")
            };

            return View("~/Views/VisaStaff/Index.cshtml", vm);
        }
    }
}
