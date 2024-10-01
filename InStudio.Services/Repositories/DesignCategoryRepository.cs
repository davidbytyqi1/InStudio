using InStudio.Data;
using InStudio.Data.Models;
using InStudio.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InStudio.Services.Repositories
{
    public class DesignCategoryRepository : GenericRepository<DesignCategory>, IDesignCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public DesignCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DesignCategory>> GetByParentIdAsync(int parentId)
        {
            return await _context.Set<DesignCategory>()
                .Where(c => c.ParentId == parentId)
                .ToListAsync();
        }
    }
}
