using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Anouncements.Repositories;
using SolarLab.Academy.Contracts.Announcements;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.DataAccess.Announcements.Repository
{
    /// <inheritdoc />
    public class AnnouncementImageRepository: BaseRepository<Domain.Announcements.Files.AnnouncementImage>, IAnnouncementImageRepository
    {
        private readonly IMapper _mapper;

        public AnnouncementImageRepository(IMapper mapper, DbContext dbContext)
            : base(dbContext)
        {
            _mapper = mapper;
        }
        /// <inheritdoc />
        public async Task DeleteByAnnouncementId(Guid id, CancellationToken cancellationToken)
        {
            var images = GetAll().Where(s => s.AnnouncementId == id);
            DbSet.RemoveRange(images);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc />
        public async Task<IEnumerable<AnnouncementImageDto>> GetByAnnouncementIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetAll().Where(s => s.AnnouncementId == id)
            .ProjectTo<AnnouncementImageDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
        }
    }
}
