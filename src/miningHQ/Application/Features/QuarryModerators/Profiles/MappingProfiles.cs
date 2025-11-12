using Application.Features.QuarryModerators.Commands.Create;
using Application.Features.QuarryModerators.Commands.Delete;
using Application.Features.QuarryModerators.Queries.GetByUserId;
using Application.Features.QuarryModerators.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
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
        
        CreateMap<QuarryModerator, GetListQuarryModeratorListItemDto>()
            .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.QuarryName, opt => opt.MapFrom(src => src.Quarry.Name));
        
        CreateMap<IPaginate<QuarryModerator>, GetListResponse<GetListQuarryModeratorListItemDto>>().ReverseMap();
    }
}
