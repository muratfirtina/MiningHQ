using Application.Services.Repositories;
using Application.Storage;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Maintenances.Constants.MaintenancesOperationClaims;

namespace Application.Features.Maintenances.Queries.GetFilesByMaintenanceId;

public class GetFilesByMaintenanceIdQuery : IRequest<List<GetMaintenanceFilesDto>>
{
    public string? MaintenanceId { get; set; }
    public string? Path { get; set; }
    public string? Category { get; set; }
    
    public string[] Roles => new[] { Admin, Read };
    
    public class GetFilesByMaintenanceIdQueryHandler : IRequestHandler<GetFilesByMaintenanceIdQuery, List<GetMaintenanceFilesDto>>
    {
        private readonly IStorageFactory _storageFactory;
        private readonly IMapper _mapper;

        public GetFilesByMaintenanceIdQueryHandler(IStorageFactory storageFactory, IMapper mapper)
        {
            _storageFactory = storageFactory;
            _mapper = mapper;
        }

        public async Task<List<GetMaintenanceFilesDto>> Handle(GetFilesByMaintenanceIdQuery request, CancellationToken cancellationToken)
        {
            // Get default storage service from factory
            var storageService = _storageFactory.GetDefaultStorageService();
        
            var files = await storageService.GetFiles<MaintenanceFile>(request.MaintenanceId);
            var filesDto = _mapper.Map<List<GetMaintenanceFilesDto>>(files);
            return filesDto;
        }
    }
}
