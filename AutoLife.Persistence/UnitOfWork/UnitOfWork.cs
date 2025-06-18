using AutoLife.Domain.Interfaces;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context; // Change DbContext to AppDbContext
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(AppDbContext context) // Update constructor parameter type
    {
        _context = context;
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T);
        if (!_repositories.ContainsKey(type))
        {
            var repoInstance = new GenericRepository<T>(_context); // _context is now AppDbContext
            _repositories[type] = repoInstance;
        }

        return (IGenericRepository<T>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

