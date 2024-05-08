using SolarLab.Academy.Contracts.Announcements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Anouncements.Services
{
    /// <summary>
    /// Сервис работы со связью изображений и объявлений.
    /// </summary>
    public interface IAnnouncementImageService
    {
        /// <summary>
        /// Добавить связь.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync(AnnouncementImageDto model, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список связей.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<AnnouncementImageDto>> GetByAnnouncementIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить записи о свзязи c изображениями.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task DeleteByAnnouncementId(Guid id, CancellationToken cancellationToken);
    }
}
