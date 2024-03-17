using Application.Features.Departments.Commands.Create;
using Application.Features.Departments.Commands.Delete;
using Application.Features.Departments.Commands.Update;
using Application.Features.Departments.Queries.GetById;
using Application.Features.Departments.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Departments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Department, CreateDepartmentCommand>().ReverseMap();
        CreateMap<Department, CreatedDepartmentResponse>().ReverseMap();
        CreateMap<Department, UpdateDepartmentCommand>().ReverseMap();
        CreateMap<Department, UpdatedDepartmentResponse>().ReverseMap();
        CreateMap<Department, DeleteDepartmentCommand>().ReverseMap();
        CreateMap<Department, DeletedDepartmentResponse>().ReverseMap();
        CreateMap<Department, GetByIdDepartmentResponse>().ReverseMap();
        CreateMap<Department, GetListDepartmentListItemDto>().ReverseMap();
        CreateMap<IPaginate<Department>, GetListResponse<GetListDepartmentListItemDto>>().ReverseMap();
    }
}