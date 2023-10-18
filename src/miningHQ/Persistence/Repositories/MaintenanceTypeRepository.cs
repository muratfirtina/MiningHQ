using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class MaintenanceTypeRepository : EfRepositoryBase<MaintenanceType, Guid, MiningHQDbContext>, IMaintenanceTypeRepository
{
    public MaintenanceTypeRepository(MiningHQDbContext context) : base(context)
    {
    }
}