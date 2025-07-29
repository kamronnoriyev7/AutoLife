using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.FuelSubTypeRepositories;

public class FuelSubTypeRepository : GenericRepository<FuelSubType>, IFuelSubTypeRepository
{
    public FuelSubTypeRepository(AppDbContext context) : base(context)
    {
    }
}