using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.AppServices.Users.Services;

/// <summary>
/// Сервис работы с пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Возвращает всех пользователей.
    /// </summary>
    /// <returns>Список пользователей <see cref="UserDto"/>.</returns>
    Task<ResultWithPagination<UserDto>> GetUsersAsync(GetAllRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает пользователей по имени.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекцию моделей пользователей.</returns>
    Task<IEnumerable<UserDto>> GetUsersByNameAsync(UsersByNameRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает пользователя по id.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Пользователь.</returns>
    ValueTask<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Создать пользователя.
    /// </summary>
    /// <param name="model">Данные пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор пользователя.</returns>
    Task<Guid> AddAsync(CreateUserRequest model, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить данные пользователя.
    /// </summary>
    /// <param name="model">Новые данные.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Обновлённые данные.</returns>
    Task<UpdateUserRequest> UpdateAsync(UpdateUserRequest model, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить польззователя.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}