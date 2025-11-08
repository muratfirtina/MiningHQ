using Application.Features.QuarryModerators.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Constants;
using MediatR;

namespace Application.Features.QuarryModerators.Commands.Delete;

public class DeleteQuarryModeratorCommand : IRequest<DeletedQuarryModeratorResponse>, 
    ISecuredRequest
{
    public Guid UserId { get; set; }
    public Guid QuarryId { get; set; }
    
    public string[] Roles => new[] { Domain.Constants.Roles.Admin };

    public class DeleteQuarryModeratorCommandHandler : 
        IRequestHandler<DeleteQuarryModeratorCommand, DeletedQuarryModeratorResponse>
    {
        private readonly IQuarryModeratorRepository _quarryModeratorRepository;
        private readonly QuarryModeratorBusinessRules _businessRules;
        private readonly IMapper _mapper;

        public DeleteQuarryModeratorCommandHandler(
            IQuarryModeratorRepository quarryModeratorRepository,
            QuarryModeratorBusinessRules businessRules,
            IMapper mapper)
        {
            _quarryModeratorRepository = quarryModeratorRepository;
            _businessRules = businessRules;
            _mapper = mapper;
        }

        public async Task<DeletedQuarryModeratorResponse> Handle(
            DeleteQuarryModeratorCommand request, 
            CancellationToken cancellationToken)
        {
            await _businessRules.AssignmentShouldExist(request.UserId, request.QuarryId);

            var quarryModerator = await _quarryModeratorRepository.GetAsync(
                qm => qm.UserId == request.UserId && qm.QuarryId == request.QuarryId);

            await _quarryModeratorRepository.DeleteAsync(quarryModerator!);

            DeletedQuarryModeratorResponse response = _mapper.Map<DeletedQuarryModeratorResponse>(quarryModerator);
            return response;
        }
    }
}
