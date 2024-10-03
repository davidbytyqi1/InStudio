using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using InStudio.Services.Repositories.Interfaces;
using InStudio.Services.Services.Interfaces;
using InStudio.Services.Dtos.UserSubscriptionType;
using Mapster;
using InStudio.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InStudio.Services.Services
{
    public class UserSubscriptionTypeService : IUserSubscriptionTypeService
    {
        private readonly IUserSubscriptionTypeRepository _userSubscriptionTypeRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSubscriptionTypeService(
            IUserSubscriptionTypeRepository userSubscriptionTypeRepository,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userSubscriptionTypeRepository = userSubscriptionTypeRepository;
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

        public async Task<UserSubscriptionTypeDto> CreateSubscriptionTypeAsync(UserSubscriptionTypeDto dto)
        {
            var subscriptionTypeEntity = dto.Adapt<UserSubscriptionType>();
            subscriptionTypeEntity.CreatedBy = await GetCurrentUserIdAsync();
            subscriptionTypeEntity.CreatedDate = DateTime.UtcNow;

            await _userSubscriptionTypeRepository.AddAsync(subscriptionTypeEntity);
            await _userSubscriptionTypeRepository.SaveChangesAsync();

            return subscriptionTypeEntity.Adapt<UserSubscriptionTypeDto>();
        }

        public async Task<IEnumerable<UserSubscriptionTypeDto>> GetAllSubscriptionTypesAsync()
        {
            var subscriptionTypes = await _userSubscriptionTypeRepository.GetAllAsync();
            return subscriptionTypes.Adapt<IEnumerable<UserSubscriptionTypeDto>>();
        }

        public async Task<UserSubscriptionTypeDto> GetSubscriptionTypeByIdAsync(int subscriptionTypeId)
        {
            var subscriptionType = await _userSubscriptionTypeRepository.FindAsync(c => c.Id == subscriptionTypeId);
            if (subscriptionType == null)
            {
                throw new KeyNotFoundException($"Subscription Type with ID {subscriptionTypeId} not found.");
            }

            return subscriptionType.Adapt<UserSubscriptionTypeDto>();
        }

        public async Task UpdateSubscriptionTypeAsync(UserSubscriptionTypeDto dto)
        {
            var existingSubscriptionType = await _userSubscriptionTypeRepository.FindAsync(c => c.Id == dto.Id);
            if (existingSubscriptionType == null)
            {
                throw new KeyNotFoundException($"Subscription Type with ID {dto.Id} not found.");
            }

            dto.Adapt(existingSubscriptionType);
            existingSubscriptionType.UpdatedBy = await GetCurrentUserIdAsync();
            existingSubscriptionType.UpdatedDate = DateTime.UtcNow;

            _userSubscriptionTypeRepository.Update(existingSubscriptionType);
            await _userSubscriptionTypeRepository.SaveChangesAsync();
        }

        public async Task DeleteSubscriptionTypeAsync(int subscriptionTypeId)
        {
            var subscriptionType = await _userSubscriptionTypeRepository.FindAsync(c => c.Id == subscriptionTypeId);
            if (subscriptionType == null)
            {
                throw new KeyNotFoundException($"Subscription Type with ID {subscriptionTypeId} not found.");
            }

            await _userSubscriptionTypeRepository.DeleteAsync(subscriptionType);
            await _userSubscriptionTypeRepository.SaveChangesAsync();
        }
    }
}
