﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Dtos.ProjectImage
{
    public class ProjectImageCreateDto
    {
        public int? ProjectId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
