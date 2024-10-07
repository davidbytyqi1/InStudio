using InStudio.Common.Types;
using InStudio.Common.Services.Interfaces;
using InStudio.Data.Models;
using InStudio.Services.Dtos.UserSubscriptionType;
using InStudio.Services.Repositories;
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
    public class UserSubscriptionTypeService : IUserSubscriptionTypeService
    {
        private readonly IUserSubscriptionTypeRepository _userSubscriptionTypeRepository;
        private readonly IScopeContext _scopeContext;

        public UserSubscriptionTypeService(
            IUserSubscriptionTypeRepository userSubscriptionTypeRepository,
            IScopeContext scopeContext)
        {
            _userSubscriptionTypeRepository = userSubscriptionTypeRepository;
            _scopeContext = scopeContext;
        }

        public async Task<UserSubscriptionTypeDto> CreateSubscriptionTypeAsync(CreateUserSubscriptionTypeDto dto)
        {
            var subscriptionTypeEntity = dto.Adapt<UserSubscriptionType>();
            subscriptionTypeEntity.CreatedBy = _scopeContext.UserId;
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

        public async Task UpdateSubscriptionTypeAsync(UpdateUserSubscriptionTypeDto dto)
        {
            var existingSubscriptionType = await _userSubscriptionTypeRepository.FindAsync(c => c.Id == dto.Id);
            if (existingSubscriptionType == null)
            {
                throw new KeyNotFoundException($"Subscription Type with ID {dto.Id} not found.");
            }

            dto.Adapt(existingSubscriptionType);
            existingSubscriptionType.UpdatedBy = _scopeContext.UserId;
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

        public async Task<PagedReadOnlyCollection<UserSubscriptionTypeFilterDto>> GetSubscriptionTypeListAsync(
            UserSubscriptionTypeFilterDto filterDto,
            PageableParams pagingParams,
            SortParameter sortParameters)
        {
            var filter = CreateFilter(filterDto);

            return await _userSubscriptionTypeRepository.GetPagedWithFilterAndProjectToAsync<UserSubscriptionTypeFilterDto>(
                filter,
                pagingParams,
                sortParameters);
        }

        private static Expression<Func<UserSubscriptionType, bool>> CreateFilter(UserSubscriptionTypeFilterDto filterDto)
        {
            var predicate = PredicateBuilder.True<UserSubscriptionType>();

            if (!string.IsNullOrEmpty(filterDto.Title))
            {
                predicate = predicate.And(x => x.Title.Contains(filterDto.Title));
            }

            if (filterDto.MinPrice.HasValue)
            {
                predicate = predicate.And(x => x.Price >= filterDto.MinPrice);
            }

            if (filterDto.MaxPrice.HasValue)
            {
                predicate = predicate.And(x => x.Price <= filterDto.MaxPrice);
            }

            return predicate;
        }
    }
}