using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class MachineRepository : EfRepositoryBase<Machine, Guid, MiningHQDbContext>, IMachineRepository
{
    public MachineRepository(MiningHQDbContext context) : base(context)
    {
    }
}