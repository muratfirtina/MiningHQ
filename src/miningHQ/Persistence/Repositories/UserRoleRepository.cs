using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserRoleRepository : EfRepositoryBase<UserRole, Guid, MiningHQDbContext>, IUserRoleRepository
{
    public UserRoleRepository(MiningHQDbContext context)
        : base(context) { }
}
