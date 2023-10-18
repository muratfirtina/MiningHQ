using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OperationClaimRepository : EfRepositoryBase<OperationClaim, int, MiningHQDbContext>, IOperationClaimRepository
{
    public OperationClaimRepository(MiningHQDbContext context)
        : base(context) { }
}
