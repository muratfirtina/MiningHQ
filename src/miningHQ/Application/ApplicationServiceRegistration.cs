using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using Application.Services.Roles;
using Application.Services.UserRoles;
using Application.Services.RoleOperationClaims;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.ElasticSearch;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Services.Brands;
using Application.Services.DailyFuelConsumptionDatas;
using Application.Services.MaintenanceTypes;
using Application.Services.Maintenances;
using Application.Services.Jobs;
using Application.Services.LeaveUsages;
using Application.Services.MachineTypes;
using Application.Services.Quarries;
using Application.Services.DailyWorkDatas;
using Application.Services.Models;
using Application.Services.EmployeeLeaveUsages;
using Application.Services.Machines;
using Application.Services.Files;
using Application.Services.Employees;
using Application.Services.EntitledLeaves;
using Application.Services.Timekeepings;
using Application.Services.Overtimes;
using Application.Services.Departments;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMailService, MailKitMailService>();
        services.AddSingleton<LoggerServiceBase, FileLogger>();
        services.AddSingleton<IElasticSearch, ElasticSearchManager>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<IRoleService, RoleManager>();
        services.AddScoped<IUserRoleService, UserRoleManager>();
        services.AddScoped<IRoleOperationClaimService, RoleOperationClaimManager>();

        services.AddScoped<IBrandsService, BrandsManager>();
        services.AddScoped<IDailyFuelConsumptionDatasService, DailyFuelConsumptionDatasManager>();
        services.AddScoped<IMaintenanceTypesService, MaintenanceTypesManager>();
        services.AddScoped<IMaintenancesService, MaintenancesManager>();
        services.AddScoped<IJobsService, JobsManager>();
        services.AddScoped<ILeaveUsagesService, LeaveUsagesManager>();
        services.AddScoped<IMachineTypesService, MachineTypesManager>();
        services.AddScoped<IQuarriesService, QuarriesManager>();
        services.AddScoped<IDailyWorkDatasService, DailyWorkDatasManager>();
        services.AddScoped<IModelsService, ModelsManager>();
        services.AddScoped<IEmployeeLeaveUsagesService, EmployeeLeaveUsagesManager>();
        services.AddScoped<IMachinesService, MachinesManager>();
        services.AddScoped<IFilesService, FilesManager>();
        services.AddScoped<IEmployeesService, EmployeesManager>();
        services.AddScoped<IEntitledLeavesService, EntitledLeavesManager>();
        services.AddScoped<ITimekeepingsService, TimekeepingsManager>();
        services.AddScoped<IOvertimesService, OvertimesManager>();
        services.AddScoped<IDepartmentsService, DepartmentsManager>();
        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
