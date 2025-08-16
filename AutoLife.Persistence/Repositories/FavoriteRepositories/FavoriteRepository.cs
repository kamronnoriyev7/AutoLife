using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.FavoriteRepositories;

public class FavoriteRepository : GenericRepository<Favorite, AppDbContext>, IFavoriteRepository
{
    public FavoriteRepository(AppDbContext context) : base(context)
    {
    }
}
