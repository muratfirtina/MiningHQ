using Application.Features.Employees.Queries.GetFilesByEmployeeId;
using Application.Services.Repositories;
using Application.Storage;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Employees.Constants.EmployeesOperationClaims;

namespace Application.Features.Employees.Queries.GetFilesByEmployeeId;

public class GetFilesByEmployeeIdQuery: IRequest<List<GetEmployeeFilesDto>>//,ISecuredRequest
{
    public string? EmployeeId { get; set; }
    public string? Path { get; set; }
    public string? Category { get; set; }
    
    public string[] Roles => new[] { Admin, Read };
    
    
    
    public class GetFilesByEmployeeIdQueryHandler : IRequestHandler<GetFilesByEmployeeIdQuery, List<GetEmployeeFilesDto>>
    {
        private readonly IStorageFactory _storageFactory;  // ⭐ IStorage yerine IStorageFactory
        private readonly IMapper _mapper;

        public GetFilesByEmployeeIdQueryHandler(IStorageFactory storageFactory, IMapper mapper)
        {
            _storageFactory = storageFactory;  // ⭐ Factory injection
            _mapper = mapper;
        }

        public async Task<List<GetEmployeeFilesDto>> Handle(GetFilesByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            // ⭐ Factory'den default storage service al
            var storageService = _storageFactory.GetDefaultStorageService();
        
            var files = await storageService.GetFiles<EmployeeFile>(request.EmployeeId);
            var filesDto = _mapper.Map<List<GetEmployeeFilesDto>>(files);
            return filesDto;
        }
    }
}
