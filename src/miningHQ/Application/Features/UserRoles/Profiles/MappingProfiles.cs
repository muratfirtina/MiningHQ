using Application.Features.UserRoles.Commands.AssignRole;
using AutoMapper;
using Core.Security.Entities;

namespace Application.Features.UserRoles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserRole, AssignRoleToUserCommand>().ReverseMap();
        CreateMap<UserRole, AssignedRoleToUserResponse>().ReverseMap();
    }
}
