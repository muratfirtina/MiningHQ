using Application.Services.Repositories;
using Application.Storage;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Machines.Constants.MachinesOperationClaims;

namespace Application.Features.Machines.Queries.GetFilesByMachineId;

public class GetFilesByMachineIdQuery : IRequest<List<GetMachineFilesDto>>//, ISecuredRequest
{
    public string? MachineId { get; set; }
    public string? Path { get; set; }
    public string? Category { get; set; }
    
    public string[] Roles => new[] { Admin, Read };
    
    public class GetFilesByMachineIdQueryHandler : IRequestHandler<GetFilesByMachineIdQuery, List<GetMachineFilesDto>>
    {
        private readonly IStorageFactory _storageFactory;
        private readonly IMapper _mapper;

        public GetFilesByMachineIdQueryHandler(IStorageFactory storageFactory, IMapper mapper)
        {
            _storageFactory = storageFactory;
            _mapper = mapper;
        }

        public async Task<List<GetMachineFilesDto>> Handle(GetFilesByMachineIdQuery request, CancellationToken cancellationToken)
        {
            // Get default storage service
            var storageService = _storageFactory.GetDefaultStorageService();
        
            var files = await storageService.GetFiles<MachineFile>(request.MachineId);
            var filesDto = _mapper.Map<List<GetMachineFilesDto>>(files);
            return filesDto;
        }
    }
}
