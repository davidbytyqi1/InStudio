using InStudio.Services.Dtos.UserEducation;
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
    public class UserEducationController : ControllerBase
    {
        private readonly IUserEducationService _userEducationService;

        public UserEducationController(IUserEducationService userEducationService)
        {
            _userEducationService = userEducationService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserEducationDto>>> GetAllEducations()
        {
            var educations = await _userEducationService.GetAllEducationsAsync();
            return Ok(educations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserEducationDto>> GetEducationById(int id)
        {
            var education = await _userEducationService.GetEducationByIdAsync(id);

            if (education == null)
                return NotFound($"Education with ID {id} not found.");

            return Ok(education);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<UserEducationDto>> CreateEducation([FromBody] CreateUserEducationDto educationDto)
        {
            if (educationDto == null)
                return BadRequest("Invalid education data.");

            var createdEducation = await _userEducationService.CreateEducationAsync(educationDto);
            return CreatedAtAction(nameof(GetEducationById), new { id = createdEducation.Id }, createdEducation);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEducation(int id, [FromBody] UpdateUserEducationDto educationDto)
        {
            if (id != educationDto.Id)
                return BadRequest("Education ID mismatch.");

            var existingEducation = await _userEducationService.GetEducationByIdAsync(id);
            if (existingEducation == null)
                return NotFound($"Education with ID {id} not found.");

            await _userEducationService.UpdateEducationAsync(educationDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var existingEducation = await _userEducationService.GetEducationByIdAsync(id);
            if (existingEducation == null)
                return NotFound($"Education with ID {id} not found.");

            await _userEducationService.DeleteEducationAsync(id);
            return NoContent();
        }
    }
}
