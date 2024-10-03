using InStudio.Services.Dtos.UserSubscriptionType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InStudio.Services.Services.Interfaces
{
    public interface IUserSubscriptionTypeService
    {
        Task<UserSubscriptionTypeDto> CreateSubscriptionTypeAsync(UserSubscriptionTypeDto dto);
        Task<IEnumerable<UserSubscriptionTypeDto>> GetAllSubscriptionTypesAsync();
        Task<UserSubscriptionTypeDto> GetSubscriptionTypeByIdAsync(int subscriptionTypeId);
        Task UpdateSubscriptionTypeAsync(UserSubscriptionTypeDto dto);
        Task DeleteSubscriptionTypeAsync(int subscriptionTypeId);
    }
}
