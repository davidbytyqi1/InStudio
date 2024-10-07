using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.Message
{
    public sealed record CreateMessageDto
    {
        public Guid? UserId { get; init; }
        public string Text { get; init; }
    }
}
