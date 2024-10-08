using System;
using System.Collections.Generic;

namespace InStudio.Data.Models;

public partial class UserEducation
{
    public int Id { get; set; }

    public int? UserProfileId { get; set; }

    public string? EducationTitle { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Description { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual UserProfile? UserProfile { get; set; }
}
