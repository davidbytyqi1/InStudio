using InStudio.Services.Dtos.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.ProjectOffer
{
    public class ProjectOfferFilterDto
    {
        public string? Id { get; set; }
        public string? CoverLetter { get; set; }
        public bool? IsAccepted { get; set; }
        public int? ProjectId { get; set; }
        public string? Price { get; init; }
        public string? Comments { get; init; }
        public bool? IsSeen { get; init; }
        public ProjectFilterDto Project { get; set; }
    }
}
