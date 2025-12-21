using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RHManagementSystem.Data;
using RHManagementSystem.Models;

namespace RHManagementSystem.Controllers
{
    public class ProfileController : Controller
    {
        private readonly RHManagementContext _context;

        public ProfileController(RHManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(userIdString) || string.IsNullOrEmpty(userRole))
            {
                return RedirectToAction("Index", "LoginMVC");
            }

            if (long.TryParse(userIdString, out long userId))
            {
                if (userRole == "admin" || userRole == "user")
                {
                    var user = await _context.Users.FindAsync(userId);
                    if (user != null)
                    {
                        ViewBag.User = user;
                        ViewBag.UserType = "User";
                        return View();
                    }
                }
                else if (userRole == "employee")
                {
                    var employee = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmployeeId == userId);
                    if (employee != null)
                    {
                        ViewBag.User = employee;
                        ViewBag.UserType = "Employee";
                        return View();
                    }
                }
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(User updatedUser)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(userIdString) || string.IsNullOrEmpty(userRole))
            {
                return RedirectToAction("Index", "LoginMVC");
            }

            if (long.TryParse(userIdString, out long userId))
            {
                if (userRole == "admin" || userRole == "user")
                {
                    var user = await _context.Users.FindAsync(userId);
                    if (user != null)
                    {
                        user.Name = updatedUser.Name;
                        user.Email = updatedUser.Email;
                        // Note: Password update should be handled separately with proper hashing

                        await _context.SaveChangesAsync();
                        TempData["Message"] = "Profile updated successfully!";
                        return RedirectToAction("Index");
                    }
                }
                else if (userRole == "employee")
                {
                    var employee = await _context.Employees.FindAsync(userId);
                    if (employee != null)
                    {
                        employee.Name = updatedUser.Name;
                        employee.Mail = updatedUser.Email;
                        // Note: Password update should be handled separately with proper hashing

                        await _context.SaveChangesAsync();
                        TempData["Message"] = "Profile updated successfully!";
                        return RedirectToAction("Index");
                    }
                }
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
