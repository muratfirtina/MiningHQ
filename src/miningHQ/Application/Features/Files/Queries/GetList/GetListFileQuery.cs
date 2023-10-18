using Application.Features.Files.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Files.Constants.FilesOperationClaims;
using File = Domain.Entities.File;

namespace Application.Features.Files.Queries.GetList;

public class GetListFileQuery : IRequest<GetListResponse<GetListFileListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListFiles({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetFiles";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListFileQueryHandler : IRequestHandler<GetListFileQuery, GetListResponse<GetListFileListItemDto>>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public GetListFileQueryHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFileListItemDto>> Handle(GetListFileQuery request, CancellationToken cancellationToken)
        {
            IPaginate<File> files = await _fileRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFileListItemDto> response = _mapper.Map<GetListResponse<GetListFileListItemDto>>(files);
            return response;
        }
    }
}