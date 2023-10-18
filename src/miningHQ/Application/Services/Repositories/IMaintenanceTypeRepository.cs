using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IMaintenanceTypeRepository : IAsyncRepository<MaintenanceType, Guid>, IRepository<MaintenanceType, Guid>
{
}