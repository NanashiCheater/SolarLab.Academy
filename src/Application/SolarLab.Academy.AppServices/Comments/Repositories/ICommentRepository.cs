using SolarLab.Academy.AppServices.Base;
using SolarLab.Academy.Contracts.Comments;
using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.Domain.Comments.Entity;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Comments.Repositories
{
    /// <summary>
    /// Репозиторий для работы с комментариями.
    /// </summary>
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        /// <summary>
        /// Добавить комментарий.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateCommentRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Получить комментарии с пагинацией.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultWithPagination<CommentDto>> GetAll(GetAllCommentsRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить все комментарии объявления.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAllByAnnouncementIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
