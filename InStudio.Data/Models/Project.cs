using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Data.Models
{
    public partial class Project
    {
        public int Id { get; set; }

        public int? DesignCategoryId { get; set; }

        public decimal? Price { get; set; }

        public int? UserProfileId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? ConnectionsNumber { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? EmployeeFeedback { get; set; }

        public string? DesignerFeedback { get; set; }

        public virtual DesignCategory? DesignCategory { get; set; }

        public virtual ICollection<ProjectImage> ProjectImages { get; set; } = new List<ProjectImage>();

        public virtual ICollection<ProjectOffer> ProjectOffers { get; set; } = new List<ProjectOffer>();

        public virtual UserProfile? UserProfile { get; set; }
    }
}
