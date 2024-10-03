using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.UserSubscriptionType
{
    public sealed record UserSubscriptionTypeDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public decimal? Price { get; set; }

        public string? Description { get; set; }

        public int? ApplicationNumber { get; set; }

        public bool? HasDashboardBenefits { get; set; }

        public bool? HasProfileListBenefits { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
