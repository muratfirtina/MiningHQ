using Application.Features.DailyFuelConsumptionDatas.Commands.Create;
using Application.Features.DailyFuelConsumptionDatas.Commands.Delete;
using Application.Features.DailyFuelConsumptionDatas.Commands.Update;
using Application.Features.DailyFuelConsumptionDatas.Queries.GetById;
using Application.Features.DailyFuelConsumptionDatas.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.DailyFuelConsumptionDatas.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<DailyFuelConsumptionData, CreateDailyFuelConsumptionDataCommand>().ReverseMap();
        CreateMap<DailyFuelConsumptionData, CreatedDailyFuelConsumptionDataResponse>().ReverseMap();
        CreateMap<DailyFuelConsumptionData, UpdateDailyFuelConsumptionDataCommand>().ReverseMap();
        CreateMap<DailyFuelConsumptionData, UpdatedDailyFuelConsumptionDataResponse>().ReverseMap();
        CreateMap<DailyFuelConsumptionData, DeleteDailyFuelConsumptionDataCommand>().ReverseMap();
        CreateMap<DailyFuelConsumptionData, DeletedDailyFuelConsumptionDataResponse>().ReverseMap();
        CreateMap<DailyFuelConsumptionData, GetByIdDailyFuelConsumptionDataResponse>().ReverseMap();
        CreateMap<DailyFuelConsumptionData, GetListDailyFuelConsumptionDataListItemDto>().ReverseMap();
        CreateMap<IPaginate<DailyFuelConsumptionData>, GetListResponse<GetListDailyFuelConsumptionDataListItemDto>>().ReverseMap();
    }
}