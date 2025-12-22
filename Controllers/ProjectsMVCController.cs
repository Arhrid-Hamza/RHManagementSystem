using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RHManagementSystem.Data;
using RHManagementSystem.Models;

namespace RHManagementSystem.Controllers
{
    public class ProjectsMVCController : Controller
    {
        private readonly RHManagementContext _context;

        public ProjectsMVCController(RHManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Include(p => p.EmployeeResponsibleNavigation)
                .Include(p => p.DepartmentResponsibleNavigation)
                .ToListAsync();
            return View(projects);
        }

        public IActionResult Create()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "admin")
            {
                return RedirectToAction("Index", "UserDashboard");
            }
            ViewBag.DepartmentResponsible = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Departments, "DepartmentId", "Name");
            ViewBag.EmployeeResponsible = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Employees, "EmployeeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,DepartmentResponsible,StartDate,EndDate,EmployeeResponsible")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Project created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DepartmentResponsible = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Departments, "DepartmentId", "Name", project.DepartmentResponsible);
            ViewBag.EmployeeResponsible = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Employees, "EmployeeId", "Name", project.EmployeeResponsible);
            return View(project);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewBag.DepartmentResponsible = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Departments, "DepartmentId", "Name", project.DepartmentResponsible);
            ViewBag.EmployeeResponsible = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Employees, "EmployeeId", "Name", project.EmployeeResponsible);
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ProjectId,Name,Description,DepartmentResponsible,StartDate,EndDate,EmployeeResponsible")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Project updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentResponsible"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Departments, "DepartmentId", "Name", project.DepartmentResponsible);
            ViewData["EmployeeResponsible"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Employees, "EmployeeId", "Name", project.EmployeeResponsible);
            return View(project);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.EmployeeResponsibleNavigation)
                .Include(p => p.DepartmentResponsibleNavigation)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.EmployeeResponsibleNavigation)
                .Include(p => p.DepartmentResponsibleNavigation)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Project deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(long id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}
