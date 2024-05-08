using SolarLab.Academy.Contracts.Announcements;
using SolarLab.Academy.Contracts.Universal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Anouncements.Services
{
    public interface IAnnouncementService
    {

        /// <summary>
        /// Возвращает объявление по id.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Объявление.</returns>
        Task<AnnouncementDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создать объявление.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateAnnouncementRequest model, CancellationToken cancellationToken);

        /// <summary>
        /// Получить объявления с пагинацией.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultWithPagination<AnnouncementDto>> GetAnnouncementsAsync(GetAllRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Поиск объявления.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultWithPagination<AnnouncementDto>> SearchAsync(SearchAnnouncementRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Обновить информацию объявления.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AnnouncementDto> UpdateInfoAsync(UpdateInfoRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить объявление.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
