using Application.Features.QuarryModerators.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Constants;
using Domain.Entities;
using MediatR;

namespace Application.Features.QuarryModerators.Commands.Create;

public class CreateQuarryModeratorCommand : IRequest<CreatedQuarryModeratorResponse>, 
    ISecuredRequest
{
    public Guid UserId { get; set; }
    public Guid QuarryId { get; set; }
    
    public string[] Roles => new[] { Domain.Constants.Roles.Admin };

    public class CreateQuarryModeratorCommandHandler : 
        IRequestHandler<CreateQuarryModeratorCommand, CreatedQuarryModeratorResponse>
    {
        private readonly IQuarryModeratorRepository _quarryModeratorRepository;
        private readonly QuarryModeratorBusinessRules _businessRules;
        private readonly IMapper _mapper;

        public CreateQuarryModeratorCommandHandler(
            IQuarryModeratorRepository quarryModeratorRepository,
            QuarryModeratorBusinessRules businessRules,
            IMapper mapper)
        {
            _quarryModeratorRepository = quarryModeratorRepository;
            _businessRules = businessRules;
            _mapper = mapper;
        }

        public async Task<CreatedQuarryModeratorResponse> Handle(
            CreateQuarryModeratorCommand request, 
            CancellationToken cancellationToken)
        {
            await _businessRules.UserShouldExist(request.UserId);
            await _businessRules.QuarryShouldExist(request.QuarryId);
            await _businessRules.AssignmentShouldNotExist(request.UserId, request.QuarryId);

            QuarryModerator quarryModerator = _mapper.Map<QuarryModerator>(request);
            quarryModerator.CreatedDate = DateTime.UtcNow;

            await _quarryModeratorRepository.AddAsync(quarryModerator);

            CreatedQuarryModeratorResponse response = _mapper.Map<CreatedQuarryModeratorResponse>(quarryModerator);
            return response;
        }
    }
}
