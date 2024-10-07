using InStudio.Services.Dtos.UserProfile;
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
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserProfileDto>>> GetAllUserProfiles()
        {
            var user = HttpContext.User;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            // Print roles for debugging
            foreach (var role in roles)
            {
                Console.WriteLine(role);
            }

            var userProfiles = await _userProfileService.GetAllUserProfilesAsync();
            return Ok(userProfiles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfileById(int id)
        {
            var userProfile = await _userProfileService.GetUserProfileByIdAsync(id);

            if (userProfile == null)
                return NotFound($"UserProfile with ID {id} not found.");

            return Ok(userProfile);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<UserProfileDto>> CreateUserProfile([FromBody] CreateUserProfileDto userProfileDto)
        {
            if (userProfileDto == null)
                return BadRequest("Invalid user profile data.");

            var createdUserProfile = await _userProfileService.CreateUserProfileAsync(userProfileDto);
            return CreatedAtAction(nameof(GetUserProfileById), new { id = createdUserProfile.Id }, createdUserProfile);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserProfile(int id, [FromBody] UpdateUserProfileDto userProfileDto)
        {
            if (id != userProfileDto.Id)
                return BadRequest("UserProfile ID mismatch.");

            var existingUserProfile = await _userProfileService.GetUserProfileByIdAsync(id);
            if (existingUserProfile == null)
                return NotFound($"UserProfile with ID {id} not found.");

            await _userProfileService.UpdateUserProfileAsync(userProfileDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            var existingUserProfile = await _userProfileService.GetUserProfileByIdAsync(id);
            if (existingUserProfile == null)
                return NotFound($"UserProfile with ID {id} not found.");

            await _userProfileService.DeleteUserProfileAsync(id);
            return NoContent();
        }
    }
}
