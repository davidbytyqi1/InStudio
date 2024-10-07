using InStudio.Data;
using InStudio.Data.Models;
using InStudio.Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Repositories
{
    public class ProjectImageRepository : GenericRepository<ProjectImage>, IProjectImageRepository
    {
        public ProjectImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
