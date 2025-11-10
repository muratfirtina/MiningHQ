using Application.Features.RoleOperationClaims.Commands.AssignClaim;
using AutoMapper;
using Core.Security.Entities;

namespace Application.Features.RoleOperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RoleOperationClaim, AssignClaimToRoleCommand>().ReverseMap();
        CreateMap<RoleOperationClaim, AssignedClaimToRoleResponse>().ReverseMap();
    }
}
