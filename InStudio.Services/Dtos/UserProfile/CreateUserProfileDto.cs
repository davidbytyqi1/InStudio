using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.UserProfile
{
    public class CreateUserProfileDto
    {
        public Guid? UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Rate { get; set; }
        public string? ImagePath { get; set; }
        public int? ConnectionsNumber { get; set; }
    }
}
