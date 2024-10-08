using InStudio.Services.Dtos.DesignCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.UserProfileDesignCategory
{
    public class UserProfileDesignCategoryFilterDto
    {
        public int? UserProfileId { get; set; }
        public string? Description { get; set; }
        public required string Title { get; set; }
        public int? DesignCategoryId { get; set; }

        public DesignCategoryFilterDto DesignCategory { get; set; }
    }
}
