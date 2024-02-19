using Application.Features.Employees.Dtos;
using Application.Features.Timekeepings.Commands.Create;
using Application.Features.Timekeepings.Commands.Delete;
using Application.Features.Timekeepings.Commands.Update;
using Application.Features.Timekeepings.Queries.GetById;
using Application.Features.Timekeepings.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Timekeepings.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Timekeeping, CreateTimekeepingCommand>().ReverseMap();
        CreateMap<Timekeeping, CreatedTimekeepingResponse>().ReverseMap();
        CreateMap<Timekeeping, UpdateTimekeepingCommand>().ReverseMap();
        CreateMap<Timekeeping, UpdatedTimekeepingResponse>().ReverseMap();
        CreateMap<Timekeeping, DeleteTimekeepingCommand>().ReverseMap();
        CreateMap<Timekeeping, DeletedTimekeepingResponse>().ReverseMap();
        CreateMap<Timekeeping, GetByIdTimekeepingResponse>().ReverseMap();
        CreateMap<Timekeeping, GetListTimekeepingListItemDto>()
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Employee.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Employee.LastName))

            .ForMember(d => d.HireDate, opt => opt.MapFrom(s => s.Employee.HireDate))

            .ReverseMap();
        CreateMap<IPaginate<Timekeeping>, GetListResponse<GetListTimekeepingListItemDto>>()
            .ForMember(d =>d.Items, opt => opt.MapFrom(s => s.Items))
            .ReverseMap();

        CreateMap<Timekeeping, GetListTimekeepingListItemDto>()
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Employee.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Employee.LastName))
            .ReverseMap();
        
        

        CreateMap<EmployeeWithTimekeepingsDto, GetListTimekeepingListItemDto>()
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))

            .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate))

            .ForMember(dest => dest.Timekeepings, opt => opt.MapFrom(src => src.Timekeepings));
        
        CreateMap<Timekeeping, TimekeepingDto>();
        
    }
}