using Application.Features.MachineTypes.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.MachineTypes.Rules;

public class MachineTypeBusinessRules : BaseBusinessRules
{
    private readonly IMachineTypeRepository _machineTypeRepository;

    public MachineTypeBusinessRules(IMachineTypeRepository machineTypeRepository)
    {
        _machineTypeRepository = machineTypeRepository;
    }

    public Task MachineTypeShouldExistWhenSelected(MachineType? machineType)
    {
        if (machineType == null)
            throw new BusinessException(MachineTypesBusinessMessages.MachineTypeNotExists);
        return Task.CompletedTask;
    }

    public async Task MachineTypeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        MachineType? machineType = await _machineTypeRepository.GetAsync(
            predicate: mt => mt.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await MachineTypeShouldExistWhenSelected(machineType);
    }
}