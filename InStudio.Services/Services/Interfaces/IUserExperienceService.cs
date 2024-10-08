using InStudio.Services.Dtos.UserExperience;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InStudio.Services.Services.Interfaces
{
    public interface IUserExperienceService
    {
        Task<UserExperienceDto> CreateExperienceAsync(CreateUserExperienceDto dto);
        Task<IEnumerable<UserExperienceDto>> GetAllExperiencesAsync();
        Task<UserExperienceDto> GetExperienceByIdAsync(int experienceId);
        Task UpdateExperienceAsync(UpdateUserExperienceDto dto);
        Task DeleteExperienceAsync(int experienceId);
    }
}
