using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IDailyWorkDataRepository : IAsyncRepository<DailyWorkData, Guid>, IRepository<DailyWorkData, Guid>
{
}