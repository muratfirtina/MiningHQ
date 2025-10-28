using Application.Features.QuarryProductions.Commands.Create;
using Application.Features.QuarryProductions.Queries.GetById;
using Application.Features.QuarryProductions.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.QuarryProductions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<QuarryProduction, CreateQuarryProductionCommand>().ReverseMap();
        CreateMap<QuarryProduction, CreatedQuarryProductionResponse>().ReverseMap();
        CreateMap<QuarryProduction, GetByIdQuarryProductionResponse>().ReverseMap();
        CreateMap<QuarryProduction, GetListQuarryProductionListItemDto>().ReverseMap();
        CreateMap<IPaginate<QuarryProduction>, GetListResponse<GetListQuarryProductionListItemDto>>().ReverseMap();
    }
}
