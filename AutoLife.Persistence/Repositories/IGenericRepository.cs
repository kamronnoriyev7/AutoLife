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
    Task<T?> GetByIdAsync(long id, string includeProperties = "", bool includeDeleted = false, bool asNoTracking = false);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}

