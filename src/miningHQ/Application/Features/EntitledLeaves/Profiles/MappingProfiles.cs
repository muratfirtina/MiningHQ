using Application.Features.EntitledLeaves.Commands.Create;
using Application.Features.EntitledLeaves.Commands.Delete;
using Application.Features.EntitledLeaves.Commands.Update;
using Application.Features.EntitledLeaves.Dtos;
using Application.Features.EntitledLeaves.Queries.GetByEmployeeId;
using Application.Features.EntitledLeaves.Queries.GetById;
using Application.Features.EntitledLeaves.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.EntitledLeaves.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<EntitledLeave, CreateEntitledLeaveCommand>().ReverseMap();
        CreateMap<EntitledLeave, CreatedEntitledLeaveResponse>().ReverseMap();
        CreateMap<EntitledLeave, UpdateEntitledLeaveCommand>().ReverseMap();
        CreateMap<EntitledLeave, UpdatedEntitledLeaveResponse>().ReverseMap();
        CreateMap<EntitledLeave, DeleteEntitledLeaveCommand>().ReverseMap();
        CreateMap<EntitledLeave, DeletedEntitledLeaveResponse>().ReverseMap();
        CreateMap<EntitledLeave, GetByIdEntitledLeaveResponse>().ReverseMap();
        CreateMap<EntitledLeave, GetListEntitledLeaveListItemDto>().ReverseMap();
        CreateMap<List<EntitledLeave>, GetEntitledLeavesByEmployeeIdResponse>().ReverseMap();
        CreateMap<EmployeeEntitledLeaveDto, GetEntitledLeavesByEmployeeIdResponse>().ReverseMap();
        CreateMap<EntitledLeave, EmployeeEntitledLeaveDto>().ReverseMap();
        CreateMap<IPaginate<EntitledLeave>, GetListResponse<GetListEntitledLeaveListItemDto>>().ReverseMap();
    }
}