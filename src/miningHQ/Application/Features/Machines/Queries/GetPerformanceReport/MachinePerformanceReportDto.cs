namespace Application.Features.Machines.Queries.GetPerformanceReport;

public class MachinePerformanceReportDto
{
    public Guid MachineId { get; set; }
    public string MachineName { get; set; }
    public string BrandName { get; set; }
    public string ModelName { get; set; }
    
    // Haftalık Veriler
    public WeeklyPerformanceDto WeeklyPerformance { get; set; }
    
    // Aylık Veriler
    public MonthlyPerformanceDto MonthlyPerformance { get; set; }
    
    // Yıllık Veriler
    public YearlyPerformanceDto YearlyPerformance { get; set; }
}

public class WeeklyPerformanceDto
{
    public double TotalWorkingHours { get; set; }
    public int TotalWorkingDays { get; set; }
    public double TotalFuelConsumption { get; set; }
    public double AverageDailyHours { get; set; }
    public double AverageDailyFuelConsumption { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class MonthlyPerformanceDto
{
    public double TotalWorkingHours { get; set; }
    public int TotalWorkingDays { get; set; }
    public double TotalFuelConsumption { get; set; }
    public double AverageDailyHours { get; set; }
    public double AverageDailyFuelConsumption { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class YearlyPerformanceDto
{
    public double TotalWorkingHours { get; set; }
    public int TotalWorkingDays { get; set; }
    public double TotalFuelConsumption { get; set; }
    public double AverageDailyHours { get; set; }
    public double AverageDailyFuelConsumption { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
