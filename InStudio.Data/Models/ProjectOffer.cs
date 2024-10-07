using System;
using System.Collections.Generic;

namespace InStudio.Data.Models;

public partial class ProjectOffer
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public Guid? DesignerId { get; set; }

    public decimal? Price { get; set; }

    public string? Comments { get; set; }

    public string? CoverLetter { get; set; }

    public bool? IsAccepted { get; set; }

    public bool? IsSeen { get; set; }

    public bool? IsWinner { get; set; }

    public int? ConnectionsNumber { get; set; }

    public string? Feedback { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Project? Project { get; set; }
}