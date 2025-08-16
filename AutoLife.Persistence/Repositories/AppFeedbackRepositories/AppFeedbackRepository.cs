using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.AppFeedbackRepositories;

public class AppFeedbackRepository : GenericRepository<AppFeedback, AppDbContext>, IAppFeedbackRepository
{
    public AppFeedbackRepository(AppDbContext context) : base(context)
    {
    }


}
