using Application.Features.Files.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using File = Domain.Entities.File;

namespace Application.Services.Files;

public class FilesManager : IFilesService
{
    private readonly IFileRepository _fileRepository;
    private readonly FileBusinessRules _fileBusinessRules;

    public FilesManager(IFileRepository fileRepository, FileBusinessRules fileBusinessRules)
    {
        _fileRepository = fileRepository;
        _fileBusinessRules = fileBusinessRules;
    }

    public async Task<File?> GetAsync(
        Expression<Func<File, bool>> predicate,
        Func<IQueryable<File>, IIncludableQueryable<File, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        File? file = await _fileRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return file;
    }

    public async Task<IPaginate<File>?> GetListAsync(
        Expression<Func<File, bool>>? predicate = null,
        Func<IQueryable<File>, IOrderedQueryable<File>>? orderBy = null,
        Func<IQueryable<File>, IIncludableQueryable<File, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<File> fileList = await _fileRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return fileList;
    }

    public async Task<File> AddAsync(File file)
    {
        File addedFile = await _fileRepository.AddAsync(file);

        return addedFile;
    }

    public async Task<File> UpdateAsync(File file)
    {
        File updatedFile = await _fileRepository.UpdateAsync(file);

        return updatedFile;
    }

    public async Task<File> DeleteAsync(File file, bool permanent = false)
    {
        File deletedFile = await _fileRepository.DeleteAsync(file);

        return deletedFile;
    }
}
