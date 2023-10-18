using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EmailAuthenticatorRepository : EfRepositoryBase<EmailAuthenticator, Guid, MiningHQDbContext>, IEmailAuthenticatorRepository
{
    public EmailAuthenticatorRepository(MiningHQDbContext context)
        : base(context) { }
}
