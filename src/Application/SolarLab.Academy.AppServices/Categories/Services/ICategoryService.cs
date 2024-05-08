using SolarLab.Academy.Contracts.Categories;
using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.AppServices.Categories.Services
{
    /// <summary>
    /// Сервис для работы с категориями.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Получить список всех категорий.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список категорий</returns>
        Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Получить информацию о категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Информация о категории.</returns>
        Task<CategoryDto> Get(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создать категорию.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateCategoryRequest model, CancellationToken cancellationToken);
        /// <summary>
        /// Обновить данные категории.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CategoryDto> UpdateAsync(CategoryDto model, CancellationToken cancellationToken);
        /// <summary>
        /// Удалить категорию.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}