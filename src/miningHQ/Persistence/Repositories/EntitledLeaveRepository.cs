using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EntitledLeaveRepository : EfRepositoryBase<EntitledLeave, Guid, MiningHQDbContext>, IEntitledLeaveRepository
{
    public EntitledLeaveRepository(MiningHQDbContext context) : base(context)
    {
    }
}