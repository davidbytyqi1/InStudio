using System;
using System.Collections.Generic;

namespace InStudio.Data.Models;

public partial class ProjectImage
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public string? ImagePath { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Project? Project { get; set; }
}
