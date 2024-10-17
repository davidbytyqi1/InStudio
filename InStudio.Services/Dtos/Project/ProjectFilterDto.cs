using InStudio.Services.Dtos.DesignCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.Project
{
    public class ProjectFilterDto
    {
        public int Id { get; set; }
        public int? DesignCategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? ConnectionsNumber { get; set; }
        public string? EmployeeFeedback { get; set; }
        public string? DesignerFeedback { get; set; }
        public DesignCategoryDto DesignCategory { get; set; }
    }
}
