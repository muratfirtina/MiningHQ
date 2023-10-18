using Application.Features.Files.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using File = Domain.Entities.File;

namespace Application.Features.Files.Rules;

public class FileBusinessRules : BaseBusinessRules
{
    private readonly IFileRepository _fileRepository;

    public FileBusinessRules(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public Task FileShouldExistWhenSelected(File? file)
    {
        if (file == null)
            throw new BusinessException(FilesBusinessMessages.FileNotExists);
        return Task.CompletedTask;
    }

    public async Task FileIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        File? file = await _fileRepository.GetAsync(
            predicate: f => f.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await FileShouldExistWhenSelected(file);
    }
}