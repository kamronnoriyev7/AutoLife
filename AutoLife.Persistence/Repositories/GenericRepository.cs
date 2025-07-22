using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace AutoLife.Persistence.Repositories;

public  class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();

    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async  Task<T?> GetByIdAsync(Guid id, string includeProperties = "", bool includeDeleted = false, bool asNoTracking = false) => await _dbSet.FindAsync(id);
    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
     => await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public async Task AddRangeAsync(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);
    public void Update(T entity) => _dbSet.Update(entity);
    public void Remove(T entity) => _dbSet.Remove(entity);
    public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

    public async Task<IEnumerable<T>> GetPagedListAsync(
         int pageNumber, int pageSize,
         Expression<Func<T, bool>>? predicate = null,
         string includeProperties = "",
         bool asNoTracking = true)
    {
        IQueryable<T> query = _dbSet;

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

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate == null)
            return await _dbSet.CountAsync();
        return await _dbSet.CountAsync(predicate);
    }

    public async Task<bool> IsUniqueAsync(T entity, params Expression<Func<T, object>>[] uniqueProperties)
    {
        IQueryable<T> query = _dbSet;

        foreach (var propExpr in uniqueProperties)
        {
            var compiled = propExpr.Compile();
            var value = compiled(entity);

            query = query.Where(e => EF.Property<object>(e, GetPropertyName(propExpr))!.Equals(value));
        }

        if (entity.BasaEntityId != Guid.Empty)
        {
            // agar yangilanyapti deb hisoblasak, o'zini tekshirishdan chiqaramiz
            query = query.Where(e => e.BasaEntityId != entity.BasaEntityId);
        }

        return !await query.AnyAsync();
    }

    private string GetPropertyName(Expression<Func<T, object>> expression)
    {
        if (expression.Body is MemberExpression member)
            return member.Member.Name;

        if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression memberOperand)
            return memberOperand.Member.Name;

        throw new InvalidOperationException("Invalid property expression");
    }


    public void UpdateRange(IEnumerable<T> entities)
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

    public async Task<IEnumerable<T>> FromSqlRawAsync(string sql, params object[] parameters)
    {
        return await _dbSet.FromSqlRaw(sql, parameters).ToListAsync();
    }

}

