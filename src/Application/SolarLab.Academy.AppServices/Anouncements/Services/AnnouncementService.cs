using AutoMapper;
using SolarLab.Academy.AppServices.Anouncements.Repositories;
using SolarLab.Academy.AppServices.Anouncements.Specifications;
using SolarLab.Academy.AppServices.Specifications;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Contracts.Announcements;
using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.Domain.Announcements.Entity;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Anouncements.Services
{

    /// <inheritdoc/>
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// Инициализирует экземпляр <see cref="AnnouncementService"/>
        /// </summary>
        /// <param name="announcementRepository"></param>
        /// <param name="mapper"></param>
        public AnnouncementService(IAnnouncementRepository announcementRepository, IMapper mapper)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }
        /// <inheritdoc/>
        public async Task<Guid> AddAsync(CreateAnnouncementRequest model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Announcement>(model);

            await _announcementRepository.AddAsync(entity, cancellationToken);
            return entity.Id;
        }
        /// <inheritdoc/>
        public Task<ResultWithPagination<AnnouncementDto>> GetAnnouncementsAsync(GetAllRequest request, CancellationToken cancellationToken)
        {
            return _announcementRepository.GetAll(request, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<AnnouncementDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _announcementRepository.GetByIdAsync(id, cancellationToken);
        }
        /// <inheritdoc/>
        public async Task<AnnouncementDto> UpdateInfoAsync(UpdateInfoRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Announcement>(request);
            var result = await _announcementRepository.UpdateAsync(entity, cancellationToken);
            return _mapper.Map<AnnouncementDto>(result);
        }
        /// <inheritdoc />
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _announcementRepository.DeleteAsync(id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<ResultWithPagination<AnnouncementDto>> SearchAsync(SearchAnnouncementRequest request, CancellationToken cancellationToken)
        {
            Specification<Announcement> specification = new ByWordSpecification(request.SearchWord);

            return await _announcementRepository.SearchAsync(request, specification, cancellationToken);
        }
    }
}

