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

    public async Task AddAsync(List<EmployeeFile> employeeFiles)
    {
        await Context.Set<EmployeeFile>().AddRangeAsync(employeeFiles);
        await Context.SaveChangesAsync();
    }

    public async Task AddAsync(List<MachineFile> machineFiles)
    {
        await Context.Set<MachineFile>().AddRangeAsync(machineFiles);
        await Context.SaveChangesAsync();
    }
}