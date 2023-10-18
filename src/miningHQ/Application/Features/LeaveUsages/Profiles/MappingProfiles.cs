using Application.Features.LeaveUsages.Commands.Create;
using Application.Features.LeaveUsages.Commands.Delete;
using Application.Features.LeaveUsages.Commands.Update;
using Application.Features.LeaveUsages.Queries.GetById;
using Application.Features.LeaveUsages.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.LeaveUsages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<LeaveUsage, CreateLeaveUsageCommand>().ReverseMap();
        CreateMap<LeaveUsage, CreatedLeaveUsageResponse>().ReverseMap();
        CreateMap<LeaveUsage, UpdateLeaveUsageCommand>().ReverseMap();
        CreateMap<LeaveUsage, UpdatedLeaveUsageResponse>().ReverseMap();
        CreateMap<LeaveUsage, DeleteLeaveUsageCommand>().ReverseMap();
        CreateMap<LeaveUsage, DeletedLeaveUsageResponse>().ReverseMap();
        CreateMap<LeaveUsage, GetByIdLeaveUsageResponse>().ReverseMap();
        CreateMap<LeaveUsage, GetListLeaveUsageListItemDto>().ReverseMap();
        CreateMap<IPaginate<LeaveUsage>, GetListResponse<GetListLeaveUsageListItemDto>>().ReverseMap();
    }
}