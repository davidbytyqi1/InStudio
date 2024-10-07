using InStudio.Common.Types;
using InStudio.Common.Services.Interfaces;
using InStudio.Data.Models;
using InStudio.Services.Dtos.UserProfileDesignCategory;
using InStudio.Services.Repositories.Interfaces;
using InStudio.Services.Services.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InStudio.Common;

namespace InStudio.Services.Services
{
    public class UserProfileDesignCategoryService : IUserProfileDesignCategoryService
    {
        private readonly IUserProfileDesignCategoryRepository _userProfileDesignCategoryRepository;
        private readonly IScopeContext _scopeContext;

        public UserProfileDesignCategoryService(
            IUserProfileDesignCategoryRepository userProfileDesignCategoryRepository,
            IScopeContext scopeContext)
        {
            _userProfileDesignCategoryRepository = userProfileDesignCategoryRepository;
            _scopeContext = scopeContext;
        }

        public async Task<UserProfileDesignCategoryDto> CreateUserProfileDesignCategoryAsync(CreateUserProfileDesignCategoryDto dto)
        {
            var entity = dto.Adapt<UserProfileDesignCategory>();
            entity.CreatedBy = _scopeContext.UserId;
            entity.CreatedDate = DateTime.UtcNow;

            await _userProfileDesignCategoryRepository.AddAsync(entity);
            await _userProfileDesignCategoryRepository.SaveChangesAsync();

            return entity.Adapt<UserProfileDesignCategoryDto>();
        }

        public async Task<IEnumerable<UserProfileDesignCategoryDto>> GetAllUserProfileDesignCategoriesAsync()
        {
            var entities = await _userProfileDesignCategoryRepository.GetAllAsync();
            return entities.Adapt<IEnumerable<UserProfileDesignCategoryDto>>();
        }

        public async Task<UserProfileDesignCategoryDto> GetUserProfileDesignCategoryByIdAsync(int id)
        {
            var entity = await _userProfileDesignCategoryRepository.FindAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"UserProfileDesignCategory with ID {id} not found.");
            }

            return entity.Adapt<UserProfileDesignCategoryDto>();
        }

        public async Task UpdateUserProfileDesignCategoryAsync(UpdateUserProfileDesignCategoryDto dto)
        {
            var existingEntity = await _userProfileDesignCategoryRepository.FindAsync(e => e.Id == dto.Id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"UserProfileDesignCategory with ID {dto.Id} not found.");
            }

            dto.Adapt(existingEntity);
            existingEntity.UpdatedBy = _scopeContext.UserId;
            existingEntity.UpdatedDate = DateTime.UtcNow;

            _userProfileDesignCategoryRepository.Update(existingEntity);
            await _userProfileDesignCategoryRepository.SaveChangesAsync();
        }

        public async Task DeleteUserProfileDesignCategoryAsync(int id)
        {
            var entity = await _userProfileDesignCategoryRepository.FindAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"UserProfileDesignCategory with ID {id} not found.");
            }

            await _userProfileDesignCategoryRepository.DeleteAsync(entity);
            await _userProfileDesignCategoryRepository.SaveChangesAsync();
        }

        public async Task<PagedReadOnlyCollection<UserProfileDesignCategoryFilterDto>> GetUserProfileDesignCategoryListAsync(UserProfileDesignCategoryFilterDto filterDto, PageableParams pagingParams, SortParameter sortParameters)
        {
            var filter = CreateFilter(filterDto);

            return await _userProfileDesignCategoryRepository.GetPagedWithFilterAndProjectToAsync<UserProfileDesignCategoryFilterDto>(
                filter, pagingParams, sortParameters, nameof(DatasourceConstants.DesignCategory)
            );
        }

        private static Expression<Func<UserProfileDesignCategory, bool>> CreateFilter(UserProfileDesignCategoryFilterDto filterDto)
        {
            var predicate = PredicateBuilder.True<UserProfileDesignCategory>();

            if (!string.IsNullOrEmpty(filterDto.Title))
            {
                predicate = predicate.And(x => x.Description.Contains(filterDto.Title));
            }

            if (filterDto.DesignCategoryId.HasValue)
            {
                predicate = predicate.And(x => x.DesignCategoryId == filterDto.DesignCategoryId);
            }

            if (filterDto.UserId.HasValue)
            {
                predicate = predicate.And(x => x.UserId == filterDto.UserId);
            }

            return predicate;
        }

    }
}
