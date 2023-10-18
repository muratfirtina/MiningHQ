using Application.Features.MachineTypes.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.MachineTypes;

public class MachineTypesManager : IMachineTypesService
{
    private readonly IMachineTypeRepository _machineTypeRepository;
    private readonly MachineTypeBusinessRules _machineTypeBusinessRules;

    public MachineTypesManager(IMachineTypeRepository machineTypeRepository, MachineTypeBusinessRules machineTypeBusinessRules)
    {
        _machineTypeRepository = machineTypeRepository;
        _machineTypeBusinessRules = machineTypeBusinessRules;
    }

    public async Task<MachineType?> GetAsync(
        Expression<Func<MachineType, bool>> predicate,
        Func<IQueryable<MachineType>, IIncludableQueryable<MachineType, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        MachineType? machineType = await _machineTypeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return machineType;
    }

    public async Task<IPaginate<MachineType>?> GetListAsync(
        Expression<Func<MachineType, bool>>? predicate = null,
        Func<IQueryable<MachineType>, IOrderedQueryable<MachineType>>? orderBy = null,
        Func<IQueryable<MachineType>, IIncludableQueryable<MachineType, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<MachineType> machineTypeList = await _machineTypeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return machineTypeList;
    }

    public async Task<MachineType> AddAsync(MachineType machineType)
    {
        MachineType addedMachineType = await _machineTypeRepository.AddAsync(machineType);

        return addedMachineType;
    }

    public async Task<MachineType> UpdateAsync(MachineType machineType)
    {
        MachineType updatedMachineType = await _machineTypeRepository.UpdateAsync(machineType);

        return updatedMachineType;
    }

    public async Task<MachineType> DeleteAsync(MachineType machineType, bool permanent = false)
    {
        MachineType deletedMachineType = await _machineTypeRepository.DeleteAsync(machineType);

        return deletedMachineType;
    }
}
