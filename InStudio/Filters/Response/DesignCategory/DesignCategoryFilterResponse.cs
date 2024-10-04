using InStudio.Attributes;
using System.ComponentModel.DataAnnotations;

namespace InStudio.Filters.Response.DesignCategory
{
    internal sealed record DesignCategoryFilterResponse
    {
        [Required]
        public string Id { get; init; } = default!;

        [Required]
        [SortableColumn(Name = "Title")]
        public string Title { get; init; } = default!;

        public string? Description { get; set; }
    }
}
