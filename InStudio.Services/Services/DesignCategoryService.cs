using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using InStudio.Services.Repositories.Interfaces;
using InStudio.Services.Services.Interfaces;
using InStudio.Services.Dtos.DesignCategory;
using Mapster;
using InStudio.Data.Models;
using Microsoft.AspNetCore.Http;

namespace InStudio.Services.Services
{
    public class DesignCategoryService : IDesignCategoryService
    {
        private readonly IDesignCategoryRepository _designCategoryRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DesignCategoryService(
            IDesignCategoryRepository designCategoryRepository,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _designCategoryRepository = designCategoryRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<Guid> GetCurrentUserIdAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            var identityUser = await _userManager.GetUserAsync(user);
            return identityUser != null ? Guid.Parse(identityUser.Id) : throw new UnauthorizedAccessException("User is not logged in.");
        }

        public async Task<DesignCategoryDto> CreateCategoryAsync(CreateDesignCategoryDto dto)
        {
            var categoryEntity = dto.Adapt<DesignCategory>();
            categoryEntity.CreatedBy = await GetCurrentUserIdAsync();
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
            existingCategory.UpdatedBy = await GetCurrentUserIdAsync();
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
