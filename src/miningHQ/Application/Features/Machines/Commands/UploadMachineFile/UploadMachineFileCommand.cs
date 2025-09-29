using Application.Features.Machines.Constants;
using Application.Services.Repositories;
using Application.Storage;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using static Application.Features.Machines.Constants.MachinesOperationClaims;

namespace Application.Features.Machines.Commands.UploadMachineFile;

public class UploadMachineFileCommand : IRequest<UploadMachineFileDto> //, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Category { get; set; }
    public string FolderPath { get; set; }
    public List<IFormFile> Files { get; set; }
    public string MachineId { get; set; }

    public string[] Roles => new[] { Admin, Write, MachinesOperationClaims.UploadMachineFile };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { "GetMachines" };

    public class UploadMachineFileCommandHandler : IRequestHandler<UploadMachineFileCommand, UploadMachineFileDto>
    {
        private readonly IStorageFactory _storageFactory;
        private readonly IFileRepository _fileRepository;
        private readonly IMachineRepository _machineRepository;
        private readonly IMapper _mapper;

        public UploadMachineFileCommandHandler(IStorageFactory storageFactory, IMachineRepository machineRepository,
            IMapper mapper, IFileRepository fileRepository)
        {
            _storageFactory = storageFactory;
            _machineRepository = machineRepository;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task<UploadMachineFileDto> Handle(UploadMachineFileCommand request,
            CancellationToken cancellationToken)
        {
            // Machine validation
            Machine? machine = await _machineRepository.GetAsync(m => m.Id == Guid.Parse(request.MachineId));
            if (machine == null)
            {
                throw new NotFoundException("Machine not found");
            }

            // File size validation
            foreach (IFormFile file in request.Files)
            {
                if (file.Length > 5 * 1024 * 1024)
                {
                    throw new BusinessException("File size must be less than 5MB");
                }
            }

            // Get default storage service
            var storageService = _storageFactory.GetDefaultStorageService();

            // Upload files
            List<(string fileName, string path, string category, string storageType)> result =
                await storageService.UploadAsync(request.Category, request.FolderPath, request.Files);

            // Save to database
            await _fileRepository.AddAsync(result.Select(r => new MachineFile()
            {
                Name = r.fileName,
                Path = r.path,
                Category = r.category,
                Storage = r.storageType,
                Machines = new List<Machine>() { machine }
            }).ToList());

            return new UploadMachineFileDto();
        }
    }
}
