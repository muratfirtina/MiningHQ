using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IQuarryProductionRepository : IAsyncRepository<QuarryProduction, Guid>, IRepository<QuarryProduction, Guid>
{
}
