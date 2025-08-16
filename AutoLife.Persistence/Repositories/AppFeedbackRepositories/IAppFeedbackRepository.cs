using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.AppFeedbackRepositories;

public interface IAppFeedbackRepository : IGenericRepository<AppFeedback, AppDbContext>
{

}