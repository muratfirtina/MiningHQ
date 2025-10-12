using Application.Features.Maintenances.Constants;
using Application.Services.Repositories;
using Application.Storage;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using static Application.Features.Maintenances.Constants.MaintenancesOperationClaims;

namespace Application.Features.Maintenances.Commands.UploadMaintenanceFile;

public class UploadMaintenanceFileCommand : IRequest<UploadMaintenanceFileDto>
{
    public string Category { get; set; }
    public string FolderPath { get; set; }
    public List<IFormFile> Files { get; set; }
    public string MaintenanceId { get; set; }

    public string[] Roles => new[] { Admin, Write, MaintenancesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetMaintenances" };

    public class UploadMaintenanceFileCommandHandler : IRequestHandler<UploadMaintenanceFileCommand, UploadMaintenanceFileDto>
    {
        private readonly IStorageFactory _storageFactory;
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public UploadMaintenanceFileCommandHandler(
            IStorageFactory storageFactory, 
            IMaintenanceRepository maintenanceRepository,
            IFileRepository fileRepository,
            IMapper mapper)
        {
            _storageFactory = storageFactory;
            _maintenanceRepository = maintenanceRepository;
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<UploadMaintenanceFileDto> Handle(UploadMaintenanceFileCommand request,
            CancellationToken cancellationToken)
        {
            // Maintenance validation
            Maintenance? maintenance = await _maintenanceRepository.GetAsync(
                m => m.Id == Guid.Parse(request.MaintenanceId), 
                cancellationToken: cancellationToken);
            
            if (maintenance == null)
            {
                throw new NotFoundException("Maintenance not found");
            }

            // File size validation
            foreach (IFormFile file in request.Files)
            {
                if (file.Length > 10 * 1024 * 1024) // 10MB limit for maintenance files
                {
                    throw new BusinessException("File size must be less than 10MB");
                }
            }

            // Get default storage service
            var storageService = _storageFactory.GetDefaultStorageService();

            // Upload files
            List<(string fileName, string path, string category, string storageType)> result =
                await storageService.UploadAsync(request.Category, request.FolderPath, request.Files);

            // Save to database - EmployeeFile pattern'ini takip ediyoruz
            await _fileRepository.AddAsync(result.Select(r => new MaintenanceFile()
            {
                Name = r.fileName,
                Path = r.path,
                Category = r.category,
                Storage = r.storageType,
                Maintenances = new List<Maintenance>() { maintenance }
            }).ToList());

            return new UploadMaintenanceFileDto();
        }
    }
}
