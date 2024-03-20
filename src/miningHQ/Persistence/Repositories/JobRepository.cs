using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class JobRepository : EfRepositoryBase<Job, Guid, MiningHQDbContext>, IJobRepository
{
    public JobRepository(MiningHQDbContext context) : base(context)
    {
    }

    public async Task<Job?> GetJobByIdWithDepartmentIdAsync(string jobId)
    {
        return await Context.Jobs
            .Include(j => j.Department)
            .FirstOrDefaultAsync(j => j.Id.ToString() == jobId);}
}