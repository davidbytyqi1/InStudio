using InStudio.Services.Dtos.DesignCategory;
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
    public class DesignCategoryController : ControllerBase
    {
        private readonly IDesignCategoryService _designCategoryService;

        public DesignCategoryController(IDesignCategoryService designCategoryService)
        {
            _designCategoryService = designCategoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DesignCategoryDto>>> GetAllCategories()
        {
            var user = HttpContext.User;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            // Print roles for debugging
            foreach (var role in roles)
            {
                Console.WriteLine(role);
            }
            var categories = await _designCategoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DesignCategoryDto>> GetCategoryById(int id)
        {
            var category = await _designCategoryService.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound($"Category with ID {id} not found.");

            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<DesignCategoryDto>> CreateCategory([FromBody] DesignCategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest("Invalid category data.");

            var createdCategory = await _designCategoryService.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] DesignCategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
                return BadRequest("Category ID mismatch.");

            var existingCategory = await _designCategoryService.GetCategoryByIdAsync(id);
            if (existingCategory == null)
                return NotFound($"Category with ID {id} not found.");

            await _designCategoryService.UpdateCategoryAsync(categoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existingCategory = await _designCategoryService.GetCategoryByIdAsync(id);
            if (existingCategory == null)
                return NotFound($"Category with ID {id} not found.");

            await _designCategoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
