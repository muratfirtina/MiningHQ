using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using File = Domain.Entities.File;

namespace Persistence.Repositories;

public class FileRepository : EfRepositoryBase<File, Guid, MiningHQDbContext>, IFileRepository
{
    public FileRepository(MiningHQDbContext context) : base(context)
    {
    }

    public async Task AddAsync(List<EmployeeFile> toList)
    {
        await Context.Set<EmployeeFile>().AddRangeAsync(toList);
        await Context.SaveChangesAsync();
        
    }
}