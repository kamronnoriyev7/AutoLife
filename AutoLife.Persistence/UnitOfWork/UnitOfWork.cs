using AutoLife.Domain.Entities;
using AutoLife.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoLife.Persistence.UnitOfWork;

public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
{
    private readonly TContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(TContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
        var type = typeof(T);
        if (!_repositories.ContainsKey(type))
        {
            var repoInstance = new GenericRepository<T>(_context);
            _repositories[type] = repoInstance;
        }

        return (IGenericRepository<T>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}

