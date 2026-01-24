using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Required for Session

namespace RSU_360_X.Controllers
{
    public class HomeController : Controller
    {
        // 1. PUBLIC LANDING PAGE (The new RSUX Design)
        public IActionResult Index()
        {
            return View();
        }

        // 2. HR PAGE (The new RSUX Design)
        public IActionResult Hr()
        {
            return View();
        }

        // 3. STUDENT DASHBOARD (Protected - Old Bootstrap Design)
        // You must rename your old 'Index.cshtml' to 'Dashboard.cshtml'
        public IActionResult Dashboard()
        {
            // Security Check
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}