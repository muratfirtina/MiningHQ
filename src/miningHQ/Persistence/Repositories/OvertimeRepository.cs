using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OvertimeRepository : EfRepositoryBase<Overtime, Guid, MiningHQDbContext>, IOvertimeRepository
{
    public OvertimeRepository(MiningHQDbContext context) : base(context)
    {
    }
    
    public async Task<float> GetTotalOvertimeHoursByEmployeeId(string? employeeId, DateTime? startDate, DateTime? endDate)
    {
        //eğer startdate ve enddate null ise listenin tamamını getir.
        
        if (startDate == null && endDate == null)
        {
            return await Context.Overtimes
                .Where(o => o.EmployeeId == Guid.Parse(employeeId))
                .SumAsync(o => o.OvertimeHours ?? 0);
        }

        //eğer startdate verilmiş enddate verilmemiş ise startdate'den bugüne kadar olan veriyi getir.
        if (startDate != null && endDate == null)
        {
            return await Context.Overtimes
                .Where(o => o.EmployeeId == Guid.Parse(employeeId) && o.OvertimeDate >= startDate)
                .SumAsync(o => o.OvertimeHours ?? 0);
        }
         
        return await Context.Overtimes
                .Where(o => o.EmployeeId == Guid.Parse(employeeId) && o.OvertimeDate >= startDate && o.OvertimeDate <= endDate)
                .SumAsync(o => o.OvertimeHours ?? 0);
        
    }
}