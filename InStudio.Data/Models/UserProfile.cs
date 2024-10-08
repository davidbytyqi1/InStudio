using InStudio.Data.Models;
using System;
using System.Collections.Generic;

namespace InStudio.Data.Models;

public partial class UserProfile
{
    public int Id { get; set; }

    public Guid? UserId { get; set; }

    public string? Title { get; set; }
    public required string Username { get; set; }

    public string? Description { get; set; }

    public decimal? Rate { get; set; }

    public string? ImagePath { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ConnectionsNumber { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsAgency { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<UserEducation> UserEducations { get; set; } = new List<UserEducation>();

    public virtual ICollection<UserExperience> UserExperiences { get; set; } = new List<UserExperience>();
    public virtual ICollection<UserProfileDesignCategory> UserProfileDesignCategory { get; set; } = new List<UserProfileDesignCategory>();
}
