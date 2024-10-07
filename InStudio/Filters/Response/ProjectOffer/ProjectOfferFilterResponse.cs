using InStudio.Attributes;
using InStudio.Filters.Response.Project;
using System.ComponentModel.DataAnnotations;

namespace InStudio.Filters.Response.ProjectOffer
{
    internal sealed record ProjectOfferFilterResponse
    {
        [Required]
        public string Id { get; init; } = default!;

        [Required]
        [SortableColumn(Name = "Price")]
        public decimal Price { get; init; }

        [SortableColumn(Name = "CoverLetter")]
        public string? CoverLetter { get; init; }

        public string? Comments { get; init; }

        public bool? IsAccepted { get; init; }

        public bool? IsSeen { get; init; }

        public ProjctFilterResponse? Project { get; init; }
    }
}
