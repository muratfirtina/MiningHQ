using Application.Features.Files.Constants;
using Application.Features.Files.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Files.Constants.FilesOperationClaims;
using File = Domain.Entities.File;

namespace Application.Features.Files.Commands.Create;

public class CreateFileCommand : IRequest<CreatedFileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Storage { get; set; }

    public string[] Roles => new[] { Admin, Write, FilesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetFiles";

    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, CreatedFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFileRepository _fileRepository;
        private readonly FileBusinessRules _fileBusinessRules;

        public CreateFileCommandHandler(IMapper mapper, IFileRepository fileRepository,
                                         FileBusinessRules fileBusinessRules)
        {
            _mapper = mapper;
            _fileRepository = fileRepository;
            _fileBusinessRules = fileBusinessRules;
        }

        public async Task<CreatedFileResponse> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            File file = _mapper.Map<File>(request);

            await _fileRepository.AddAsync(file);

            CreatedFileResponse response = _mapper.Map<CreatedFileResponse>(file);
            return response;
        }
    }
}