using Application.Features.Employees.Constants;
using Application.Services.Repositories;
using Application.Storage;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using static Application.Features.Employees.Constants.EmployeesOperationClaims;

namespace Application.Features.Employees.Commands.UploadEmployeeFile;

public class
    UploadEmployeeFileCommand : IRequest<UploadEmployeeFileDto> //, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Category { get; set; }
    public string FolderPath { get; set; }
    public List<IFormFile> Files { get; set; }
    public string EmployeeId { get; set; }

    public string[] Roles => new[] { Admin, Write, EmployeesOperationClaims.UploadEmployeeFile };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetEmployees" };

    public class UploadEmployeeFileCommandHandler : IRequestHandler<UploadEmployeeFileCommand, UploadEmployeeFileDto>
    {
        private readonly IStorageFactory _storageFactory; // ⭐ IStorageService yerine IStorageFactory
        private readonly IFileRepository _fileRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UploadEmployeeFileCommandHandler(IStorageFactory storageFactory, IEmployeeRepository employeeRepository,
            IMapper mapper, IFileRepository fileRepository)
        {
            _storageFactory = storageFactory; // ⭐ Factory injection
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task<UploadEmployeeFileDto> Handle(UploadEmployeeFileCommand request,
            CancellationToken cancellationToken)
        {
            // Employee validation
            Employee? employee = await _employeeRepository.GetAsync(e => e.Id == Guid.Parse(request.EmployeeId));
            if (employee == null)
            {
                throw new NotFoundException("Employee not found");
            }

            // File size validation
            foreach (IFormFile file in request.Files)
            {
                if (file.Length > 5 * 1024 * 1024)
                {
                    throw new BusinessException("File size must be less than 5MB");
                }
            }

            // ⭐ Factory'den default storage service al
            var storageService = _storageFactory.GetDefaultStorageService();

            // Upload files
            List<(string fileName, string path, string category, string storageType)> result =
                await storageService.UploadAsync(request.Category, request.FolderPath, request.Files);

            // Save to database
            await _fileRepository.AddAsync(result.Select(r => new EmployeeFile()
            {
                Name = r.fileName,
                Path = r.path,
                Category = r.category,
                Storage = r.storageType,
                Employees = new List<Employee>() { employee }
            }).ToList());

            return new UploadEmployeeFileDto();
        }
    }
}