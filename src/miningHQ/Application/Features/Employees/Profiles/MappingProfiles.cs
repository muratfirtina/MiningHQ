using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Commands.Delete;
using Application.Features.Employees.Commands.Update;
using Application.Features.Employees.Dtos;
using Application.Features.Employees.Queries.GetById;
using Application.Features.Employees.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Employees.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Employee, CreateEmployeeCommand>().ReverseMap();
        CreateMap<Employee, CreatedEmployeeResponse>().ReverseMap();
        CreateMap<Employee, UpdateEmployeeCommand>().ReverseMap();
        CreateMap<Employee, UpdatedEmployeeResponse>().ReverseMap();
        CreateMap<Employee, DeleteEmployeeCommand>().ReverseMap();
        CreateMap<Employee, DeletedEmployeeResponse>().ReverseMap();
        CreateMap<Employee, GetByIdEmployeeResponse>()
            .ForMember(dest => dest.TotalUsedLeaveDays, opt => opt.MapFrom(src => src.EmployeeLeaveUsages.Count))
            .ForMember(dest => dest.TotalEntitledLeaveDays, opt => opt.MapFrom(src => src.EmployeeLeaveUsages.Count))
            .ReverseMap();
        CreateMap<Employee, GetListEmployeeListItemDto>().ReverseMap();
        
        CreateMap<Employee, GetListEmployeeListItemDto>()
            .ForMember(dest => dest.Timekeepings, opt => opt.MapFrom(src => src.Timekeepings))
            .ReverseMap();
        
        CreateMap<Timekeeping, TimekeepingDto>().ReverseMap();

        
        CreateMap<IPaginate<Employee>, GetListResponse<GetListEmployeeListItemDto>>().ReverseMap();
    }
}