using InStudio.Services.Dtos.UserExperience;
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
    public class UserExperienceController : ControllerBase
    {
        private readonly IUserExperienceService _userExperienceService;

        public UserExperienceController(IUserExperienceService userExperienceService)
        {
            _userExperienceService = userExperienceService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserExperienceDto>>> GetAllExperiences()
        {
            var experiences = await _userExperienceService.GetAllExperiencesAsync();
            return Ok(experiences);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserExperienceDto>> GetExperienceById(int id)
        {
            var experience = await _userExperienceService.GetExperienceByIdAsync(id);

            if (experience == null)
                return NotFound($"Experience with ID {id} not found.");

            return Ok(experience);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<UserExperienceDto>> CreateExperience([FromBody] CreateUserExperienceDto experienceDto)
        {
            if (experienceDto == null)
                return BadRequest("Invalid experience data.");

            var createdExperience = await _userExperienceService.CreateExperienceAsync(experienceDto);
            return CreatedAtAction(nameof(GetExperienceById), new { id = createdExperience.Id }, createdExperience);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateExperience(int id, [FromBody] UpdateUserExperienceDto experienceDto)
        {
            if (id != experienceDto.Id)
                return BadRequest("Experience ID mismatch.");

            var existingExperience = await _userExperienceService.GetExperienceByIdAsync(id);
            if (existingExperience == null)
                return NotFound($"Experience with ID {id} not found.");

            await _userExperienceService.UpdateExperienceAsync(experienceDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var existingExperience = await _userExperienceService.GetExperienceByIdAsync(id);
            if (existingExperience == null)
                return NotFound($"Experience with ID {id} not found.");

            await _userExperienceService.DeleteExperienceAsync(id);
            return NoContent();
        }
    }
}
