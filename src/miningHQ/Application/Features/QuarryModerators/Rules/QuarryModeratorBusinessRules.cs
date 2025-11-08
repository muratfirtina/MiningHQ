using Application.Features.QuarryModerators.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.QuarryModerators.Rules;

public class QuarryModeratorBusinessRules : BaseBusinessRules
{
    private readonly IQuarryModeratorRepository _quarryModeratorRepository;
    private readonly IUserRepository _userRepository;
    private readonly IQuarryRepository _quarryRepository;

    public QuarryModeratorBusinessRules(
        IQuarryModeratorRepository quarryModeratorRepository,
        IUserRepository userRepository,
        IQuarryRepository quarryRepository)
    {
        _quarryModeratorRepository = quarryModeratorRepository;
        _userRepository = userRepository;
        _quarryRepository = quarryRepository;
    }

    public async Task UserShouldExist(Guid userId)
    {
        var user = await _userRepository.GetAsync(u => u.Id == userId);
        if (user == null)
            throw new BusinessException(QuarryModeratorMessages.UserNotFound);
    }

    public async Task QuarryShouldExist(Guid quarryId)
    {
        var quarry = await _quarryRepository.GetAsync(q => q.Id == quarryId);
        if (quarry == null)
            throw new BusinessException(QuarryModeratorMessages.QuarryNotFound);
    }

    public async Task AssignmentShouldNotExist(Guid userId, Guid quarryId)
    {
        var exists = await _quarryModeratorRepository.GetAsync(
            qm => qm.UserId == userId && qm.QuarryId == quarryId);
        
        if (exists != null)
            throw new BusinessException(QuarryModeratorMessages.AssignmentAlreadyExists);
    }

    public async Task AssignmentShouldExist(Guid userId, Guid quarryId)
    {
        var exists = await _quarryModeratorRepository.GetAsync(
            qm => qm.UserId == userId && qm.QuarryId == quarryId);
        
        if (exists == null)
            throw new BusinessException(QuarryModeratorMessages.AssignmentNotFound);
    }

    public Task QuarryModeratorShouldExistWhenSelected(QuarryModerator? quarryModerator)
    {
        if (quarryModerator == null)
            throw new BusinessException(QuarryModeratorMessages.AssignmentNotFound);
        return Task.CompletedTask;
    }
}
