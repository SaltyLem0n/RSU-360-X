using Microsoft.AspNetCore.Mvc;
using RSU_360_X.Services;
using RSU_360_X.Models.Auth;

namespace RSU_360_X.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHybridAuthService _authService;

        public LoginController(IHybridAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
                return RedirectBasedOnRole(HttpContext.Session.GetString("Role"));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Please enter both username and password.";
                return View();
            }

            // Call Service
            AppUser? user = await _authService.LoginAsync(username, password);

            if (user == null)
            {
                ViewBag.Error = "Invalid ID or Password.";
                return View();
            }

            // Hydrate Session
            HttpContext.Session.SetString("UserId", user.UserId);
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetString("DisplayName", user.GetDisplayName());
            if (!string.IsNullOrEmpty(user.EmpId)) HttpContext.Session.SetString("EmpId", user.EmpId);

            return RedirectBasedOnRole(user.Role);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        private IActionResult RedirectBasedOnRole(string? role)
        {
            return role == "Student" ? Redirect("/home") : Redirect("/Employee");
        }
    }
}
