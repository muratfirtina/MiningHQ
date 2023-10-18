using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IQuarryRepository : IAsyncRepository<Quarry, Guid>, IRepository<Quarry, Guid>
{
}