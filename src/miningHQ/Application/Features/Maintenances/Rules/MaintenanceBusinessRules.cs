using Application.Features.Maintenances.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Maintenances.Rules;

public class MaintenanceBusinessRules : BaseBusinessRules
{
    private readonly IMaintenanceRepository _maintenanceRepository;

    public MaintenanceBusinessRules(IMaintenanceRepository maintenanceRepository)
    {
        _maintenanceRepository = maintenanceRepository;
    }

    public Task MaintenanceShouldExistWhenSelected(Maintenance? maintenance)
    {
        if (maintenance == null)
            throw new BusinessException(MaintenancesBusinessMessages.MaintenanceNotExists);
        return Task.CompletedTask;
    }

    public async Task MaintenanceIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Maintenance? maintenance = await _maintenanceRepository.GetAsync(
            predicate: m => m.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await MaintenanceShouldExistWhenSelected(maintenance);
    }
}