using SolarLab.Academy.AppServices.Base;
using SolarLab.Academy.AppServices.Specifications;
using SolarLab.Academy.Contracts.Announcements;
using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.Domain.Announcements.Entity;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Anouncements.Repositories
{
    /// <summary>
    /// Репозиторий для работы с Объявлениями.
    /// </summary>
    public interface IAnnouncementRepository : IBaseRepository<Announcement>
    {
        /// <summary>
        /// Получить объявление по идентификатору.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<AnnouncementDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);


        /// <summary>
        /// Получить объявления с пагинацией.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultWithPagination<AnnouncementDto>> GetAll(GetAllRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Поиск объявления.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="specification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultWithPagination<AnnouncementDto>> SearchAsync(SearchAnnouncementRequest request, Specification<Announcement> specification, CancellationToken cancellationToken);
    }
}
