using AutoMapper;
using SolarLab.Academy.AppServices.Anouncements.Repositories;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Contracts.Announcements;
using SolarLab.Academy.Domain.Announcements.Entity;
using SolarLab.Academy.Domain.Announcements.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Anouncements.Services
{
    /// <inheritdoc />
    public class AnnouncementImageService : IAnnouncementImageService
    {
        private readonly IAnnouncementImageRepository _announcementImageRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// Инициализирует экземпляр <see cref="AnnouncementImageService"/>
        /// </summary>
        /// <param name="announcementImageRepository"></param>
        /// <param name="mapper"></param>
        public AnnouncementImageService(IAnnouncementImageRepository announcementImageRepository, IMapper mapper)
        {
            _announcementImageRepository = announcementImageRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task AddAsync(AnnouncementImageDto model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<AnnouncementImage>(model);
            await _announcementImageRepository.AddAsync(entity, cancellationToken);
        }
        /// <inheritdoc />
        public async Task DeleteByAnnouncementId(Guid id, CancellationToken cancellationToken)
        {
            await _announcementImageRepository.DeleteByAnnouncementId(id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AnnouncementImageDto>> GetByAnnouncementIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _announcementImageRepository.GetByAnnouncementIdAsync(id,cancellationToken);
        }
    }
}
