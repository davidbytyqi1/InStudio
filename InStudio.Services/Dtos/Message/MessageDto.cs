using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public string? Text { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
