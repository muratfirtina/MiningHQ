using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.DbConfig;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<MiningHQDbContext>(options => options.UseInMemoryDatabase("nArchitecture"));
        services.AddDbContext<MiningHQDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
        services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
        services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();

        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IDailyFuelConsumptionDataRepository, DailyFuelConsumptionDataRepository>();
        services.AddScoped<IMaintenanceTypeRepository, MaintenanceTypeRepository>();
        services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<IMachineTypeRepository, MachineTypeRepository>();
        services.AddScoped<IQuarryRepository, QuarryRepository>();
        services.AddScoped<IDailyWorkDataRepository, DailyWorkDataRepository>();
        services.AddScoped<IModelRepository, ModelRepository>();
        services.AddScoped<IEmployeeLeaveUsageRepository, EmployeeLeaveUsageRepository>();
        services.AddScoped<IMachineRepository, MachineRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEntitledLeaveRepository, EntitledLeaveRepository>();
        services.AddScoped<ITimekeepingRepository, TimekeepingRepository>();
        return services;
    }
}
