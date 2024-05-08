using SolarLab.Academy.Contracts.Comments;
using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Comments.Services
{
    public interface ICommentService
    {
        /// <summary>
        /// Возвращает все комментарии.
        /// </summary>
        /// <returns>Список комментариев <see cref="CommentDto"/>.</returns>
        Task<ResultWithPagination<CommentDto>> GetCommentsAsync(GetAllCommentsRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Создать комментарий.
        /// </summary>
        /// <param name="model">.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Идентификатор пользователя.</returns>
        Task<Guid> AddAsync(CreateCommentRequest model, CancellationToken cancellationToken);

        /// <summary>
        /// Обновить данные коммментария.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Обновлённые данные.</returns>
        Task<CommentDto> UpdateAsync(UpdateCommentRequest model, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить комментарий.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить комментарии объявления.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task DeleteAllByAnnouncementIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
