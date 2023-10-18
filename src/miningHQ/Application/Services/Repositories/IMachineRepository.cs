using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IMachineRepository : IAsyncRepository<Machine, Guid>, IRepository<Machine, Guid>
{
}