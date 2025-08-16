using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.NewsRepositories;

public class NewsRepository(AppDbContext context) : GenericRepository<News, AppDbContext>(context), INewsRepository
{
}
