using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.NewsRepositories;

public class NewsRepository : GenericRepository<News>, INewsRepository
{
    public NewsRepository(DbContext context) : base(context)
    {
    }
}
