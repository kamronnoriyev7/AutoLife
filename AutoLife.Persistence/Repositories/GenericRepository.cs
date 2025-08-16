using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace AutoLife.Persistence.Repositories;

public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity, TContext>
    where TEntity : BaseEntity
    where TContext : DbContext
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();

    protected readonly TContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, string includeProperties = "", bool includeDeleted = false, bool asNoTracking = false)
        => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        => await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

    public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await _dbSet.AddRangeAsync(entities);

    public void Update(TEntity entity) => _dbSet.Update(entity);

    public void Remove(TEntity entity) => _dbSet.Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

    public async Task<IEnumerable<TEntity>> GetPagedListAsync(
         int pageNumber, int pageSize,
         Expression<Func<TEntity, bool>>? predicate = null,
         string includeProperties = "",
         bool asNoTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (predicate is not null)
            query = query.Where(predicate);

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }
        }

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        if (predicate == null)
            return await _dbSet.CountAsync();
        return await _dbSet.CountAsync(predicate);
    }

    public async Task<bool> IsUniqueAsync(TEntity entity, params Expression<Func<TEntity, object>>[] uniqueProperties)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var propExpr in uniqueProperties)
        {
            var compiled = propExpr.Compile();
            var value = compiled(entity);

            query = query.Where(e => EF.Property<object>(e, GetPropertyName(propExpr))!.Equals(value));
        }

        if (entity.BasaEntityId != Guid.Empty)
        {
            query = query.Where(e => e.BasaEntityId != entity.BasaEntityId);
        }

        return !await query.AnyAsync();
    }

    private string GetPropertyName(Expression<Func<TEntity, object>> expression)
    {
        if (expression.Body is MemberExpression member)
            return member.Member.Name;

        if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression memberOperand)
            return memberOperand.Member.Name;

        throw new InvalidOperationException("Invalid property expression");
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(e => e.BasaEntityId == id);
        if (entity is null) throw new Exception("Entity not found");

        entity.IsDeleted = true;
        entity.DeleteDate = DateTime.UtcNow;
        _dbSet.Update(entity);
    }

    public async Task RestoreDeletedAsync(Guid id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(e => e.BasaEntityId == id && e.IsDeleted);
        if (entity is null) throw new Exception("Entity not found or not deleted");

        entity.IsDeleted = false;
        entity.DeleteDate = null;
        _dbSet.Update(entity);
    }

    public async Task<List<T>> FromSqlRawAsync<T>(string sql, params object[] parameters) where T : class
    {
        return await _context.Set<T>().FromSqlRaw(sql, parameters).ToListAsync();
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return _dbSet.AsQueryable();
    }
}
