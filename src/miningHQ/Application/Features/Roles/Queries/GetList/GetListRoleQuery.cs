using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Roles.Queries.GetList;

public class GetListRoleQuery : IRequest<GetListResponse<GetListRoleListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; } = null!;

    public string[] Roles => new[] { Domain.Constants.Roles.Admin };

    public class GetListRoleQueryHandler : IRequestHandler<GetListRoleQuery, GetListResponse<GetListRoleListItemDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetListRoleQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListRoleListItemDto>> Handle(GetListRoleQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Role> roles = await _roleRepository.GetListAsync(
                include: r => r.Include(r => r.RoleOperationClaims).ThenInclude(roc => roc.OperationClaim),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListRoleListItemDto> response = _mapper.Map<GetListResponse<GetListRoleListItemDto>>(roles);
            return response;
        }
    }
}
