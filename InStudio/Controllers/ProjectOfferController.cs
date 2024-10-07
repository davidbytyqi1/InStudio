using InStudio.Attributes;
using InStudio.Extensions;
using InStudio.Filters.Requests.ProjectOffer;
using InStudio.Filters.Response.ProjectOffer;
using InStudio.Services.Dtos.ProjectOffer;
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
    public class ProjectOfferController : ControllerBase
    {
        private readonly IProjectOfferService _projectOfferService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectOfferController(IProjectOfferService projectOfferService, IHttpContextAccessor httpContextAccessor)
        {
            _projectOfferService = projectOfferService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProjectOfferDto>>> GetAllProjectOffers()
        {
            var user = HttpContext.User;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            foreach (var role in roles)
            {
                Console.WriteLine(role);
            }

            var projectOffers = await _projectOfferService.GetAllProjectOffersAsync();
            return Ok(projectOffers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectOfferDto>> GetProjectOfferById(int id)
        {
            var projectOffer = await _projectOfferService.GetProjectOfferByIdAsync(id);

            if (projectOffer == null)
                return NotFound($"Project Offer with ID {id} not found.");

            return Ok(projectOffer);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ProjectOfferDto>> CreateProjectOffer([FromBody] CreateProjectOfferDto projectOfferDto)
        {
            if (projectOfferDto == null)
                return BadRequest("Invalid project offer data.");

            var createdProjectOffer = await _projectOfferService.CreateProjectOfferAsync(projectOfferDto);
            return CreatedAtAction(nameof(GetProjectOfferById), new { id = createdProjectOffer.Id }, createdProjectOffer);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProjectOffer(int id, [FromBody] UpdateProjectOfferDto projectOfferDto)
        {
            if (id != projectOfferDto.Id)
                return BadRequest("Project Offer ID mismatch.");

            var existingProjectOffer = await _projectOfferService.GetProjectOfferByIdAsync(id);
            if (existingProjectOffer == null)
                return NotFound($"Project Offer with ID {id} not found.");

            await _projectOfferService.UpdateProjectOfferAsync(projectOfferDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProjectOffer(int id)
        {
            var existingProjectOffer = await _projectOfferService.GetProjectOfferByIdAsync(id);
            if (existingProjectOffer == null)
                return NotFound($"Project Offer with ID {id} not found.");

            await _projectOfferService.DeleteProjectOfferAsync(id);
            return NoContent();
        }

        [HttpGet("Filter")]
        [PageableAndSortable]
        [AllowAnonymous]
        public async Task<IActionResult> GetProjectOffers([FromQuery] ProjectOfferFilterRequest model)
        {
            var projectOffers = await _projectOfferService.GetProjectOfferListAsync(
                model.Adapt<ProjectOfferFilterDto>(),
                _httpContextAccessor.GetPageableParams(),
                _httpContextAccessor.GetSortParams<ProjectOfferFilterResponse>());

            return projectOffers.TotalCount > 0 ? new OkObjectResult(new { Data = projectOffers.Adapt<IList<ProjectOfferFilterResponse>>(), Count = projectOffers.TotalCount }) : new NoContentResult();
        }
    }
}
