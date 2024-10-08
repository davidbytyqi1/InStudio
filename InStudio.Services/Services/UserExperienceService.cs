using InStudio.Services.Dtos.UserExperience;
using InStudio.Services.Repositories.Interfaces;
using Mapster;
using InStudio.Data.Models;
using InStudio.Common.Services.Interfaces;
using InStudio.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InStudio.Services.Services
{
    public class UserExperienceService : IUserExperienceService
    {
        private readonly IUserExperienceRepository _userExperienceRepository;
        private readonly IScopeContext _scopeContext;

        public UserExperienceService(
            IUserExperienceRepository userExperienceRepository,
            IScopeContext scopeContext)
        {
            _userExperienceRepository = userExperienceRepository;
            _scopeContext = scopeContext;
        }

        public async Task<UserExperienceDto> CreateExperienceAsync(CreateUserExperienceDto dto)
        {
            var experienceEntity = dto.Adapt<UserExperience>();
            experienceEntity.CreatedBy = _scopeContext.UserId;
            experienceEntity.CreatedDate = DateTime.UtcNow;

            await _userExperienceRepository.AddAsync(experienceEntity);
            await _userExperienceRepository.SaveChangesAsync();

            return experienceEntity.Adapt<UserExperienceDto>();
        }

        public async Task<IEnumerable<UserExperienceDto>> GetAllExperiencesAsync()
        {
            var experiences = await _userExperienceRepository.GetAllAsync();
            return experiences.Adapt<IEnumerable<UserExperienceDto>>();
        }

        public async Task<UserExperienceDto> GetExperienceByIdAsync(int experienceId)
        {
            var experience = await _userExperienceRepository.FindAsync(e => e.Id == experienceId);
            if (experience == null)
            {
                throw new KeyNotFoundException($"Experience with ID {experienceId} not found.");
            }

            return experience.Adapt<UserExperienceDto>();
        }

        public async Task UpdateExperienceAsync(UpdateUserExperienceDto dto)
        {
            var existingExperience = await _userExperienceRepository.FindAsync(e => e.Id == dto.Id);
            if (existingExperience == null)
            {
                throw new KeyNotFoundException($"Experience with ID {dto.Id} not found.");
            }

            dto.Adapt(existingExperience);
            existingExperience.UpdatedBy = _scopeContext.UserId;
            existingExperience.UpdatedDate = DateTime.UtcNow;

            _userExperienceRepository.Update(existingExperience);
            await _userExperienceRepository.SaveChangesAsync();
        }

        public async Task DeleteExperienceAsync(int experienceId)
        {
            var experience = await _userExperienceRepository.FindAsync(e => e.Id == experienceId);
            if (experience == null)
            {
                throw new KeyNotFoundException($"Experience with ID {experienceId} not found.");
            }

            await _userExperienceRepository.DeleteAsync(experience);
        }
    }
}
