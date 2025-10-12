using Application.Features.Maintenances.Commands.Create;
using Application.Features.Maintenances.Commands.Delete;
using Application.Features.Maintenances.Commands.Update;
using Application.Features.Maintenances.Queries.GetById;
using Application.Features.Maintenances.Queries.GetList;
using Application.Features.Maintenances.Queries.GetListByMachineId;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Maintenances.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Maintenance, CreateMaintenanceCommand>().ReverseMap();
        CreateMap<Maintenance, CreatedMaintenanceResponse>().ReverseMap();
        CreateMap<Maintenance, UpdateMaintenanceCommand>().ReverseMap();
        CreateMap<Maintenance, UpdatedMaintenanceResponse>().ReverseMap();
        CreateMap<Maintenance, DeleteMaintenanceCommand>().ReverseMap();
        CreateMap<Maintenance, DeletedMaintenanceResponse>().ReverseMap();
        
        // GetById mapping with MaintenanceFile DTO
        CreateMap<Maintenance, GetByIdMaintenanceResponse>()
            .ForMember(dest => dest.MachineName, opt => opt.MapFrom(src => src.Machine.Name))
            .ForMember(dest => dest.MaintenanceTypeName, opt => opt.MapFrom(src => src.MaintenanceType.Name))
            .ForMember(dest => dest.MaintenanceFiles, opt => opt.MapFrom(src => src.MaintenanceFiles))
            .ReverseMap();
        
        // MaintenanceFile to DTO mapping
        CreateMap<MaintenanceFile, MaintenanceFileDto>().ReverseMap();
        
        CreateMap<Maintenance, GetListMaintenanceListItemDto>()
            .ForMember(dest => dest.MachineName, opt => opt.MapFrom(src => src.Machine.Name))
            .ForMember(dest => dest.MaintenanceTypeName, opt => opt.MapFrom(src => src.MaintenanceType.Name))
            .ForMember(dest => dest.FileCount, opt => opt.MapFrom(src => src.MaintenanceFiles.Count))
            .ReverseMap();
        CreateMap<Maintenance, GetListMaintenanceByMachineIdListItemDto>()
            .ForMember(dest => dest.MachineName, opt => opt.MapFrom(src => src.Machine.Name))
            .ForMember(dest => dest.MaintenanceTypeName, opt => opt.MapFrom(src => src.MaintenanceType.Name))
            .ForMember(dest => dest.FileCount, opt => opt.MapFrom(src => src.MaintenanceFiles.Count))
            .ReverseMap();
        CreateMap<IPaginate<Maintenance>, GetListResponse<GetListMaintenanceListItemDto>>().ReverseMap();
        CreateMap<IPaginate<Maintenance>, GetListResponse<GetListMaintenanceByMachineIdListItemDto>>().ReverseMap();
    }
}
