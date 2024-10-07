using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.ProjectOffer
{
    public class ProjectOfferDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public decimal Price { get; set; }
        public string CoverLetter { get; set; }
        public string Comments { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsSeen { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

