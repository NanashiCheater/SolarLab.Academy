using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Anouncements.Repositories;
using SolarLab.Academy.AppServices.Specifications;
using SolarLab.Academy.Contracts.Announcements;
using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.DataAccess.Base;
using SolarLab.Academy.Domain.Announcements.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.DataAccess.Announcements.Repository
{
    /// <inheritdoc />
    public class AnnouncementRepository: BaseRepository<Domain.Announcements.Entity.Announcement>, IAnnouncementRepository
    {
        private readonly IMapper _mapper;

        public AnnouncementRepository(IMapper mapper, DbContext dbContext)
            : base(dbContext)
        {
            _mapper = mapper;
        }
        /// <inheritdoc />
        public Task<AnnouncementDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return GetAll().Where(s => s.Id == id)
            .ProjectTo<AnnouncementDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        }
        /// <inheritdoc />
        public async Task<ResultWithPagination<AnnouncementDto>> GetAll(GetAllRequest request, CancellationToken cancellationToken)
        {
            var result = new ResultWithPagination<AnnouncementDto>();

            var query = GetAll();

            var elementsCount = await query.CountAsync(cancellationToken);
            result.AvailablePages = elementsCount / request.Batchsize;

            var paginationQuery = await query
                .OrderBy(ann => ann.Id)
                .Skip(request.Batchsize * (request.PageNumber - 1))
                .Take(request.Batchsize)
                .ProjectTo<AnnouncementDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);

            result.Result = paginationQuery;

            return result;
        }
        /// <inheritdoc />
        public async Task<ResultWithPagination<AnnouncementDto>> SearchAsync(SearchAnnouncementRequest request, Specification<Announcement> specification, CancellationToken cancellationToken)
        {
            var result = new ResultWithPagination<AnnouncementDto>();
           

            var paginationQuery = await GetAll()
                .OrderBy(ann => ann.Id)
                .Where(specification.ToExpression())
                .Skip(request.Batchsize.Value * (request.PageNumber.Value - 1))
                .Take(request.Batchsize.Value)
                .ProjectTo<AnnouncementDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);

            var elementsCount = paginationQuery.Length;
            result.AvailablePages = elementsCount / request.Batchsize.Value;

            result.Result = paginationQuery;

            return result;
                
        }
    }
}
