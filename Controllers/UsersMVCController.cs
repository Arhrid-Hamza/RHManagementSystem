using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RHManagementSystem.Data;
using RHManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RHManagementSystem.Controllers
{
    public class UsersMVCController : Controller
    {
        private readonly RHManagementContext _context;

        public UsersMVCController(RHManagementContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "admin")
            {
                return RedirectToAction("Index", "UserDashboard");
            }

            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            var userIdString = HttpContext.Session.GetString("UserId");

            // Only admin or the user himself can view
            if (userRole != "admin" && (string.IsNullOrEmpty(userIdString) || long.Parse(userIdString) != id))
            {
                return RedirectToAction("Index", "UserDashboard");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "admin")
            {
                return RedirectToAction("Index", "UserDashboard");
            }
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Role,Password")] User user)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "admin")
            {
                return RedirectToAction("Index", "UserDashboard");
            }

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();

                // Sync role to employee if exists
                if (!string.IsNullOrEmpty(user.Role))
                {
                    var employeeRole = user.Role.ToLower() == "user" ? "employee" : user.Role.ToLower();
                    var employee = await _context.Employees.FindAsync(user.UserId);
                    if (employee != null)
                    {
                        employee.Role = employeeRole;
                        _context.Update(employee);
                        await _context.SaveChangesAsync();
                    }
                }

                TempData["SuccessMessage"] = "User created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            var userIdString = HttpContext.Session.GetString("UserId");

            // Only admin or the user himself can edit
            if (userRole != "admin" && (string.IsNullOrEmpty(userIdString) || long.Parse(userIdString) != id))
            {
                return RedirectToAction("Index", "UserDashboard");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UserId,Name,Email,Role,Password")] User user)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            var userIdString = HttpContext.Session.GetString("UserId");

            if (userRole != "admin" && (string.IsNullOrEmpty(userIdString) || long.Parse(userIdString) != id))
            {
                return RedirectToAction("Index", "UserDashboard");
            }

            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    // Sync role to employee if exists
                    if (!string.IsNullOrEmpty(user.Role))
                    {
                        var employeeRole = user.Role.ToLower() == "user" ? "employee" : user.Role.ToLower();
                        var employee = await _context.Employees.FindAsync(user.UserId);
                        if (employee != null)
                        {
                            employee.Role = employeeRole;
                            _context.Update(employee);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "admin")
            {
                return RedirectToAction("Index", "UserDashboard");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            // Delete corresponding employee if exists
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = "User deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
