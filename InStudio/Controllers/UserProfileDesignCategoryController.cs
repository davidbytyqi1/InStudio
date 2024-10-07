using InStudio.Extensions;
using InStudio.Services.Dtos.UserProfileDesignCategory;
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
    public class UserProfileDesignCategoryController : ControllerBase
    {
        private readonly IUserProfileDesignCategoryService _userProfileDesignCategoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProfileDesignCategoryController(
            IUserProfileDesignCategoryService userProfileDesignCategoryService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userProfileDesignCategoryService = userProfileDesignCategoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserProfileDesignCategoryDto>>> GetAllUserProfileDesignCategories()
        {
            var user = HttpContext.User;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            foreach (var role in roles)
            {
                Console.WriteLine(role);
            }

            var categories = await _userProfileDesignCategoryService.GetAllUserProfileDesignCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDesignCategoryDto>> GetUserProfileDesignCategoryById(int id)
        {
            var category = await _userProfileDesignCategoryService.GetUserProfileDesignCategoryByIdAsync(id);

            if (category == null)
                return NotFound($"UserProfileDesignCategory with ID {id} not found.");

            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<UserProfileDesignCategoryDto>> CreateUserProfileDesignCategory([FromBody] CreateUserProfileDesignCategoryDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid category data.");

            var createdCategory = await _userProfileDesignCategoryService.CreateUserProfileDesignCategoryAsync(dto);
            return CreatedAtAction(nameof(GetUserProfileDesignCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserProfileDesignCategory(int id, [FromBody] UpdateUserProfileDesignCategoryDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Category ID mismatch.");

            var existingCategory = await _userProfileDesignCategoryService.GetUserProfileDesignCategoryByIdAsync(id);
            if (existingCategory == null)
                return NotFound($"UserProfileDesignCategory with ID {id} not found.");

            await _userProfileDesignCategoryService.UpdateUserProfileDesignCategoryAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserProfileDesignCategory(int id)
        {
            var existingCategory = await _userProfileDesignCategoryService.GetUserProfileDesignCategoryByIdAsync(id);
            if (existingCategory == null)
                return NotFound($"UserProfileDesignCategory with ID {id} not found.");

            await _userProfileDesignCategoryService.DeleteUserProfileDesignCategoryAsync(id);
            return NoContent();
        }

        [HttpGet("Filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserProfileDesignCategories([FromQuery] UserProfileDesignCategoryFilterDto filterDto)
        {
            var categories = await _userProfileDesignCategoryService.GetUserProfileDesignCategoryListAsync(
                filterDto,
                _httpContextAccessor.GetPageableParams(),
                _httpContextAccessor.GetSortParams<UserProfileDesignCategoryDto>());

            return categories.TotalCount > 0 ? new OkObjectResult(new { Data = categories.Adapt<IList<UserProfileDesignCategoryDto>>(), Count = categories.TotalCount }) : new NoContentResult();
        }
    }
}
