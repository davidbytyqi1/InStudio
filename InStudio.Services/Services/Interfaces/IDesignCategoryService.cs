using InStudio.Services.Dtos.DesignCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Services.Interfaces
{
    public interface IDesignCategoryService
    {
        Task<DesignCategoryDto> CreateCategoryAsync(CreateDesignCategoryDto dto);
        Task<IEnumerable<DesignCategoryDto>> GetAllCategoriesAsync();
        Task<DesignCategoryDto> GetCategoryByIdAsync(int categoryId);
        Task UpdateCategoryAsync(UpdateDesignCategoryDto dto);
        Task DeleteCategoryAsync(int categoryId);
    }
}

