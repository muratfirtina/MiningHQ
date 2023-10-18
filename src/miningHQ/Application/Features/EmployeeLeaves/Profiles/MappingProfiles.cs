using Application.Features.EmployeeLeaves.Commands.Create;
using Application.Features.EmployeeLeaves.Commands.Delete;
using Application.Features.EmployeeLeaves.Commands.Update;
using Application.Features.EmployeeLeaves.Queries.GetById;
using Application.Features.EmployeeLeaves.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.EmployeeLeaves.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<EmployeeLeave, CreateEmployeeLeaveCommand>().ReverseMap();
        CreateMap<EmployeeLeave, CreatedEmployeeLeaveResponse>().ReverseMap();
        CreateMap<EmployeeLeave, UpdateEmployeeLeaveCommand>().ReverseMap();
        CreateMap<EmployeeLeave, UpdatedEmployeeLeaveResponse>().ReverseMap();
        CreateMap<EmployeeLeave, DeleteEmployeeLeaveCommand>().ReverseMap();
        CreateMap<EmployeeLeave, DeletedEmployeeLeaveResponse>().ReverseMap();
        CreateMap<EmployeeLeave, GetByIdEmployeeLeaveResponse>().ReverseMap();
        CreateMap<EmployeeLeave, GetListEmployeeLeaveListItemDto>().ReverseMap();
        CreateMap<IPaginate<EmployeeLeave>, GetListResponse<GetListEmployeeLeaveListItemDto>>().ReverseMap();
    }
}