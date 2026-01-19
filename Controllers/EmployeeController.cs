using Microsoft.AspNetCore.Mvc;

namespace RSU_360_X.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            // Security check: Ensure user is logged in
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
