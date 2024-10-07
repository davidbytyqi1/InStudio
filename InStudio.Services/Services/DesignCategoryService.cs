using InStudio.Services.Repositories.Interfaces;
using InStudio.Services.Dtos.DesignCategory;
using Mapster;
using InStudio.Data.Models;
using InStudio.Common.Services.Interfaces;
using InStudio.Services.Services.Interfaces;

namespace InStudio.Services.Services
{
    public class DesignCategoryService : IDesignCategoryService
    {
        private readonly IDesignCategoryRepository _designCategoryRepository;
        private readonly IScopeContext _scopeContext;

        public DesignCategoryService(
            IDesignCategoryRepository designCategoryRepository,
            IScopeContext scopeContext)
        {
            _designCategoryRepository = designCategoryRepository;
            _scopeContext = scopeContext;
        }

        public async Task<DesignCategoryDto> CreateCategoryAsync(CreateDesignCategoryDto dto)
        {
            var categoryEntity = dto.Adapt<DesignCategory>();
            categoryEntity.CreatedBy = _scopeContext.UserId;
            categoryEntity.CreatedDate = DateTime.UtcNow;

            await _designCategoryRepository.AddAsync(categoryEntity);
            await _designCategoryRepository.SaveChangesAsync();

            return categoryEntity.Adapt<DesignCategoryDto>();
        }

        public async Task<IEnumerable<DesignCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _designCategoryRepository.GetAllAsync();
            return categories.Adapt<IEnumerable<DesignCategoryDto>>();
        }

        public async Task<DesignCategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _designCategoryRepository.FindAsync(c => c.Id == categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
            }

            return category.Adapt<DesignCategoryDto>();
        }

        public async Task UpdateCategoryAsync(UpdateDesignCategoryDto dto)
        {
            var existingCategory = await _designCategoryRepository.FindAsync(c => c.Id == dto.Id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {dto.Id} not found.");
            }

            dto.Adapt(existingCategory);
            existingCategory.UpdatedBy = _scopeContext.UserId;
            existingCategory.UpdatedDate = DateTime.UtcNow;

            _designCategoryRepository.Update(existingCategory);
            await _designCategoryRepository.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _designCategoryRepository.FindAsync(c => c.Id == categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
            }

            await _designCategoryRepository.DeleteAsync(category);
        }
    }
}
