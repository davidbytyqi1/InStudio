using InStudio.Common;
using InStudio.Common.Types;
using InStudio.Services.Dtos.UserProfileDesignCategory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InStudio.Services.Services.Interfaces
{
    public interface IUserProfileDesignCategoryService
    {
        Task<UserProfileDesignCategoryDto> CreateUserProfileDesignCategoryAsync(CreateUserProfileDesignCategoryDto dto);
        Task<IEnumerable<UserProfileDesignCategoryDto>> GetAllUserProfileDesignCategoriesAsync();
        Task<UserProfileDesignCategoryDto> GetUserProfileDesignCategoryByIdAsync(int id);
        Task UpdateUserProfileDesignCategoryAsync(UpdateUserProfileDesignCategoryDto dto);
        Task DeleteUserProfileDesignCategoryAsync(int id);
        Task<PagedReadOnlyCollection<UserProfileDesignCategoryFilterDto>> GetUserProfileDesignCategoryListAsync(UserProfileDesignCategoryFilterDto filterDto, PageableParams pagingParams, SortParameter sortParameters);
    }
}
