using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IDepartmentRepository : IAsyncRepository<Department, Guid>, IRepository<Department, Guid>
{
}