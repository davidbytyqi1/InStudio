using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.UserSubscriptionType
{
    public sealed record UserSubscriptionTypeFilterDto
    {
        public string? Title { get; init; }
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
        public decimal? Price { get; init; }
        public int? ApplicationNumber { get; init; }
        public bool? HasDashboardBenefits { get; init; }
        public bool? HasProfileListBenefits { get; init; }
    }
}
