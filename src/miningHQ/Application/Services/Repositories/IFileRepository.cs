using Domain.Entities;
using Core.Persistence.Repositories;
using File = Domain.Entities.File;

namespace Application.Services.Repositories;

public interface IFileRepository : IAsyncRepository<File, Guid>, IRepository<File, Guid>
{
}