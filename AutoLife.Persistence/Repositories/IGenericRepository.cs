using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using AutoLife.Domain.Entities;


namespace AutoLife.Persistence.Repositories;


public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetPagedListAsync(
            int pageNumber, int pageSize,
            Expression<Func<T, bool>>? predicate = null,
            string includeProperties = "",
            bool asNoTracking = true);

    Task<T?> GetByIdAsync(Guid id, string includeProperties = "", bool includeDeleted = false, bool asNoTracking = false);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    Task<bool> IsUniqueAsync(T entity, params Expression<Func<T, object>>[] uniqueProperties);
    void UpdateRange(IEnumerable<T> entities);
    Task SoftDeleteAsync(Guid id);
    Task RestoreDeletedAsync(Guid id);
    Task<List<T>> FromSqlRawAsync<T>(string sql, params object[] parameters) where T : class;
    IQueryable<T> GetQueryable();

}

