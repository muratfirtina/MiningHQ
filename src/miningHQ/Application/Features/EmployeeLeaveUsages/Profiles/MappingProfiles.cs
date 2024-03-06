using Application.Features.EmployeeLeaveUsages.Commands.Create;
using Application.Features.EmployeeLeaveUsages.Commands.Delete;
using Application.Features.EmployeeLeaveUsages.Commands.Update;
using Application.Features.EmployeeLeaveUsages.Queries.GetById;
using Application.Features.EmployeeLeaveUsages.Queries.GetList;
using Application.Features.EmployeeLeaveUsages.Queries.GetListByDynamic;
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
        CreateMap<EmployeeLeaveUsage, GetListEmployeeLeaveUsageListItemDto>()
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Employee.FirstName))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.Employee.LastName))
            .ReverseMap();
        CreateMap<IPaginate<EmployeeLeaveUsage>, GetListResponse<GetListEmployeeLeaveUsageListItemDto>>().ReverseMap();
        CreateMap<EmployeeLeaveUsage, GetListByDynamicEmployeeLeaveUsageListItemDto>().ReverseMap();
        
        CreateMap<IPaginate<EmployeeLeaveUsage>, GetListResponse<GetListByDynamicEmployeeLeaveUsageListItemDto>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();



    }
}