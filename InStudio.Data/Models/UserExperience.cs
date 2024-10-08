using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Data.Models
{
    public partial class UserExperience
    {
        public int Id { get; set; }

        public int? UserProfileId { get; set; }

        public string? Position { get; set; }

        public string? Company { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual UserProfile? UserProfile { get; set; }
    }

}
