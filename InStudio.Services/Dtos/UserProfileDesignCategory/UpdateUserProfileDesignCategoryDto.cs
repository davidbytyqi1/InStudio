using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.UserProfileDesignCategory
{
    public class UpdateUserProfileDesignCategoryDto
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int? DesignCategoryId { get; set; }
        public string? Description { get; set; }
    }
}
