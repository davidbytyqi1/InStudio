using InStudio.Services.Dtos.UserEducation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Services.Interfaces
{

    public interface IUserEducationService
    {
        Task<UserEducationDto> CreateEducationAsync(CreateUserEducationDto dto);
        Task<IEnumerable<UserEducationDto>> GetAllEducationsAsync();
        Task<UserEducationDto> GetEducationByIdAsync(int educationId);
        Task UpdateEducationAsync(UpdateUserEducationDto dto);
        Task DeleteEducationAsync(int educationId);
    }
}
