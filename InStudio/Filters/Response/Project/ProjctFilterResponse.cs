using InStudio.Attributes;
using InStudio.Filters.Response.DesignCategory;
using System.ComponentModel.DataAnnotations;

namespace InStudio.Filters.Response.Project
{
    internal sealed record ProjctFilterResponse
    {
        [Required]
        public string Id { get; init; } = default!;

        [Required]
        [SortableColumn(Name = "Title")]
        public string Title { get; init; } = default!;

        public decimal? Price { get; set; }

        public string? Description { get; set; }

        public int? ConnectionsNumber { get; set; }

        public string? EmployeeFeedback { get; set; }

        public string? DesignerFeedback { get; set; }

        public DesignCategoryFilterResponse DesignCategory { get; set; }
    }
}
