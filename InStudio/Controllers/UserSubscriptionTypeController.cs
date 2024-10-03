﻿using InStudio.Services.Dtos.UserSubscriptionType;
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
    public class UserSubscriptionTypeController : ControllerBase
    {
        private readonly IUserSubscriptionTypeService _userSubscriptionTypeService;

        public UserSubscriptionTypeController(IUserSubscriptionTypeService userSubscriptionTypeService)
        {
            _userSubscriptionTypeService = userSubscriptionTypeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserSubscriptionTypeDto>>> GetAllSubscriptionTypes([FromQuery] FilterUserSubscriptionTypeDto filterDto)
        {
            var subscriptionTypes = await _userSubscriptionTypeService.GetSubscriptionTypeListAsync(filterDto, null, null);
            return Ok(subscriptionTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserSubscriptionTypeDto>> GetSubscriptionTypeById(int id)
        {
            var subscriptionType = await _userSubscriptionTypeService.GetSubscriptionTypeByIdAsync(id);

            if (subscriptionType == null)
                return NotFound($"Subscription Type with ID {id} not found.");

            return Ok(subscriptionType);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<UserSubscriptionTypeDto>> CreateSubscriptionType([FromBody] CreateUserSubscriptionTypeDto subscriptionTypeDto)
        {
            if (subscriptionTypeDto == null)
                return BadRequest("Invalid subscription type data.");

            var createdSubscriptionType = await _userSubscriptionTypeService.CreateSubscriptionTypeAsync(subscriptionTypeDto);
            return CreatedAtAction(nameof(GetSubscriptionTypeById), new { id = createdSubscriptionType.Id }, createdSubscriptionType);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSubscriptionType(int id, [FromBody] UpdateUserSubscriptionTypeDto subscriptionTypeDto)
        {
            if (id != subscriptionTypeDto.Id)
                return BadRequest("Subscription Type ID mismatch.");

            var existingSubscriptionType = await _userSubscriptionTypeService.GetSubscriptionTypeByIdAsync(id);
            if (existingSubscriptionType == null)
                return NotFound($"Subscription Type with ID {id} not found.");

            await _userSubscriptionTypeService.UpdateSubscriptionTypeAsync(subscriptionTypeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSubscriptionType(int id)
        {
            var existingSubscriptionType = await _userSubscriptionTypeService.GetSubscriptionTypeByIdAsync(id);
            if (existingSubscriptionType == null)
                return NotFound($"Subscription Type with ID {id} not found.");

            await _userSubscriptionTypeService.DeleteSubscriptionTypeAsync(id);
            return NoContent();
        }
    }
}
