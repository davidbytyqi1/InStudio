using System;
using System.Collections.Generic;

namespace InStudio.Data.Models;

public partial class DesignCategory
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? ParentId { get; set; }

    public virtual ICollection<DesignCategory> InverseParent { get; set; } = new List<DesignCategory>();

    public virtual DesignCategory? Parent { get; set; }
}
