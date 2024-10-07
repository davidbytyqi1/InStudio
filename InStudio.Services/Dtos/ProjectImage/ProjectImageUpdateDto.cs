using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.ProjectImage
{
    public class ProjectImageUpdateDto
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
