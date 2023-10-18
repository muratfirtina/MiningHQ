using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IEmployeeLeaveRepository : IAsyncRepository<EmployeeLeave, Guid>, IRepository<EmployeeLeave, Guid>
{
}