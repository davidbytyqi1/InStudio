using InStudio.Services.Dtos.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Services.Interfaces
{

    public interface IProjectService
    {
        Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto);
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto> GetProjectByIdAsync(int projectId);
        Task UpdateProjectAsync(UpdateProjectDto dto);
        Task DeleteProjectAsync(int projectId);
    }
}
