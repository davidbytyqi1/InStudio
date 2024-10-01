using InStudio.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InStudio.Services.Repositories.Interfaces
{
    public interface IDesignCategoryRepository : IGenericRepository<DesignCategory>
    {
        Task<IEnumerable<DesignCategory>> GetByParentIdAsync(int parentId);
    }
}
