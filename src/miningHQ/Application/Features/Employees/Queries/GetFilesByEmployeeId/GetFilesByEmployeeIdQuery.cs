using Application.Features.Employees.Queries.GetFilesByEmployeeId;
using Application.Services.Repositories;
using Application.Storage;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Employees.Constants.EmployeesOperationClaims;

namespace Application.Features.Employees.Queries.GetFilesByEmployeeId;

public class GetFilesByEmployeeIdQuery: IRequest<List<GetFilesByEmployeeIdResponse>>//,ISecuredRequest
{
    public string? EmployeeId { get; set; }
    
    public string[] Roles => new[] { Admin, Read };
    
    
    public class GetImagesByEmployeeIdQueryHandler : IRequestHandler<GetFilesByEmployeeIdQuery, List<GetFilesByEmployeeIdResponse>>
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

        public async Task<List<GetFilesByEmployeeIdResponse>> Handle(GetFilesByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            //employeeId'ye ait resimleri getir
            if (request.EmployeeId != null)
            {
                await _employeeRepository.GetFilesByEmployeeId(request.EmployeeId);
                
            }
            else
            {
                throw new Exception("EmployeeId is null");
            }

            return new List<GetFilesByEmployeeIdResponse>();
            
        }
    }
}
