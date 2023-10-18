using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Domain.Entities;
using File = Domain.Entities.File;

namespace Persistence.Contexts;

public class MiningHQDbContext : DbContext
{
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<DailyFuelConsumptionData> DailyFuelConsumptionDatas { get; set; }
    public DbSet<MaintenanceType> MaintenanceTypes { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<LeaveUsage> LeaveUsages { get; set; }
    public DbSet<MachineType> MachineTypes { get; set; }
    public DbSet<Quarry> Quarries { get; set; }
    public DbSet<DailyWorkData> DailyWorkDatas { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<EmployeeLeave> EmployeeLeaves { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public MiningHQDbContext(DbContextOptions<MiningHQDbContext> dbContextOptions): base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
