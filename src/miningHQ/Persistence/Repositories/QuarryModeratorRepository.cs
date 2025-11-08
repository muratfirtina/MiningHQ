using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class QuarryModeratorRepository : EfRepositoryBase<QuarryModerator, Guid, MiningHQDbContext>, 
    IQuarryModeratorRepository
{
    public QuarryModeratorRepository(MiningHQDbContext context) : base(context)
    {
    }
}
