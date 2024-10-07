using System;
using System.Collections.Generic;

namespace InStudio.Data.Models;

public partial class Message
{
    public int Id { get; set; }

    public Guid? UserId { get; set; }

    public string? Text { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }
}
