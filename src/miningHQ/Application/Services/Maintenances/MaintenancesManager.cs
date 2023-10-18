using Application.Features.Maintenances.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Maintenances;

public class MaintenancesManager : IMaintenancesService
{
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly MaintenanceBusinessRules _maintenanceBusinessRules;

    public MaintenancesManager(IMaintenanceRepository maintenanceRepository, MaintenanceBusinessRules maintenanceBusinessRules)
    {
        _maintenanceRepository = maintenanceRepository;
        _maintenanceBusinessRules = maintenanceBusinessRules;
    }

    public async Task<Maintenance?> GetAsync(
        Expression<Func<Maintenance, bool>> predicate,
        Func<IQueryable<Maintenance>, IIncludableQueryable<Maintenance, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Maintenance? maintenance = await _maintenanceRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return maintenance;
    }

    public async Task<IPaginate<Maintenance>?> GetListAsync(
        Expression<Func<Maintenance, bool>>? predicate = null,
        Func<IQueryable<Maintenance>, IOrderedQueryable<Maintenance>>? orderBy = null,
        Func<IQueryable<Maintenance>, IIncludableQueryable<Maintenance, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Maintenance> maintenanceList = await _maintenanceRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return maintenanceList;
    }

    public async Task<Maintenance> AddAsync(Maintenance maintenance)
    {
        Maintenance addedMaintenance = await _maintenanceRepository.AddAsync(maintenance);

        return addedMaintenance;
    }

    public async Task<Maintenance> UpdateAsync(Maintenance maintenance)
    {
        Maintenance updatedMaintenance = await _maintenanceRepository.UpdateAsync(maintenance);

        return updatedMaintenance;
    }

    public async Task<Maintenance> DeleteAsync(Maintenance maintenance, bool permanent = false)
    {
        Maintenance deletedMaintenance = await _maintenanceRepository.DeleteAsync(maintenance);

        return deletedMaintenance;
    }
}
