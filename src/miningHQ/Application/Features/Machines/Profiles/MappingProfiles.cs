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
        CreateMap<Machine, CreatedMachineResponse>()
            .ForMember(d => d.BrandName, opt => opt.MapFrom(s => s.Model.Brand.Name))
            .ForMember(d => d.ModelName, opt => opt.MapFrom(s => s.Model.Name))
            .ForMember(d => d.BrandId, opt => opt.MapFrom(s => s.Model.BrandId.ToString()))
            .ForMember(d => d.QuarryName, opt => opt.MapFrom(s => s.Quarry.Name))
            .ForMember(d => d.MachineTypeName, opt => opt.MapFrom(s => s.MachineType.Name))
            .ReverseMap();
        CreateMap<Machine, UpdateMachineCommand>().ReverseMap();
        CreateMap<Machine, UpdatedMachineResponse>()
            .ForMember(d => d.BrandName, opt => opt.MapFrom(s => s.Model.Brand.Name))
            .ForMember(d => d.ModelName, opt => opt.MapFrom(s => s.Model.Name))
            .ForMember(d => d.BrandId, opt => opt.MapFrom(s => s.Model.BrandId))
            .ForMember(d => d.QuarryName, opt => opt.MapFrom(s => s.Quarry.Name))
            .ForMember(d => d.MachineTypeName, opt => opt.MapFrom(s => s.MachineType.Name))
            .ForMember(d => d.CurrentOperatorName, opt => opt.MapFrom(s => s.CurrentOperator != null ? s.CurrentOperator.FirstName + " " + s.CurrentOperator.LastName : null))
            .ReverseMap();
        CreateMap<Machine, DeleteMachineCommand>().ReverseMap();
        CreateMap<Machine, DeletedMachineResponse>().ReverseMap();
        CreateMap<Machine, GetByIdMachineResponse>()
            .ForMember(d => d.BrandName, opt => opt.MapFrom(s => s.Model.Brand.Name))
            .ForMember(d => d.ModelName, opt => opt.MapFrom(s => s.Model.Name))
            .ForMember(d => d.BrandId, opt => opt.MapFrom(s => s.Model.BrandId))
            .ForMember(d => d.QuarryName, opt => opt.MapFrom(s => s.Quarry.Name))
            .ForMember(d => d.MachineTypeName, opt => opt.MapFrom(s => s.MachineType.Name))
            .ForMember(d => d.CurrentOperatorName, opt => opt.MapFrom(s => s.CurrentOperator != null ? s.CurrentOperator.FirstName + " " + s.CurrentOperator.LastName : null))
            .ReverseMap();
        CreateMap<Machine, GetListMachineListItemDto>()
        .ForMember(d => d.BrandName, opt => opt.MapFrom(s => s.Model.Brand.Name))
        .ForMember(d => d.ModelName, opt => opt.MapFrom(s => s.Model.Name))
        .ForMember(d => d.QuarryName, opt => opt.MapFrom(s => s.Quarry.Name))
        .ForMember(d => d.MachineTypeName, opt => opt.MapFrom(s => s.MachineType.Name))
        .ForMember(d => d.BrandId, opt => opt.MapFrom(s => s.Model.BrandId))
        .ForMember(d => d.ModelId, opt => opt.MapFrom(s => s.ModelId))
        .ForMember(d => d.MachineTypeId, opt => opt.MapFrom(s => s.MachineTypeId))
        .ForMember(d => d.QuarryId, opt => opt.MapFrom(s => s.QuarryId))
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