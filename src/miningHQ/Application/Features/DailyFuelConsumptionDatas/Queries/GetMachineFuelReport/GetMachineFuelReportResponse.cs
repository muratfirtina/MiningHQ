using Core.Application.Responses;

namespace Application.Features.DailyFuelConsumptionDatas.Queries.GetMachineFuelReport;

public class GetMachineFuelReportResponse : IResponse
{
    // Machine Info
    public Guid MachineId { get; set; }
    public string MachineName { get; set; }
    public string? BrandName { get; set; }
    public string? ModelName { get; set; }
    public string? MachineTypeName { get; set; }
    public string SerialNumber { get; set; }
    
    // Date Range
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalDays { get; set; }
    public int WorkingDays { get; set; }
    
    // Statistics
    public double TotalFuelConsumption { get; set; }
    public double TotalWorkHours { get; set; }
    public double DailyAverage { get; set; }
    public double WeeklyAverage { get; set; }
    public double MonthlyAverage { get; set; }
    public double YearlyAverage { get; set; }
    public double FuelPerHour { get; set; }
    public double AverageWorkHoursPerDay { get; set; }
    
    // Detailed Data
    public List<DailyFuelBreakdown> DailyBreakdown { get; set; }
    public List<WeeklyFuelSummary> WeeklySummary { get; set; }
    public List<MonthlyFuelSummary> MonthlySummary { get; set; }
}

public class DailyFuelBreakdown
{
    public DateTime Date { get; set; }
    public double FuelConsumption { get; set; }
    public double WorkingHours { get; set; }
    public double FuelPerHour { get; set; }
}

public class WeeklyFuelSummary
{
    public int Year { get; set; }
    public int WeekNumber { get; set; }
    public double TotalFuel { get; set; }
    public double AverageFuel { get; set; }
    public int DaysWorked { get; set; }
    public double TotalWorkHours { get; set; }
}

public class MonthlyFuelSummary
{
    public int Year { get; set; }
    public int Month { get; set; }
    public double TotalFuel { get; set; }
    public double AverageFuel { get; set; }
    public int DaysWorked { get; set; }
    public double TotalWorkHours { get; set; }
}
