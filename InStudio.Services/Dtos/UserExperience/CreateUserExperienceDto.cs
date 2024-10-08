using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.UserExperience
{
    public class CreateUserExperienceDto
    {
        public int? UserProfileId { get; set; }
        public string? Position { get; set; }
        public string? Company { get; set; }
    }
}
