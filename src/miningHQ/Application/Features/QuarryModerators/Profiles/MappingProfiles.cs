using Application.Features.QuarryModerators.Commands.Create;
using Application.Features.QuarryModerators.Commands.Delete;
using Application.Features.QuarryModerators.Queries.GetByUserId;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.QuarryModerators.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<QuarryModerator, CreateQuarryModeratorCommand>().ReverseMap();
        CreateMap<QuarryModerator, CreatedQuarryModeratorResponse>().ReverseMap();
        
        CreateMap<QuarryModerator, DeleteQuarryModeratorCommand>().ReverseMap();
        CreateMap<QuarryModerator, DeletedQuarryModeratorResponse>().ReverseMap();
        
        CreateMap<IPaginate<QuarryModerator>, GetUserQuarriesResponse>()
            .ForMember(dest => dest.Quarries, 
                opt => opt.MapFrom(src => src.Items.Select(qm => qm.Quarry)));
        
        CreateMap<Quarry, UserQuarryDto>();
    }
}
