using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RHManagementSystem.Data;
using RHManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RHManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly RHManagementContext _context;

        public ProjectsController(RHManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.EmployeeResponsibleNavigation)
                .Include(p => p.DepartmentResponsibleNavigation)
                .ToListAsync();

            var projectsWithNames = projects.Select(p => new
            {
                p.ProjectId,
                p.Name,
                p.Description,
                p.DepartmentResponsible,
                p.StartDate,
                p.EndDate,
                p.EmployeeResponsible,
                EmployeeResponsibleName = p.EmployeeResponsibleNavigation?.Name ?? "",
                DepartmentResponsibleName = p.DepartmentResponsibleNavigation?.Name ?? ""
            });

            return Ok(projectsWithNames);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(long id, [FromBody] Project updatedProject)
        {
            if (id != updatedProject.ProjectId) return BadRequest();

            _context.Entry(updatedProject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id)) return NotFound();
                else throw;
            }

            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Project deleted successfully" });
        }

        private bool ProjectExists(long id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}
