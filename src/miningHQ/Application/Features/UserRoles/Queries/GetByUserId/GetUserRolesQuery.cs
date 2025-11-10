using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserRoles.Queries.GetByUserId;

public class GetUserRolesQuery : IRequest<List<GetUserRolesResponse>>, ISecuredRequest
{
    public Guid UserId { get; set; }

    public string[] Roles => new[] { Domain.Constants.Roles.Admin };

    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, List<GetUserRolesResponse>>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public GetUserRolesQueryHandler(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<List<GetUserRolesResponse>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var userRoles = await _userRoleRepository
                .Query()
                .Where(ur => ur.UserId == request.UserId)
                .Include(ur => ur.Role)
                .Select(ur => new GetUserRolesResponse
                {
                    Id = ur.Id,
                    RoleId = ur.RoleId,
                    RoleName = ur.Role.Name,
                    RoleDescription = ur.Role.Description
                })
                .ToListAsync(cancellationToken);

            return userRoles;
        }
    }
}
