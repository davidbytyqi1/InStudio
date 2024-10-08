using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.UserProfile
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public string? Title { get; set; }
        public required string Username { get; set; }
        public string? Description { get; set; }
        public decimal? Rate { get; set; }
        public string? ImagePath { get; set; }
        public int? ConnectionsNumber { get; set; }
        public bool? IsAgency { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
