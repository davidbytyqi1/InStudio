using InStudio.Attributes;
using System.ComponentModel.DataAnnotations;

namespace InStudio.Filters.Response.UserSubscriptionType
{
    internal sealed record UserSubscriptionTypeFilterResponse
    {
        [Required]
        public string Id { get; init; } = default!;

        [Required]
        [SortableColumn(Name = "Title")]
        public string Title { get; init; } = default!;

        [Required]
        [SortableColumn(Name = "Price")]
        public decimal Price { get; init; }

        public int? ApplicationNumber { get; init; }

        public bool? HasDashboardBenefits { get; init; }

        public bool? HasProfileListBenefits { get; init; }

    }
}
