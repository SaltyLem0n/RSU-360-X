using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // <--- REQUIRED for Session.GetString()

namespace RSU_360_X.Controllers
{
    public class HomeController : Controller
    {
        // 1. PUBLIC LANDING PAGE (New Index)
        public IActionResult Index()
        {
            return View();
        }

        // 2. HR PAGE (Public)
        public IActionResult Hr()
        {
            return View();
        }

        // 3. STUDENT DASHBOARD (Protected - Old Index)
        public IActionResult Dashboard()
        {
            // Security check: Ensure user is logged in
            // Requires "using Microsoft.AspNetCore.Http;" at the top
            if (HttpContext.Session.GetString("UserId") == null)
            {
                // Ensure you have a LoginController with an Index action
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