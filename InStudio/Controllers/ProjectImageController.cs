using InStudio.Services.Dtos.ProjectImage;
using InStudio.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InStudio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectImageController : ControllerBase
    {
        private readonly IProjectImageService _projectImageService;

        public ProjectImageController(IProjectImageService projectImageService)
        {
            _projectImageService = projectImageService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProjectImageDto>>> GetAllProjectImages()
        {
            var images = await _projectImageService.GetAllProjectImagesAsync();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectImageDto>> GetProjectImageById(int id)
        {
            var image = await _projectImageService.GetProjectImageByIdAsync(id);
            if (image == null)
                return NotFound($"Project image with ID {id} not found.");

            return Ok(image);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Designer")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ProjectImageDto>> CreateProjectImage([FromForm] ProjectImageCreateDto projectImageCreateDto)
        {
            if (projectImageCreateDto == null || projectImageCreateDto.ImageFile == null)
                return BadRequest("Invalid project image data.");

            await _projectImageService.CreateProjectImageAsync(projectImageCreateDto);
            return Ok("Project image created successfully.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Designer")]
        public async Task<IActionResult> UpdateProjectImage(int id, [FromForm] ProjectImageDto projectImageDto)
        {

            var existingImage = await _projectImageService.GetProjectImageByIdAsync(id);
            if (existingImage == null)
                return NotFound($"Project image with ID {id} not found.");

            await _projectImageService.UpdateProjectImageAsync(id, projectImageDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Designer")]
        public async Task<IActionResult> DeleteProjectImage(int id)
        {
            var existingImage = await _projectImageService.GetProjectImageByIdAsync(id);
            if (existingImage == null)
                return NotFound($"Project image with ID {id} not found.");

            await _projectImageService.DeleteProjectImageAsync(id);
            return NoContent();
        }
    }
}
