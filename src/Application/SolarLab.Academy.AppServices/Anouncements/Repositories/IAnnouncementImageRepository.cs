using SolarLab.Academy.AppServices.Base;
using SolarLab.Academy.Contracts.Announcements;
using SolarLab.Academy.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Anouncements.Repositories
{
    /// <summary>
    /// Репозитория для связи изображений и объявлений.
    /// </summary>
    public interface IAnnouncementImageRepository : IBaseRepository<Domain.Announcements.Files.AnnouncementImage>
    {
        /// <summary>
        /// Получить список идентификаторров изображений.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<AnnouncementImageDto>> GetByAnnouncementIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить записи о свзязи c изображениями.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task DeleteByAnnouncementId (Guid id, CancellationToken cancellationToken);

    }
}
