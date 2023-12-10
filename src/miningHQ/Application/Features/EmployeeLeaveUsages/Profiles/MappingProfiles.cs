using Application.Features.EmployeeLeaveUsages.Commands.Create;
using Application.Features.EmployeeLeaveUsages.Commands.Delete;
using Application.Features.EmployeeLeaveUsages.Commands.Update;
using Application.Features.EmployeeLeaveUsages.Queries.GetById;
using Application.Features.EmployeeLeaveUsages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.EmployeeLeaveUsages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<EmployeeLeaveUsage, CreateEmployeeLeaveUsageCommand>().ReverseMap();
        CreateMap<EmployeeLeaveUsage, CreatedEmployeeLeaveUsageResponse>().ReverseMap();
        CreateMap<EmployeeLeaveUsage, UpdateEmployeeLeaveUsageCommand>().ReverseMap();
        CreateMap<EmployeeLeaveUsage, UpdatedEmployeeLeaveUsageResponse>().ReverseMap();
        CreateMap<EmployeeLeaveUsage, DeleteEmployeeLeaveUsageCommand>().ReverseMap();
        CreateMap<EmployeeLeaveUsage, DeletedEmployeeLeaveUsageResponse>().ReverseMap();
        CreateMap<EmployeeLeaveUsage, GetByIdEmployeeLeaveUsageResponse>().ReverseMap();
        CreateMap<EmployeeLeaveUsage, GetListEmployeeLeaveUsageListItemDto>().ReverseMap();
        CreateMap<IPaginate<EmployeeLeaveUsage>, GetListResponse<GetListEmployeeLeaveUsageListItemDto>>().ReverseMap();
    }
}