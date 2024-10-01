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
            public required string Title { get; init; }
            public string? Description { get; init; }
            public int? ParentId { get; init; }
        }
}
