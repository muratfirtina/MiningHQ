using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserRepository : EfRepositoryBase<User, Guid, MiningHQDbContext>, IUserRepository
{
    public UserRepository(MiningHQDbContext context)
        : base(context) { }
}
