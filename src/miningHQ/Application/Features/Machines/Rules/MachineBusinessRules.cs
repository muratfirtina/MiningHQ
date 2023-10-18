using Application.Features.Machines.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Machines.Rules;

public class MachineBusinessRules : BaseBusinessRules
{
    private readonly IMachineRepository _machineRepository;

    public MachineBusinessRules(IMachineRepository machineRepository)
    {
        _machineRepository = machineRepository;
    }

    public Task MachineShouldExistWhenSelected(Machine? machine)
    {
        if (machine == null)
            throw new BusinessException(MachinesBusinessMessages.MachineNotExists);
        return Task.CompletedTask;
    }

    public async Task MachineIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Machine? machine = await _machineRepository.GetAsync(
            predicate: m => m.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await MachineShouldExistWhenSelected(machine);
    }
}