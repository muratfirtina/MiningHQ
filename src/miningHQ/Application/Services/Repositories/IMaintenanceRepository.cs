using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IMaintenanceRepository : IAsyncRepository<Maintenance, Guid>, IRepository<Maintenance, Guid>
{
}