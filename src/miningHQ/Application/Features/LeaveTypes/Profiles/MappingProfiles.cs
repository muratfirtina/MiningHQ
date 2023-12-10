using Application.Features.LeaveTypes.Commands.Create;
using Application.Features.LeaveTypes.Commands.Delete;
using Application.Features.LeaveTypes.Commands.Update;
using Application.Features.LeaveTypes.Queries.GetList;
using Application.Features.LeaveUsages.Queries.GetById;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.LeaveTypes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<LeaveType, CreateLeaveTypeCommand>().ReverseMap();
        CreateMap<LeaveType, CreatedLeaveTypeResponse>().ReverseMap();
        CreateMap<LeaveType, UpdateLeaveTypeCommand>().ReverseMap();
        CreateMap<LeaveType, UpdatedLeaveTypeResponse>().ReverseMap();
        CreateMap<LeaveType, DeleteLeaveTypeCommand>().ReverseMap();
        CreateMap<LeaveType, DeletedLeaveTypeResponse>().ReverseMap();
        CreateMap<LeaveType, GetByIdLeaveTypeResponse>().ReverseMap();
        CreateMap<LeaveType, GetListLeaveTypesListItemDto>().ReverseMap();
        CreateMap<IPaginate<LeaveType>, GetListResponse<GetListLeaveTypesListItemDto>>().ReverseMap();
    }
}