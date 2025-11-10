using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RoleOperationClaimRepository : EfRepositoryBase<RoleOperationClaim, Guid, MiningHQDbContext>, IRoleOperationClaimRepository
{
    public RoleOperationClaimRepository(MiningHQDbContext context)
        : base(context) { }
}
