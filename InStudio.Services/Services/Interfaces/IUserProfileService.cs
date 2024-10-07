using InStudio.Services.Dtos.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> CreateUserProfileAsync(CreateUserProfileDto dto);
        Task<IEnumerable<UserProfileDto>> GetAllUserProfilesAsync();
        Task<UserProfileDto> GetUserProfileByIdAsync(int profileId);
        Task UpdateUserProfileAsync(UpdateUserProfileDto dto);
        Task DeleteUserProfileAsync(int profileId);
    }
}
