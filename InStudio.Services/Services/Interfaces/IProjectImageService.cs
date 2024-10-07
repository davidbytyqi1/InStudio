using InStudio.Services.Dtos.ProjectImage;
using System.Threading.Tasks;

namespace InStudio.Services.Services.Interfaces
{
    public interface IProjectImageService
    {
        Task CreateProjectImageAsync(ProjectImageCreateDto projectImageCreateDto);
        Task<ProjectImageDto> GetProjectImageByIdAsync(int id);
        Task UpdateProjectImageAsync(int id, ProjectImageDto projectImageDto);
        Task DeleteProjectImageAsync(int id);
        Task<IEnumerable<ProjectImageDto>> GetAllProjectImagesAsync();
    }
}
