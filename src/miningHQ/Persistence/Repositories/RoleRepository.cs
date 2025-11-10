using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RoleRepository : EfRepositoryBase<Role, int, MiningHQDbContext>, IRoleRepository
{
    public RoleRepository(MiningHQDbContext context)
        : base(context) { }
}
