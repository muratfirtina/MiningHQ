using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IJobRepository : IAsyncRepository<Job, Guid>, IRepository<Job, Guid>
{
    Task<Job?> GetJobByIdWithDepartmentIdAsync(string jobId);
}