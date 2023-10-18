using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using File = Domain.Entities.File;

namespace Application.Services.Files;

public interface IFilesService
{
    Task<File?> GetAsync(
        Expression<Func<File, bool>> predicate,
        Func<IQueryable<File>, IIncludableQueryable<File, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<File>?> GetListAsync(
        Expression<Func<File, bool>>? predicate = null,
        Func<IQueryable<File>, IOrderedQueryable<File>>? orderBy = null,
        Func<IQueryable<File>, IIncludableQueryable<File, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<File> AddAsync(File file);
    Task<File> UpdateAsync(File file);
    Task<File> DeleteAsync(File file, bool permanent = false);
}
