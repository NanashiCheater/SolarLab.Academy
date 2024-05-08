using System.Linq.Expressions;

namespace SolarLab.Academy.AppServices.Base;
/// <summary>
/// Базовый репозиторий.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseRepository<TEntity> where TEntity: class
{
    /// <summary>
    /// Возвращает все элементы сущности <see cref="TEntity"/>
    /// </summary>
    /// <returns>Все элементы сущности <see cref="TEntity"/></returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Возвращает все элементы сущности <see cref="TEntity"/> по предикату
    /// </summary>
    /// <param name="predicate">Предикат</param>
    /// <returns>все элементы сущности <see cref="TEntity"/> по предикату</returns>
    IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Возвращает элемент сущности по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns><see cref="TEntity"/></returns>
    ValueTask<TEntity?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить сущность.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(TEntity model, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить данные сущности.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> UpdateAsync(TEntity model, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить сущность.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}