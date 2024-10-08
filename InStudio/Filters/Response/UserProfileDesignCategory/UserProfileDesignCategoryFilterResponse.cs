using InStudio.Attributes;
using InStudio.Filters.Response.DesignCategory;
using InStudio.Services.Dtos.DesignCategory;
using System.ComponentModel.DataAnnotations;

namespace InStudio.Filters.Response.UserProfileDesignCategory
{
    internal class UserProfileDesignCategoryFilterResponse
    {
        [Required]
        [SortableColumn(Name = "UserProfileId")]
        public int UserProfileId { get; set; }
        public string? Description { get; set; }
        [Required]
        [SortableColumn(Name = "Title")]
        public required string Title { get; set; }
        [Required]
        [SortableColumn(Name = "DesignCategoryId")]
        public int? DesignCategoryId { get; set; }
        [Required]
        [SortableColumn(Name = "Username")]
        public string Username { get; set; }

        public required DesignCategoryFilterResponse DesignCategory { get; set; }
    }
}
