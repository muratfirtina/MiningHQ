using Application.Features.UserRoles.Commands.AssignRole;
using Application.Features.UserRoles.Commands.RemoveRole;
using AutoMapper;
using Core.Security.Entities;

namespace Application.Features.UserRoles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserRole, AssignRoleToUserCommand>().ReverseMap();
        CreateMap<UserRole, AssignedRoleToUserResponse>().ReverseMap();
        CreateMap<UserRole, RemoveRoleFromUserCommand>().ReverseMap();
        CreateMap<UserRole, RemovedRoleFromUserResponse>().ReverseMap();
    }
}
