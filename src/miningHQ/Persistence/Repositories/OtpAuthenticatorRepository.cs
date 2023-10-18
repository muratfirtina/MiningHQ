using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OtpAuthenticatorRepository : EfRepositoryBase<OtpAuthenticator, Guid, MiningHQDbContext>, IOtpAuthenticatorRepository
{
    public OtpAuthenticatorRepository(MiningHQDbContext context)
        : base(context) { }
}
