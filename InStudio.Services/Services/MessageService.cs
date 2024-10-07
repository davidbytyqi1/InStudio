using InStudio.Services.Repositories.Interfaces;
using InStudio.Services.Dtos.Message;
using Mapster;
using InStudio.Data.Models;
using InStudio.Common.Services.Interfaces;
using InStudio.Services.Services.Interfaces;

namespace InStudio.Services.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IScopeContext _scopeContext;

        public MessageService(IMessageRepository messageRepository, IScopeContext scopeContext)
        {
            _messageRepository = messageRepository;
            _scopeContext = scopeContext;
        }

        public async Task<MessageDto> SendMessageAsync(CreateMessageDto dto)
        {
            var messageEntity = dto.Adapt<Message>();
            messageEntity.CreatedBy = _scopeContext.UserId;
            messageEntity.CreatedDate = DateTime.UtcNow;

            await _messageRepository.AddAsync(messageEntity);
            await _messageRepository.SaveChangesAsync();

            return messageEntity.Adapt<MessageDto>();
        }

        public async Task<IEnumerable<MessageDto>> GetMessagesByUserIdAsync(Guid userId)
        {
            var loggedInUserId = _scopeContext.UserId;
            var messages = await _messageRepository.FindAllAsync(m => m.UserId == userId && m.CreatedBy == loggedInUserId);
            return messages.Adapt<IEnumerable<MessageDto>>();
        }

    }
}