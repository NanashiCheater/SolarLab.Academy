using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Base;
using SolarLab.Academy.Domain.Users.Entity;

namespace SolarLab.Academy.DataAccess.Base;

/// <inheritdoc />
public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity: class
{
    protected DbContext DbContext { get; }
    
    protected DbSet<TEntity> DbSet { get; }

    protected BaseRepository(DbContext context)
    {
        DbContext = context;
        DbSet = DbContext.Set<TEntity>();
    }
    /// <inheritdoc />
    public IQueryable<TEntity> GetAll()
    {
        return DbSet.AsNoTracking();
    }
    /// <inheritdoc />
    public IQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return DbSet.Where(predicate).AsNoTracking();
    }
    /// <inheritdoc />
    public ValueTask<TEntity?> GetByIdAsync(Guid id)
    {
        return DbSet.FindAsync(id);
    }
    /// <inheritdoc />
    public async Task AddAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model == null)
        { 
            throw new ArgumentNullException(nameof(model));
        }

        await DbSet.AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
    /// <inheritdoc />
    public async Task<TEntity> UpdateAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var entity = DbSet.Update(model).Entity;
        await DbContext.SaveChangesAsync(cancellationToken);
        return entity;
         
    }
    /// <inheritdoc />
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        DbSet.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}