using InStudio.Services.Repositories.Interfaces;
using InStudio.Services.Dtos.UserProfile;
using Mapster;
using InStudio.Data.Models;
using InStudio.Common.Services.Interfaces;
using InStudio.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InStudio.Services.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IScopeContext _scopeContext;

        public UserProfileService(
            IUserProfileRepository userProfileRepository,
            IScopeContext scopeContext)
        {
            _userProfileRepository = userProfileRepository;
            _scopeContext = scopeContext;
        }

        public async Task<UserProfileDto> CreateUserProfileAsync(CreateUserProfileDto dto)
        {
            var userProfileEntity = dto.Adapt<UserProfile>();
            userProfileEntity.CreatedBy = _scopeContext.UserId;
            userProfileEntity.CreatedDate = DateTime.UtcNow;

            await _userProfileRepository.AddAsync(userProfileEntity);
            await _userProfileRepository.SaveChangesAsync();

            return userProfileEntity.Adapt<UserProfileDto>();
        }

        public async Task<IEnumerable<UserProfileDto>> GetAllUserProfilesAsync()
        {
            var userProfiles = await _userProfileRepository.GetAllAsync();
            return userProfiles.Adapt<IEnumerable<UserProfileDto>>();
        }

        public async Task<UserProfileDto> GetUserProfileByIdAsync(int profileId)
        {
            var userProfile = await _userProfileRepository.FindAsync(p => p.Id == profileId);
            if (userProfile == null)
            {
                throw new KeyNotFoundException($"UserProfile with ID {profileId} not found.");
            }

            return userProfile.Adapt<UserProfileDto>();
        }

        public async Task UpdateUserProfileAsync(UpdateUserProfileDto dto)
        {
            var existingProfile = await _userProfileRepository.FindAsync(p => p.Id == dto.Id);
            if (existingProfile == null)
            {
                throw new KeyNotFoundException($"UserProfile with ID {dto.Id} not found.");
            }

            dto.Adapt(existingProfile);
            existingProfile.UpdatedBy = _scopeContext.UserId;
            existingProfile.UpdatedDate = DateTime.UtcNow;

            _userProfileRepository.Update(existingProfile);
            await _userProfileRepository.SaveChangesAsync();
        }

        public async Task DeleteUserProfileAsync(int profileId)
        {
            var userProfile = await _userProfileRepository.FindAsync(p => p.Id == profileId);
            if (userProfile == null)
            {
                throw new KeyNotFoundException($"UserProfile with ID {profileId} not found.");
            }

            await _userProfileRepository.DeleteAsync(userProfile);
            await _userProfileRepository.SaveChangesAsync();
        }
    }
}
