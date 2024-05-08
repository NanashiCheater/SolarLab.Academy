using AutoMapper;
using SolarLab.Academy.AppServices.Comments.Repositories;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Contracts.Comments;
using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Comments.Entity;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Comments.Services
{
    /// <inheritdoc />
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        /// <inheritdoc />
        public async Task<Guid> AddAsync(CreateCommentRequest model, CancellationToken cancellationToken)
        {
            var result = await _commentRepository.AddAsync(model, cancellationToken);
            return result;
        }
        /// <inheritdoc />
        public async Task DeleteAllByAnnouncementIdAsync(Guid id, CancellationToken cancellationToken)
        {
            await _commentRepository.DeleteAllByAnnouncementIdAsync(id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _commentRepository.DeleteAsync(id, cancellationToken);
        }
        /// <inheritdoc />
        public async Task<ResultWithPagination<CommentDto>> GetCommentsAsync(GetAllCommentsRequest request, CancellationToken cancellationToken)
        {
            return await _commentRepository.GetAll(request, cancellationToken);   
        }
        /// <inheritdoc />
        public async Task<CommentDto> UpdateAsync(UpdateCommentRequest model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Comment>(model);
            var result = await _commentRepository.UpdateAsync(entity, cancellationToken);
            return _mapper.Map<CommentDto>(result);
        }
    }
}
