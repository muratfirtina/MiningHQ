using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, Guid, MiningHQDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(MiningHQDbContext context)
        : base(context) { }
}
