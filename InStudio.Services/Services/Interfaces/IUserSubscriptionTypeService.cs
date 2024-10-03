using InStudio.Services.Dtos.UserSubscriptionType;
using System.Collections.Generic;
using System.Threading.Tasks;
using InStudio.Common.Types;
using InStudio.Common;

namespace InStudio.Services.Services.Interfaces
{
    public interface IUserSubscriptionTypeService
    {
        Task<UserSubscriptionTypeDto> CreateSubscriptionTypeAsync(CreateUserSubscriptionTypeDto dto);

        Task<IEnumerable<UserSubscriptionTypeDto>> GetAllSubscriptionTypesAsync();

        Task<UserSubscriptionTypeDto> GetSubscriptionTypeByIdAsync(int subscriptionTypeId);

        Task UpdateSubscriptionTypeAsync(UpdateUserSubscriptionTypeDto dto);

        Task DeleteSubscriptionTypeAsync(int subscriptionTypeId);

        Task<PagedReadOnlyCollection<UserSubscriptionTypeDto>> GetSubscriptionTypeListAsync(
            FilterUserSubscriptionTypeDto filterDto, PageableParams pagingParams, SortParameter sortParameters);
    }
}
