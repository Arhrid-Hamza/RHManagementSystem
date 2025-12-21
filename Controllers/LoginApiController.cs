using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RHManagementSystem.Data;
using RHManagementSystem.Models;

namespace RHManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly RHManagementContext _context;

        public LoginController(RHManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Email and password are required.";
                return View("Index");
            }

            // Check in Users collection
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("UserRole", user.Role ?? "user");
                HttpContext.Session.SetString("UserName", user.Name ?? "");

                if (user.Role == "admin")
                {
                    return RedirectToAction("Index", "AdminDashboard");
                }
                else
                {
                    return RedirectToAction("Index", "UserDashboard");
                }
            }

            // Check in Employees collection
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Mail == email && e.Password == password);
            if (employee != null)
            {
                HttpContext.Session.SetString("UserId", employee.EmployeeId.ToString());
                HttpContext.Session.SetString("UserRole", employee.Role ?? "employee");
                HttpContext.Session.SetString("UserName", employee.Name ?? "");

                return RedirectToAction("Index", "UserDashboard");
            }

            ViewBag.Error = "Invalid email or password.";
            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
