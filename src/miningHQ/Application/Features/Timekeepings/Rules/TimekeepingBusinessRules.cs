using Application.Features.Timekeepings.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Timekeepings.Rules;

public class TimekeepingBusinessRules : BaseBusinessRules
{
    private readonly ITimekeepingRepository _timekeepingRepository;

    public TimekeepingBusinessRules(ITimekeepingRepository timekeepingRepository)
    {
        _timekeepingRepository = timekeepingRepository;
    }

    public Task TimekeepingShouldExistWhenSelected(Timekeeping? timekeeping)
    {
        if (timekeeping == null)
            throw new BusinessException(TimekeepingsBusinessMessages.TimekeepingNotExists);
        return Task.CompletedTask;
    }

    public async Task TimekeepingIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Timekeeping? timekeeping = await _timekeepingRepository.GetAsync(
            predicate: t => t.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TimekeepingShouldExistWhenSelected(timekeeping);
    }
}