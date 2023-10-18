using Application.Features.MachineTypes.Commands.Create;
using Application.Features.MachineTypes.Commands.Delete;
using Application.Features.MachineTypes.Commands.Update;
using Application.Features.MachineTypes.Queries.GetById;
using Application.Features.MachineTypes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.MachineTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<MachineType, CreateMachineTypeCommand>().ReverseMap();
        CreateMap<MachineType, CreatedMachineTypeResponse>().ReverseMap();
        CreateMap<MachineType, UpdateMachineTypeCommand>().ReverseMap();
        CreateMap<MachineType, UpdatedMachineTypeResponse>().ReverseMap();
        CreateMap<MachineType, DeleteMachineTypeCommand>().ReverseMap();
        CreateMap<MachineType, DeletedMachineTypeResponse>().ReverseMap();
        CreateMap<MachineType, GetByIdMachineTypeResponse>().ReverseMap();
        CreateMap<MachineType, GetListMachineTypeListItemDto>().ReverseMap();
        CreateMap<IPaginate<MachineType>, GetListResponse<GetListMachineTypeListItemDto>>().ReverseMap();
    }
}