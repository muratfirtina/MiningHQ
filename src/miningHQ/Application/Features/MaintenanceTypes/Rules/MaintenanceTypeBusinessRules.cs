using Application.Features.MaintenanceTypes.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.MaintenanceTypes.Rules;

public class MaintenanceTypeBusinessRules : BaseBusinessRules
{
    private readonly IMaintenanceTypeRepository _maintenanceTypeRepository;

    public MaintenanceTypeBusinessRules(IMaintenanceTypeRepository maintenanceTypeRepository)
    {
        _maintenanceTypeRepository = maintenanceTypeRepository;
    }

    public Task MaintenanceTypeShouldExistWhenSelected(MaintenanceType? maintenanceType)
    {
        if (maintenanceType == null)
            throw new BusinessException(MaintenanceTypesBusinessMessages.MaintenanceTypeNotExists);
        return Task.CompletedTask;
    }

    public async Task MaintenanceTypeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        MaintenanceType? maintenanceType = await _maintenanceTypeRepository.GetAsync(
            predicate: mt => mt.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await MaintenanceTypeShouldExistWhenSelected(maintenanceType);
    }
}