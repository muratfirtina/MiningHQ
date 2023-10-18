using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class MachineTypeRepository : EfRepositoryBase<MachineType, Guid, MiningHQDbContext>, IMachineTypeRepository
{
    public MachineTypeRepository(MiningHQDbContext context) : base(context)
    {
    }
}