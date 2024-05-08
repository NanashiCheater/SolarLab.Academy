using SolarLab.Academy.AppServices.Base;
using SolarLab.Academy.AppServices.Specifications;
using SolarLab.Academy.Contracts.Universal;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Users.Entity;

namespace SolarLab.Academy.AppServices.Users.Repositories;

/// <summary>
/// Репозиторий для работы с пользователями.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Получить пользователей с пагинацией.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResultWithPagination<UserDto>> GetAll(GetAllRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить пользователей со спецификацией.
    /// </summary>
    /// <param name="specification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<UserDto>> GetAllBySpecification(Specification<User> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Получить пользователя по идентификатору.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить данные пользователя.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UpdateUserRequest> UpdateAsync(UpdateUserRequest model, CancellationToken cancellationToken);

    /// <summary>
    /// Добавить пользователя.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> AddAsync(CreateUserRequest request, CancellationToken cancellationToken);
}