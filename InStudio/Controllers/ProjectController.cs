using InStudio.Attributes;
using InStudio.Extensions;
using InStudio.Filters.Requests.Project;
using InStudio.Filters.Response.Project;
using InStudio.Filters.Response.UserSubscriptionType;
using InStudio.Services.Dtos.Project;
using InStudio.Services.Services.Interfaces;
using Mapster;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProjectController(IProjectService projectService, IHttpContextAccessor httpContextAccessor)
        {
            _projectService = projectService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            var user = HttpContext.User;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

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

        [HttpGet("Filter")]
        [PageableAndSortable]
        [AllowAnonymous]
        public async Task<IActionResult> GetProjects([FromQuery] ProjectFilterRequest model)
        {
            var projects = await _projectService.GetProjectListAsync(
                model.Adapt<ProjectFilterDto>(),
                _httpContextAccessor.GetPageableParams(),
                _httpContextAccessor.GetSortParams<ProjctFilterResponse>());

            return projects.TotalCount > 0 ? new OkObjectResult(new { Data = projects.Adapt<IList<ProjctFilterResponse>>(), Count = projects.TotalCount }) : new NoContentResult();
        }
    }
}
