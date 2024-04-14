using Application.Features.Employees.Constants;
using Application.Services.Repositories;
using Application.Storage;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using static Application.Features.Employees.Constants.EmployeesOperationClaims;
using File = Domain.Entities.File;

namespace Application.Features.Employees.Commands.UploadEmployeeFile;

public class UploadEmployeeFileCommand : IRequest<UploadEmployeeFileDto>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Category { get; set; }
    public string Path { get; set; }
    public IFormFileCollection Files { get; set; }
    public string EmployeeId { get; set; } 
    
    public string[] Roles => new[] { Admin, Write, EmployeesOperationClaims.UploadEmployeeFile };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetEmployees"};

    public class UploadEmployeeFileCommandHandler : IRequestHandler<UploadEmployeeFileCommand, UploadEmployeeFileDto>
    {
        private readonly IStorageService _storageService;
        private readonly IStorage _storage;
        private readonly IFileRepository _fileRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
    

        public UploadEmployeeFileCommandHandler(IStorageService storageService, IEmployeeRepository employeeRepository, IMapper mapper, IFileRepository fileRepository, IStorage storage)
        {
        _storageService = storageService;
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _fileRepository = fileRepository;
        _storage = storage;
        }

        public async Task<UploadEmployeeFileDto> Handle(UploadEmployeeFileCommand request, CancellationToken cancellationToken)
        {
            List<(string fileName, string path, string containerName)> result = await _storageService.UploadAsync(request.Category, request.Path, request.Files);
            Employee? employee = await _employeeRepository.GetAsync(e=>e.Id == Guid.Parse(request.EmployeeId));
            if (employee == null)
            {
                throw new NotFoundException("Employee not found");
            }
            await _fileRepository.AddAsync(result.Select(r => new EmployeeFile()
            {
                Name = r.fileName,
                Category = r.containerName,
                Path = r.path,
                Storage = _storageService.StorageName,
                Employees = new List<Employee>() {employee}
            }).ToList());
            return new UploadEmployeeFileDto();
        }
        
    }

}

