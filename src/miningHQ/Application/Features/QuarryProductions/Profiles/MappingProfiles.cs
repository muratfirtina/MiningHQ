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
        // CRITICAL: Latitude ve Longitude otomatik hesaplanacak, AutoMapper bunları null yapmasın!
        CreateMap<CreateQuarryProductionCommand, QuarryProduction>()
            .ForMember(dest => dest.Latitude, opt => opt.Ignore())
            .ForMember(dest => dest.Longitude, opt => opt.Ignore());
        
        CreateMap<QuarryProduction, CreateQuarryProductionCommand>();
        CreateMap<QuarryProduction, CreatedQuarryProductionResponse>();
        CreateMap<QuarryProduction, GetByIdQuarryProductionResponse>();
        CreateMap<QuarryProduction, GetListQuarryProductionListItemDto>();
        CreateMap<IPaginate<QuarryProduction>, GetListResponse<GetListQuarryProductionListItemDto>>().ReverseMap();
    }
}
