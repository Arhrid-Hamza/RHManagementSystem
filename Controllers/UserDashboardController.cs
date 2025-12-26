using Microsoft.AspNetCore.Mvc;

namespace RHManagementSystem.Controllers
{
    public class UserDashboardController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "user")
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public IActionResult Employees() => View();
        public IActionResult Departments() => View();
        public IActionResult Projects() => View();
        public IActionResult Reports() => View();

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
