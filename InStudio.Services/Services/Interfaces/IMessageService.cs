using InStudio.Services.Dtos.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Services.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> SendMessageAsync(CreateMessageDto dto);
        Task<IEnumerable<MessageDto>> GetMessagesByUserIdAsync(Guid userId);
    }
}
