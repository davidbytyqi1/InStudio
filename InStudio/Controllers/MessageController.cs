using InStudio.Services.Dtos.Message;
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
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> SendMessage([FromBody] CreateMessageDto messageDto)
        {
            if (messageDto == null)
                return BadRequest("Invalid message data.");

            var createdMessage = await _messageService.SendMessageAsync(messageDto);
            return CreatedAtAction(nameof(GetMessagesByUserId), new { userId = createdMessage.UserId }, createdMessage);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesByUserId(Guid userId)
        {
            var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (loggedInUserId == null)
                return Unauthorized("Invalid user context.");

            var messages = await _messageService.GetMessagesByUserIdAsync(userId);

            if (messages == null)
                return NotFound($"No messages found for User ID {userId}.");

            return Ok(messages);
        }
    }
}
