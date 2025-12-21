using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RHManagementSystem.Data;
using RHManagementSystem.Models;

namespace RHManagementSystem.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly RHManagementContext _context;

        public AdminDashboardController(RHManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "admin")
            {
                return RedirectToAction("Index", "LoginMVC");
            }

            var employeeCount = await _context.Employees.CountAsync();
            var departmentCount = await _context.Departments.CountAsync();

            ViewBag.EmployeeCount = employeeCount;
            ViewBag.DepartmentCount = departmentCount;

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
