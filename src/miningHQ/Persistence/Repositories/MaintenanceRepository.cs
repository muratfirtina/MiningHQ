using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class MaintenanceRepository : EfRepositoryBase<Maintenance, Guid, MiningHQDbContext>, IMaintenanceRepository
{
    public MaintenanceRepository(MiningHQDbContext context) : base(context)
    {
    }
}