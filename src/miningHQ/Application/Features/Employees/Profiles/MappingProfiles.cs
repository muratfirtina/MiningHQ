using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Commands.Delete;
using Application.Features.Employees.Commands.Update;
using Application.Features.Employees.Commands.UpdateShowcase;
using Application.Features.Employees.Dtos;
using Application.Features.Employees.Queries.GetById;
using Application.Features.Employees.Queries.GetList;
using Application.Features.Employees.Queries.GetList.ShortDetail;
using Application.Features.Employees.Queries.GetListByDynamic;
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
        
        CreateMap<IPaginate<Employee>, GetListResponse<GetListByDynamicEmployeeListItemDto>>().ReverseMap();

        CreateMap<Employee, GetListByDynamicEmployeeListItemDto>()
            
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.JobName, opt => opt.MapFrom(src => src.Job.Name))
            .ForMember(dest => dest.QuarryName, opt => opt.MapFrom(src => src.Quarry.Name))
            .ForMember(dest => dest.TypeOfBlood, opt => opt.MapFrom(src => src.TypeOfBlood))
            .ForMember(dest => dest.EmergencyContact, opt => opt.MapFrom(src => src.EmergencyContact))
            .ReverseMap();
            ;

        CreateMap<IPaginate<Employee>, GetListResponse<GetListByEmplooyeeShortDetailItemDto>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();

        CreateMap<Employee, GetListByEmplooyeeShortDetailItemDto>()
            .ReverseMap();

        CreateMap<UpdateShowcaseCommand, UpdateShowcaseResponse>()
            .ReverseMap();

    }
}