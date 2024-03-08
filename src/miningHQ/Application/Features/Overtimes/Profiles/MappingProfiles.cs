using Application.Features.Overtimes.Commands.Create;
using Application.Features.Overtimes.Commands.Delete;
using Application.Features.Overtimes.Commands.Update;
using Application.Features.Overtimes.Queries.GetById;
using Application.Features.Overtimes.Queries.GetList;
using Application.Features.Overtimes.Queries.GetOvertimeListByEmployeeId;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Overtimes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Overtime, CreateOvertimeCommand>().ReverseMap();
        CreateMap<Overtime, CreatedOvertimeResponse>().ReverseMap();
        CreateMap<Overtime, UpdateOvertimeCommand>().ReverseMap();
        CreateMap<Overtime, UpdatedOvertimeResponse>().ReverseMap();
        CreateMap<Overtime, DeleteOvertimeCommand>().ReverseMap();
        CreateMap<Overtime, DeletedOvertimeResponse>().ReverseMap();
        CreateMap<Overtime, GetByIdOvertimeResponse>().ReverseMap();
        CreateMap<Overtime, GetListOvertimeListItemDto>().ReverseMap();
        CreateMap<IPaginate<Overtime>, GetListResponse<GetListOvertimeListItemDto>>().ReverseMap();
        CreateMap<Overtime, GetOvertimeListByEmployeeIdListItemDto>().ReverseMap();
        CreateMap<IPaginate<Overtime>, GetListResponse<GetOvertimeListByEmployeeIdListItemDto>>().ReverseMap();
        
        
    }
}