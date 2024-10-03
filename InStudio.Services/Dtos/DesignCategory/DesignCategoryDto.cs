using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.DesignCategory
{
    public sealed record DesignCategoryDto
    {
        public int Id { get; init; }
        public string Title { get; init; } = null!;
        public string? Description { get; init; }
        public int? ParentId { get; init; }
        public Guid CreatedBy { get; init; }
        public DateTime CreatedDate { get; init; }
        public Guid? UpdatedBy { get; init; }
        public DateTime? UpdatedDate { get; init; }
    }
}
