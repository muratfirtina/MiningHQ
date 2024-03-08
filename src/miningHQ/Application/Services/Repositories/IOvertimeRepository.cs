using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IOvertimeRepository : IAsyncRepository<Overtime, Guid>, IRepository<Overtime, Guid>
{
    Task<float> GetTotalOvertimeHoursByEmployeeId(string? employeeId, DateTime? startDate, DateTime? endDate);
}