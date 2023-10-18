using Application.Features.Machines.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Machines;

public class MachinesManager : IMachinesService
{
    private readonly IMachineRepository _machineRepository;
    private readonly MachineBusinessRules _machineBusinessRules;

    public MachinesManager(IMachineRepository machineRepository, MachineBusinessRules machineBusinessRules)
    {
        _machineRepository = machineRepository;
        _machineBusinessRules = machineBusinessRules;
    }

    public async Task<Machine?> GetAsync(
        Expression<Func<Machine, bool>> predicate,
        Func<IQueryable<Machine>, IIncludableQueryable<Machine, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Machine? machine = await _machineRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return machine;
    }

    public async Task<IPaginate<Machine>?> GetListAsync(
        Expression<Func<Machine, bool>>? predicate = null,
        Func<IQueryable<Machine>, IOrderedQueryable<Machine>>? orderBy = null,
        Func<IQueryable<Machine>, IIncludableQueryable<Machine, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Machine> machineList = await _machineRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return machineList;
    }

    public async Task<Machine> AddAsync(Machine machine)
    {
        Machine addedMachine = await _machineRepository.AddAsync(machine);

        return addedMachine;
    }

    public async Task<Machine> UpdateAsync(Machine machine)
    {
        Machine updatedMachine = await _machineRepository.UpdateAsync(machine);

        return updatedMachine;
    }

    public async Task<Machine> DeleteAsync(Machine machine, bool permanent = false)
    {
        Machine deletedMachine = await _machineRepository.DeleteAsync(machine);

        return deletedMachine;
    }
}
