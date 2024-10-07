using InStudio.Common;
using InStudio.Common.Types;
using InStudio.Services.Dtos.ProjectOffer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InStudio.Services.Services.Interfaces
{
    public interface IProjectOfferService
    {
        Task<ProjectOfferDto> CreateProjectOfferAsync(CreateProjectOfferDto dto);
        Task<IEnumerable<ProjectOfferDto>> GetAllProjectOffersAsync();
        Task<ProjectOfferDto> GetProjectOfferByIdAsync(int offerId);
        Task UpdateProjectOfferAsync(UpdateProjectOfferDto dto);
        Task DeleteProjectOfferAsync(int offerId);
        Task<PagedReadOnlyCollection<ProjectOfferFilterDto>> GetProjectOfferListAsync(ProjectOfferFilterDto filterDto, PageableParams pagingParams, SortParameter sortParameters);
    }
}
