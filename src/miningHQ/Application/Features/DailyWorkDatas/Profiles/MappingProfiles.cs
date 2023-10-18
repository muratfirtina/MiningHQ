using Application.Features.DailyWorkDatas.Commands.Create;
using Application.Features.DailyWorkDatas.Commands.Delete;
using Application.Features.DailyWorkDatas.Commands.Update;
using Application.Features.DailyWorkDatas.Queries.GetById;
using Application.Features.DailyWorkDatas.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.DailyWorkDatas.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<DailyWorkData, CreateDailyWorkDataCommand>().ReverseMap();
        CreateMap<DailyWorkData, CreatedDailyWorkDataResponse>().ReverseMap();
        CreateMap<DailyWorkData, UpdateDailyWorkDataCommand>().ReverseMap();
        CreateMap<DailyWorkData, UpdatedDailyWorkDataResponse>().ReverseMap();
        CreateMap<DailyWorkData, DeleteDailyWorkDataCommand>().ReverseMap();
        CreateMap<DailyWorkData, DeletedDailyWorkDataResponse>().ReverseMap();
        CreateMap<DailyWorkData, GetByIdDailyWorkDataResponse>().ReverseMap();
        CreateMap<DailyWorkData, GetListDailyWorkDataListItemDto>().ReverseMap();
        CreateMap<IPaginate<DailyWorkData>, GetListResponse<GetListDailyWorkDataListItemDto>>().ReverseMap();
    }
}