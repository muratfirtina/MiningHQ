using Application.Features.Roles.Commands.Create;
using Application.Features.Roles.Commands.Update;
using Application.Features.Roles.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.Roles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Role, CreateRoleCommand>().ReverseMap();
        CreateMap<Role, CreatedRoleResponse>().ReverseMap();

        CreateMap<Role, UpdateRoleCommand>().ReverseMap();
        CreateMap<Role, UpdatedRoleResponse>().ReverseMap();

        CreateMap<Role, GetListRoleListItemDto>()
            .ForMember(dest => dest.Claims, opt => opt.MapFrom(src =>
                src.RoleOperationClaims.Select(roc => roc.OperationClaim.Name).ToList()))
            .ReverseMap();

        CreateMap<IPaginate<Role>, GetListResponse<GetListRoleListItemDto>>().ReverseMap();
    }
}
