using Application.Features.Machines.Commands.Create;
using Application.Features.Machines.Commands.Delete;
using Application.Features.Machines.Commands.Update;
using Application.Features.Machines.Queries.GetById;
using Application.Features.Machines.Queries.GetList;
using Application.Features.Machines.Queries.GetListDynamic;
using Application.Features.Machines.Queries.GetFilesByMachineId;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Machines.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Machine, CreateMachineCommand>().ReverseMap();
        CreateMap<Machine, CreatedMachineResponse>().ReverseMap();
        CreateMap<Machine, UpdateMachineCommand>().ReverseMap();
        CreateMap<Machine, UpdatedMachineResponse>().ReverseMap();
        CreateMap<Machine, DeleteMachineCommand>().ReverseMap();
        CreateMap<Machine, DeletedMachineResponse>().ReverseMap();
        CreateMap<Machine, GetByIdMachineResponse>().ReverseMap();
        CreateMap<Machine, GetListMachineListItemDto>()
        .ForMember(d => d.BrandName, opt => opt.MapFrom(s => s.Model.Brand.Name))
        .ForMember(d => d.ModelName, opt => opt.MapFrom(s => s.Model.Name))
        .ForMember(d => d.QuarryName, opt => opt.MapFrom(s => s.Quarry.Name))
        .ForMember(d => d.MachineTypeName, opt => opt.MapFrom(s => s.MachineType.Name))
        .ReverseMap();
        CreateMap<Machine, GetListResponse<GetListDynamicDto>>().ReverseMap();
        CreateMap<IPaginate<Machine>, GetListResponse<GetListMachineListItemDto>>().ReverseMap();
        
        // Machine Files Mappings
        CreateMap<MachineFile, GetMachineFilesDto>()
            .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Path))
            .ReverseMap();
    }
}