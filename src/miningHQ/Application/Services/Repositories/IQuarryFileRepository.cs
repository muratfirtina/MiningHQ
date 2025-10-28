using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IQuarryFileRepository : IAsyncRepository<QuarryFile, Guid>, IRepository<QuarryFile, Guid>
{
}
