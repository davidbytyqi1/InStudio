using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.DesignCategory
{
    public sealed record CreateDesignCategoryDto
    {
        public required string Title { get; init; }
        public string? Description { get; init; }
        public int? ParentId { get; init; }
    }
}
