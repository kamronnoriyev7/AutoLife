using AutoLife.Domain.Entities;
using AutoLife.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoLife.Persistence.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public IGenericRepository<TEntity, TContext> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                var repoInstance = new GenericRepository<TEntity, TContext>(_context);
                _repositories[type] = repoInstance;
            }

            return (IGenericRepository<TEntity, TContext>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
