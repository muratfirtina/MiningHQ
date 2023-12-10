using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IEmployeeLeaveUsageRepository : IAsyncRepository<EmployeeLeaveUsage, Guid>, IRepository<EmployeeLeaveUsage, Guid>
{
}