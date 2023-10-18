using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ILeaveUsageRepository : IAsyncRepository<LeaveUsage, Guid>, IRepository<LeaveUsage, Guid>
{
}