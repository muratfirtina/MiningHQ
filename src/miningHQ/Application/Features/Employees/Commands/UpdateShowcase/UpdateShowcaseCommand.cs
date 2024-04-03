using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Employees.Commands.UpdateShowcase;

public class UpdateShowcaseCommand : IRequest<UpdateShowcaseResponse>
{
    public string? EmployeeId { get; set; }
    public string? FileId { get; set; }
    public bool Showcase { get; set; }

    public class UpdateShowcaseCommandHandler : IRequestHandler<UpdateShowcaseCommand, UpdateShowcaseResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateShowcaseCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<UpdateShowcaseResponse> Handle(UpdateShowcaseCommand request, CancellationToken cancellationToken)
        {
            if (request.EmployeeId != null && request.FileId != null)
            {
                await _employeeRepository.ChangeShowcase(request.EmployeeId, request.FileId, request.Showcase);
            }
            else
            {
                throw new Exception("EmployeeId or FileId is null");
            }
            
            
            var response = _mapper.Map<UpdateShowcaseResponse>(request);
            return response;
        }
    }
    
}