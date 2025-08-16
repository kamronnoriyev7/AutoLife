using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoLife.Persistence.Repositories
{
    public interface IGenericRepository<TEntity, TContext>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        Task<IEnumerable<TEntity>> GetPagedListAsync(
            int pageNumber, int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            string includeProperties = "",
            bool asNoTracking = true);

        Task<TEntity?> GetByIdAsync(Guid id, string includeProperties = "", bool includeDeleted = false, bool asNoTracking = false);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
        Task<bool> IsUniqueAsync(TEntity entity, params Expression<Func<TEntity, object>>[] uniqueProperties);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task SoftDeleteAsync(Guid id);
        Task RestoreDeletedAsync(Guid id);
        Task<List<TResult>> FromSqlRawAsync<TResult>(string sql, params object[] parameters) where TResult : class;
        IQueryable<TEntity> GetQueryable();
    }
}
