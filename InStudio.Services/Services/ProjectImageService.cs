using InStudio.Data.Models;
using InStudio.Services.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using InStudio.Services.Dtos.ProjectImage;
using InStudio.Services.Services.Interfaces;
using InStudio.Common.Services.Interfaces;

namespace InStudio.Services.Services
{
    public class ProjectImageService : IProjectImageService
    {
        private readonly IProjectImageRepository _projectImageRepository;
        private readonly IScopeContext  _scopeContext;
        public ProjectImageService(IProjectImageRepository projectImageRepository, IScopeContext scopeContext)
        {
            _projectImageRepository = projectImageRepository;
            _scopeContext = scopeContext;
        }

        public async Task CreateProjectImageAsync(ProjectImageCreateDto projectImageCreateDto)
        {
            if (projectImageCreateDto.ImageFile == null || projectImageCreateDto.ImageFile.Length == 0)
                throw new ArgumentException("Invalid image file");

            var projectImage = new ProjectImage
            {
                ProjectId = projectImageCreateDto.ProjectId,
                CreatedBy = _scopeContext.UserId,
                CreatedDate = DateTime.Now
            };

            await _projectImageRepository.AddAsync(projectImage);
            await _projectImageRepository.SaveChangesAsync();

            var imagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "ProjectImages", $"Project_{projectImageCreateDto.ProjectId}");
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            var fileExtension = Path.GetExtension(projectImageCreateDto.ImageFile.FileName);
            var fileName = $"ProjectImage_{projectImageCreateDto.ProjectId}_{projectImage.Id}{fileExtension}";
            var filePath = Path.Combine(imagesFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await projectImageCreateDto.ImageFile.CopyToAsync(stream);
            }

            projectImage.ImagePath = filePath;
            _projectImageRepository.Update(projectImage);
            await _projectImageRepository.SaveChangesAsync();
        }


        public async Task<ProjectImageDto> GetProjectImageByIdAsync(int id)
        {
            var projectImage = await _projectImageRepository.FindAsync(x => x.Id == id);
            if (projectImage == null)
                throw new KeyNotFoundException("Project image not found");

            return projectImage.Adapt<ProjectImageDto>();
        }

        public async Task UpdateProjectImageAsync(int id, ProjectImageDto projectImageDto)
        {
            var projectImage = await _projectImageRepository.FindAsync(x => x.Id == id);
            if (projectImage == null)
                throw new KeyNotFoundException("Project image not found");

            projectImage.ProjectId = projectImageDto.ProjectId;
            projectImage.UpdatedBy = _scopeContext.UserId;
            projectImage.UpdatedDate = DateTime.Now;

            if (projectImageDto.ImageFile != null && projectImageDto.ImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(projectImage.ImagePath) && File.Exists(projectImage.ImagePath))
                {
                    File.Delete(projectImage.ImagePath);
                }

                var imagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "ProjectImages", $"Project_{projectImageDto.ProjectId}");
                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                var fileExtension = Path.GetExtension(projectImageDto.ImageFile.FileName);
                var fileName = $"ProjectImage_{projectImageDto.ProjectId}_{projectImage.Id}{fileExtension}";
                var filePath = Path.Combine(imagesFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await projectImageDto.ImageFile.CopyToAsync(stream);
                }

                projectImage.ImagePath = filePath;
            }

            _projectImageRepository.Update(projectImage);
            await _projectImageRepository.SaveChangesAsync();
        }



        public async Task DeleteProjectImageAsync(int id)
        {
            var projectImage = await _projectImageRepository.FindAsync(x => x.Id == id);
            if (projectImage == null)
                throw new KeyNotFoundException("Project image not found");

            if (!string.IsNullOrEmpty(projectImage.ImagePath) && File.Exists(projectImage.ImagePath))
            {
                File.Delete(projectImage.ImagePath);
            }

            await _projectImageRepository.DeleteAsync(projectImage);
            await _projectImageRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectImageDto>> GetAllProjectImagesAsync()
        {
            var projectImages = await _projectImageRepository.GetAllAsync();

            return projectImages.Adapt<IEnumerable<ProjectImageDto>>();
        }
    }
}
