using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class JobRepository : EfRepositoryBase<Job, Guid, MiningHQDbContext>, IJobRepository
{
    public JobRepository(MiningHQDbContext context) : base(context)
    {
    }
}