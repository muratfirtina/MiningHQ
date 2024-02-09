using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITimekeepingRepository : IAsyncRepository<Timekeeping, Guid>, IRepository<Timekeeping, Guid>
{
}