using Application.Features.EmployeeLeaveUsages.Queries.GetListByDynamic;
using Application.Features.EntitledLeaves.Commands.Create;
using Application.Features.EntitledLeaves.Commands.Delete;
using Application.Features.EntitledLeaves.Commands.Update;
using Application.Features.EntitledLeaves.Dtos;
using Application.Features.EntitledLeaves.Queries.GetByEmployeeId;
using Application.Features.EntitledLeaves.Queries.GetById;
using Application.Features.EntitledLeaves.Queries.GetList;
using Application.Features.EntitledLeaves.Queries.GetListByDynamic;
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
        CreateMap<List<EntitledLeave>, GetEmployeeEntitledLeaveDto>().ReverseMap();
        CreateMap<EntitledLeave, GetEmployeeEntitledLeaveDto>().ReverseMap();
        CreateMap<IPaginate<EntitledLeave>, GetListResponse<GetEmployeeEntitledLeaveDto>>().ReverseMap();
        CreateMap<IPaginate<EntitledLeave>, GetListResponse<GetListEntitledLeaveListItemDto>>().ReverseMap();
        CreateMap<IPaginate<EntitledLeave>, GetListResponse<GetListByDynamicEntitledLeavesListItemDto>>()
            .ForMember(d => d.Items, opt => opt.MapFrom(s => s.Items))
            .ReverseMap();
        CreateMap<EntitledLeave, GetListByDynamicEntitledLeavesListItemDto>()
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee.Id))
            .ForMember(d => d.LeaveTypeId, opt => opt.MapFrom(s => s.LeaveType.Id))
            .ForMember(d => d.LeaveTypeName, opt => opt.MapFrom(s => s.LeaveType.Name))
            .ReverseMap();
        /*CreateMap<EntitledLeave, GetListByDynamicEmployeeLeaveUsageListItemDto>()
            .ForMember(e=>e.EmployeeId, opt=>opt.MapFrom(s=>s.Employee.Id))
            .ForMember(e=>e.FirstName, opt=>opt.MapFrom(s=>s.Employee.FirstName))
            .ForMember(e=>e.LastName, opt=>opt.MapFrom(s=>s.Employee.LastName))
            .ForMember(e=>e.LeaveTypeId, opt=>opt.MapFrom(s=>s.LeaveType.Id))
            .ForMember(e=>e.LeaveTypeName, opt=>opt.MapFrom(s=>s.LeaveType.Name))
            .ReverseMap();
        CreateMap<IPaginate<EntitledLeave>, GetListResponse<GetListByDynamicEmployeeLeaveUsageListItemDto>>()
            .ForMember(d => d.Items, opt => opt.MapFrom(s => s.Items))
            .ReverseMap();*/


    }
}