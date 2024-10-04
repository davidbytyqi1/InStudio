using InStudio.Common.Types;
using InStudio.Common;
using InStudio.Data.Models;
using InStudio.Services.Dtos.Project;
using InStudio.Services.Dtos.UserSubscriptionType;
using InStudio.Services.Repositories;
using InStudio.Services.Repositories.Interfaces;
using InStudio.Services.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InStudio.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectService(
            IProjectRepository projectRepository,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _projectRepository = projectRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<Guid> GetCurrentUserIdAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            var identityUser = await _userManager.GetUserAsync(user);
            return identityUser != null ? Guid.Parse(identityUser.Id) : throw new UnauthorizedAccessException("User is not logged in.");
        }

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto)
        {
            var projectEntity = dto.Adapt<Project>();
            projectEntity.CreatedBy = await GetCurrentUserIdAsync();
            projectEntity.CreatedDate = DateTime.UtcNow;

            await _projectRepository.AddAsync(projectEntity);
            await _projectRepository.SaveChangesAsync();

            return projectEntity.Adapt<ProjectDto>();
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Adapt<IEnumerable<ProjectDto>>();
        }

        public async Task<ProjectDto> GetProjectByIdAsync(int projectId)
        {
            var project = await _projectRepository.FindAsync(p => p.Id == projectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {projectId} not found.");
            }

            return project.Adapt<ProjectDto>();
        }

        public async Task UpdateProjectAsync(UpdateProjectDto dto)
        {
            var existingProject = await _projectRepository.FindAsync(p => p.Id == dto.Id);
            if (existingProject == null)
            {
                throw new KeyNotFoundException($"Project with ID {dto.Id} not found.");
            }

            dto.Adapt(existingProject);
            existingProject.UpdatedBy = await GetCurrentUserIdAsync();
            existingProject.UpdatedDate = DateTime.UtcNow;

            _projectRepository.Update(existingProject);
            await _projectRepository.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await _projectRepository.FindAsync(p => p.Id == projectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {projectId} not found.");
            }

            await _projectRepository.DeleteAsync(project);
        }

        public async Task<PagedReadOnlyCollection<ProjectFilterDto>> GetProjectListAsync(ProjectFilterDto filterDto, PageableParams pagingParams, SortParameter sortParameters)
        {
            var filter = CreateFilter(filterDto);

            return await _projectRepository.GetPagedWithFilterAndProjectToAsync<ProjectFilterDto>(
                filter, pagingParams, sortParameters, nameof(DatasourceConstants.DesignCategory)
            );
        }

        private static Expression<Func<Project, bool>> CreateFilter(ProjectFilterDto filterDto)
        {
            var predicate = PredicateBuilder.True<Project>();

            if (!string.IsNullOrEmpty(filterDto.Title))
            {
                predicate = predicate.And(x => x.Title.Contains(filterDto.Title));
            }

            return predicate;
        }


    }
}
