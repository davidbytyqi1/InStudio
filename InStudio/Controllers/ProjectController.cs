using InStudio.Services.Dtos.Project;
using InStudio.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InStudio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            var user = HttpContext.User;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            // Print roles for debugging
            foreach (var role in roles)
            {
                Console.WriteLine(role);
            }

            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            if (project == null)
                return NotFound($"Project with ID {id} not found.");

            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectDto projectDto)
        {
            if (projectDto == null)
                return BadRequest("Invalid project data.");

            var createdProject = await _projectService.CreateProjectAsync(projectDto);
            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto projectDto)
        {
            if (id != projectDto.Id)
                return BadRequest("Project ID mismatch.");

            var existingProject = await _projectService.GetProjectByIdAsync(id);
            if (existingProject == null)
                return NotFound($"Project with ID {id} not found.");

            await _projectService.UpdateProjectAsync(projectDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var existingProject = await _projectService.GetProjectByIdAsync(id);
            if (existingProject == null)
                return NotFound($"Project with ID {id} not found.");

            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
