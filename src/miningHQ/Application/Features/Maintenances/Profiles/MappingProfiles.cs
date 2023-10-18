using Application.Features.Maintenances.Commands.Create;
using Application.Features.Maintenances.Commands.Delete;
using Application.Features.Maintenances.Commands.Update;
using Application.Features.Maintenances.Queries.GetById;
using Application.Features.Maintenances.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Maintenances.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Maintenance, CreateMaintenanceCommand>().ReverseMap();
        CreateMap<Maintenance, CreatedMaintenanceResponse>().ReverseMap();
        CreateMap<Maintenance, UpdateMaintenanceCommand>().ReverseMap();
        CreateMap<Maintenance, UpdatedMaintenanceResponse>().ReverseMap();
        CreateMap<Maintenance, DeleteMaintenanceCommand>().ReverseMap();
        CreateMap<Maintenance, DeletedMaintenanceResponse>().ReverseMap();
        CreateMap<Maintenance, GetByIdMaintenanceResponse>().ReverseMap();
        CreateMap<Maintenance, GetListMaintenanceListItemDto>().ReverseMap();
        CreateMap<IPaginate<Maintenance>, GetListResponse<GetListMaintenanceListItemDto>>().ReverseMap();
    }
}