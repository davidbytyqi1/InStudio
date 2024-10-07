using InStudio.Common.Types;
using InStudio.Common.Services.Interfaces;
using InStudio.Data.Models;
using InStudio.Services.Dtos.ProjectOffer;
using InStudio.Services.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InStudio.Common;
using InStudio.Services.Services.Interfaces;

namespace InStudio.Services.Services
{
    public class ProjectOfferService : IProjectOfferService
    {
        private readonly IProjectOfferRepository _projectOfferRepository;
        private readonly IScopeContext _scopeContext;

        public ProjectOfferService(
            IProjectOfferRepository projectOfferRepository,
            IScopeContext scopeContext)
        {
            _projectOfferRepository = projectOfferRepository;
            _scopeContext = scopeContext;
        }

        public async Task<ProjectOfferDto> CreateProjectOfferAsync(CreateProjectOfferDto dto)
        {
            var offerEntity = dto.Adapt<ProjectOffer>();
            offerEntity.CreatedBy = _scopeContext.UserId;
            offerEntity.CreatedDate = DateTime.UtcNow;

            await _projectOfferRepository.AddAsync(offerEntity);
            await _projectOfferRepository.SaveChangesAsync();

            return offerEntity.Adapt<ProjectOfferDto>();
        }

        public async Task<IEnumerable<ProjectOfferDto>> GetAllProjectOffersAsync()
        {
            var offers = await _projectOfferRepository.GetAllAsync();
            return offers.Adapt<IEnumerable<ProjectOfferDto>>();
        }

        public async Task<ProjectOfferDto> GetProjectOfferByIdAsync(int offerId)
        {
            var offer = await _projectOfferRepository.FindAsync(o => o.Id == offerId);
            if (offer == null)
            {
                throw new KeyNotFoundException($"Project Offer with ID {offerId} not found.");
            }

            return offer.Adapt<ProjectOfferDto>();
        }

        public async Task UpdateProjectOfferAsync(UpdateProjectOfferDto dto)
        {
            var existingOffer = await _projectOfferRepository.FindAsync(o => o.Id == dto.Id);
            if (existingOffer == null)
            {
                throw new KeyNotFoundException($"Project Offer with ID {dto.Id} not found.");
            }

            dto.Adapt(existingOffer);
            existingOffer.UpdatedBy = _scopeContext.UserId;
            existingOffer.UpdatedDate = DateTime.UtcNow;

            _projectOfferRepository.Update(existingOffer);
            await _projectOfferRepository.SaveChangesAsync();
        }

        public async Task DeleteProjectOfferAsync(int offerId)
        {
            var offer = await _projectOfferRepository.FindAsync(o => o.Id == offerId);
            if (offer == null)
            {
                throw new KeyNotFoundException($"Project Offer with ID {offerId} not found.");
            }

            await _projectOfferRepository.DeleteAsync(offer);
        }

        public async Task<PagedReadOnlyCollection<ProjectOfferFilterDto>> GetProjectOfferListAsync(ProjectOfferFilterDto filterDto, PageableParams pagingParams, SortParameter sortParameters)
        {
            var filter = CreateFilter(filterDto);

            return await _projectOfferRepository.GetPagedWithFilterAndProjectToAsync<ProjectOfferFilterDto>(
                filter, pagingParams, sortParameters, nameof(DatasourceConstants.Project)
            );
        }

        private static Expression<Func<ProjectOffer, bool>> CreateFilter(ProjectOfferFilterDto filterDto)
        {
            var predicate = PredicateBuilder.True<ProjectOffer>();

            if (!string.IsNullOrEmpty(filterDto.CoverLetter))
            {
                predicate = predicate.And(x => x.CoverLetter.Contains(filterDto.CoverLetter));
            }

            if (filterDto.IsAccepted.HasValue)
            {
                predicate = predicate.And(x => x.IsAccepted == filterDto.IsAccepted.Value);
            }

            return predicate;
        }
    }
}
