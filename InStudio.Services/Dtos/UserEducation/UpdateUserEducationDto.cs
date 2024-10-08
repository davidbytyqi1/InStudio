using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.UserEducation
{
    public class UpdateUserEducationDto
    {
        public int Id { get; set; }
        public int? UserProfileId { get; set; }
        public string? EducationTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
    }
}
