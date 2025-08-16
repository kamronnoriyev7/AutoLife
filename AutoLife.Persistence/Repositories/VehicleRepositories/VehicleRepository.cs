using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.VehicleRepositories;

public class VehicleRepository : GenericRepository<Vehicle, AppDbContext>, IVehicleRepository
{
    public VehicleRepository(AppDbContext context) : base(context)
    {
    }
}

