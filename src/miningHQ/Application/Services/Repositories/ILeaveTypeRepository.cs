using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ILeaveTypeRepository : IAsyncRepository<LeaveType, Guid>, IRepository<LeaveType, Guid>
{
}