using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IDailyFuelConsumptionDataRepository : IAsyncRepository<DailyFuelConsumptionData, Guid>, IRepository<DailyFuelConsumptionData, Guid>
{
}