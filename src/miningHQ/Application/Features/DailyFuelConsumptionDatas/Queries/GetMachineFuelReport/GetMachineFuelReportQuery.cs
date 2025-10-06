using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DailyFuelConsumptionDatas.Queries.GetMachineFuelReport;

public class GetMachineFuelReportQuery : IRequest<GetMachineFuelReportResponse>
{
    public Guid MachineId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public class GetMachineFuelReportQueryHandler : IRequestHandler<GetMachineFuelReportQuery, GetMachineFuelReportResponse>
    {
        private readonly IDailyFuelConsumptionDataRepository _fuelRepository;
        private readonly IDailyWorkDataRepository _workDataRepository;
        private readonly IMachineRepository _machineRepository;

        public GetMachineFuelReportQueryHandler(
            IDailyFuelConsumptionDataRepository fuelRepository,
            IDailyWorkDataRepository workDataRepository,
            IMachineRepository machineRepository)
        {
            _fuelRepository = fuelRepository;
            _workDataRepository = workDataRepository;
            _machineRepository = machineRepository;
        }

        public async Task<GetMachineFuelReportResponse> Handle(GetMachineFuelReportQuery request, CancellationToken cancellationToken)
        {
            // Get machine info
            var machine = await _machineRepository.GetAsync(
                predicate: m => m.Id == request.MachineId,
                include: m => m
                    .Include(x => x.Model)
                        .ThenInclude(model => model.Brand)
                    .Include(x => x.MachineType),
                cancellationToken: cancellationToken
            );

            if (machine == null)
                throw new Exception("Machine not found");

            // Set date range (default: last 30 days)
            var endDate = request.EndDate ?? DateTime.Now;
            var startDate = request.StartDate ?? endDate.AddDays(-30);
            
            // For inclusive date range, add 1 day to endDate for comparison
            var endDateExclusive = endDate.Date.AddDays(1);

            // Get fuel consumption data (all data, no pagination)
            var fuelData = await _fuelRepository.GetListAsync(
                predicate: f => f.MachineId == request.MachineId && 
                               f.Date >= startDate && 
                               f.Date < endDateExclusive,
                orderBy: q => q.OrderByDescending(f => f.Date),
                index: 0,
                size: 10000,
                cancellationToken: cancellationToken
            );

            // Get work data (all data, no pagination)
            var workData = await _workDataRepository.GetListAsync(
                predicate: w => w.MachineId == request.MachineId && 
                               w.Date >= startDate && 
                               w.Date < endDateExclusive,
                orderBy: q => q.OrderByDescending(w => w.Date),
                index: 0,
                size: 10000,
                cancellationToken: cancellationToken
            );

            var fuelItems = fuelData.Items.ToList();
            var workItems = workData.Items.ToList();

            // Calculate statistics
            var totalFuel = fuelItems.Sum(f => f.FuelConsumption);
            var totalWorkHours = workItems.Sum(w => w.WorkingHoursOrKm);
            var totalDays = (endDate - startDate).Days + 1;
            var workingDays = fuelItems.Count;

            var dailyAverage = workingDays > 0 ? totalFuel / workingDays : 0;
            var weeklyAverage = workingDays > 0 ? (totalFuel / totalDays) * 7 : 0;
            var monthlyAverage = workingDays > 0 ? (totalFuel / totalDays) * 30 : 0;
            var yearlyAverage = workingDays > 0 ? (totalFuel / totalDays) * 365 : 0;
            
            var fuelPerHour = totalWorkHours > 0 ? totalFuel / totalWorkHours : 0;
            var averageWorkHoursPerDay = workingDays > 0 ? totalWorkHours / workingDays : 0;

            // Combined daily breakdown
            var dailyBreakdown = fuelItems.Select(f => new DailyFuelBreakdown
            {
                Date = f.Date,
                FuelConsumption = f.FuelConsumption,
                WorkingHours = workItems.FirstOrDefault(w => w.Date.Date == f.Date.Date)?.WorkingHoursOrKm ?? 0,
                FuelPerHour = workItems.FirstOrDefault(w => w.Date.Date == f.Date.Date)?.WorkingHoursOrKm > 0 
                    ? f.FuelConsumption / workItems.First(w => w.Date.Date == f.Date.Date).WorkingHoursOrKm 
                    : 0
            }).ToList();

            // Weekly summary
            var weeklyData = fuelItems
                .GroupBy(f => new 
                { 
                    Year = f.Date.Year, 
                    Week = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                        f.Date, 
                        System.Globalization.CalendarWeekRule.FirstDay, 
                        DayOfWeek.Monday)
                })
                .Select(g => new WeeklyFuelSummary
                {
                    Year = g.Key.Year,
                    WeekNumber = g.Key.Week,
                    TotalFuel = g.Sum(f => f.FuelConsumption),
                    AverageFuel = g.Average(f => f.FuelConsumption),
                    DaysWorked = g.Count(),
                    TotalWorkHours = workItems
                        .Where(w => System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                            w.Date, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday) == g.Key.Week
                            && w.Date.Year == g.Key.Year)
                        .Sum(w => w.WorkingHoursOrKm)
                })
                .OrderBy(w => w.Year)
                .ThenBy(w => w.WeekNumber)
                .ToList();

            // Monthly summary
            var monthlyData = fuelItems
                .GroupBy(f => new { f.Date.Year, f.Date.Month })
                .Select(g => new MonthlyFuelSummary
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalFuel = g.Sum(f => f.FuelConsumption),
                    AverageFuel = g.Average(f => f.FuelConsumption),
                    DaysWorked = g.Count(),
                    TotalWorkHours = workItems
                        .Where(w => w.Date.Year == g.Key.Year && w.Date.Month == g.Key.Month)
                        .Sum(w => w.WorkingHoursOrKm)
                })
                .OrderBy(m => m.Year)
                .ThenBy(m => m.Month)
                .ToList();

            return new GetMachineFuelReportResponse
            {
                MachineId = machine.Id,
                MachineName = machine.Name,
                BrandName = machine.Model?.Brand?.Name,
                ModelName = machine.Model?.Name,
                MachineTypeName = machine.MachineType?.Name,
                SerialNumber = machine.SerialNumber,
                
                StartDate = startDate,
                EndDate = endDate,
                TotalDays = totalDays,
                WorkingDays = workingDays,
                
                TotalFuelConsumption = totalFuel,
                TotalWorkHours = totalWorkHours,
                DailyAverage = dailyAverage,
                WeeklyAverage = weeklyAverage,
                MonthlyAverage = monthlyAverage,
                YearlyAverage = yearlyAverage,
                FuelPerHour = fuelPerHour,
                AverageWorkHoursPerDay = averageWorkHoursPerDay,
                
                DailyBreakdown = dailyBreakdown,
                WeeklySummary = weeklyData,
                MonthlySummary = monthlyData
            };
        }
    }
}
