using InStudio.Services.Dtos.UserEducation;
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
    public class UserEducationService : IUserEducationService
    {
        private readonly IUserEducationRepository _userEducationRepository;
        private readonly IScopeContext _scopeContext;

        public UserEducationService(
            IUserEducationRepository userEducationRepository,
            IScopeContext scopeContext)
        {
            _userEducationRepository = userEducationRepository;
            _scopeContext = scopeContext;
        }

        public async Task<UserEducationDto> CreateEducationAsync(CreateUserEducationDto dto)
        {
            var educationEntity = dto.Adapt<UserEducation>();
            educationEntity.CreatedBy = _scopeContext.UserId;
            educationEntity.CreatedDate = DateTime.UtcNow;

            await _userEducationRepository.AddAsync(educationEntity);
            await _userEducationRepository.SaveChangesAsync();

            return educationEntity.Adapt<UserEducationDto>();
        }

        public async Task<IEnumerable<UserEducationDto>> GetAllEducationsAsync()
        {
            var educations = await _userEducationRepository.GetAllAsync();
            return educations.Adapt<IEnumerable<UserEducationDto>>();
        }

        public async Task<UserEducationDto> GetEducationByIdAsync(int educationId)
        {
            var education = await _userEducationRepository.FindAsync(e => e.Id == educationId);
            if (education == null)
            {
                throw new KeyNotFoundException($"Education with ID {educationId} not found.");
            }

            return education.Adapt<UserEducationDto>();
        }

        public async Task UpdateEducationAsync(UpdateUserEducationDto dto)
        {
            var existingEducation = await _userEducationRepository.FindAsync(e => e.Id == dto.Id);
            if (existingEducation == null)
            {
                throw new KeyNotFoundException($"Education with ID {dto.Id} not found.");
            }

            dto.Adapt(existingEducation);
            existingEducation.UpdatedBy = _scopeContext.UserId;
            existingEducation.UpdatedDate = DateTime.UtcNow;

            _userEducationRepository.Update(existingEducation);
            await _userEducationRepository.SaveChangesAsync();
        }

        public async Task DeleteEducationAsync(int educationId)
        {
            var education = await _userEducationRepository.FindAsync(e => e.Id == educationId);
            if (education == null)
            {
                throw new KeyNotFoundException($"Education with ID {educationId} not found.");
            }

            await _userEducationRepository.DeleteAsync(education);
        }
    }
}
