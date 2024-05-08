using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Comments.Repositories;
using SolarLab.Academy.Contracts.Comments;
using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.DataAccess.Base;
using SolarLab.Academy.Domain.Comments.Entity;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.DataAccess.Comments.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        private readonly IMapper _mapper;
        public CommentRepository(DbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        /// <inheritdoc />
        public async Task<Guid> AddAsync(CreateCommentRequest request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Comment>(request);

           await AddAsync(entity, cancellationToken);

            return entity.Id;

        }
        /// <inheritdoc />
        public async Task DeleteAllByAnnouncementIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var comments = GetAll().Where(s => s.AnnouncementId == id);
            DbSet.RemoveRange(comments);
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<ResultWithPagination<CommentDto>> GetAll(GetAllCommentsRequest request, CancellationToken cancellationToken)
        {
            var result = new ResultWithPagination<CommentDto>();

            var query = GetAll();

            var elementsCount = await query.CountAsync(cancellationToken);
            result.AvailablePages = elementsCount / request.Batchsize;

            var paginationQuery = await query
                .Where(s => s.AnnouncementId == request.AnnouncementId)
                .OrderBy(comm => comm.Id)
                .Skip(request.Batchsize * (request.PageNumber - 1))
                .Take(request.Batchsize)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);

            result.Result = paginationQuery;

            return result;
        }
    }
}
