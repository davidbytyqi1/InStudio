using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.UserSubscriptionType
{
    public sealed record UpdateUserSubscriptionTypeDto
    {
        public int Id { get; init; }
        public required string Title { get; init; }
        public decimal? Price { get; init; }
        public string? Description { get; init; }
        public int? ApplicationNumber { get; init; }
        public bool? HasDashboardBenefits { get; init; }
        public bool? HasProfileListBenefits { get; init; }
    }
}
