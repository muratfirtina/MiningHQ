using Application.Features.EmployeeLeaves.Constants;
using Application.Features.EmployeeLeaves.Constants;
using Application.Features.EmployeeLeaves.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.EmployeeLeaves.Constants.EmployeeLeavesOperationClaims;

namespace Application.Features.EmployeeLeaves.Commands.Delete;

public class DeleteEmployeeLeaveCommand : IRequest<DeletedEmployeeLeaveResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, EmployeeLeavesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new [] {"GetEmployeeLeaves"};

    public class DeleteEmployeeLeaveCommandHandler : IRequestHandler<DeleteEmployeeLeaveCommand, DeletedEmployeeLeaveResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeLeaveRepository _employeeLeaveRepository;
        private readonly EmployeeLeaveBusinessRules _employeeLeaveBusinessRules;

        public DeleteEmployeeLeaveCommandHandler(IMapper mapper, IEmployeeLeaveRepository employeeLeaveRepository,
                                         EmployeeLeaveBusinessRules employeeLeaveBusinessRules)
        {
            _mapper = mapper;
            _employeeLeaveRepository = employeeLeaveRepository;
            _employeeLeaveBusinessRules = employeeLeaveBusinessRules;
        }

        public async Task<DeletedEmployeeLeaveResponse> Handle(DeleteEmployeeLeaveCommand request, CancellationToken cancellationToken)
        {
            EmployeeLeave? employeeLeave = await _employeeLeaveRepository.GetAsync(predicate: el => el.Id == request.Id, cancellationToken: cancellationToken);
            await _employeeLeaveBusinessRules.EmployeeLeaveShouldExistWhenSelected(employeeLeave);

            await _employeeLeaveRepository.DeleteAsync(employeeLeave!);

            DeletedEmployeeLeaveResponse response = _mapper.Map<DeletedEmployeeLeaveResponse>(employeeLeave);
            return response;
        }
    }
}