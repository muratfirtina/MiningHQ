using Application.Features.Employees.Queries.GetFilesByEmployeeId;
using Application.Services.Repositories;
using Application.Storage;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Employees.Constants.EmployeesOperationClaims;

namespace Application.Features.Employees.Queries.GetFilesByEmployeeId;

public class GetFilesByEmployeeIdQuery: IRequest<List<GetEmployeeFilesDto>>//,ISecuredRequest
{
    public string? EmployeeId { get; set; }
    
    public string[] Roles => new[] { Admin, Read };
    
    
    public class GetImagesByEmployeeIdQueryHandler : IRequestHandler<GetFilesByEmployeeIdQuery, List<GetEmployeeFilesDto>>
    {
        private readonly IStorage _storage;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public GetImagesByEmployeeIdQueryHandler(IStorage storage, IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _storage = storage;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<List<GetEmployeeFilesDto>> Handle(GetFilesByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var employeeFiles = await _employeeRepository.GetFilesByEmployeeId(request.EmployeeId);
            return _mapper.Map<List<GetEmployeeFilesDto>>(employeeFiles);
            
        }
    }
}
