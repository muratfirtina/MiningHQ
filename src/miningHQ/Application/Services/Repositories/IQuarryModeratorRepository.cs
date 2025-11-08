using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IQuarryModeratorRepository : IAsyncRepository<QuarryModerator, Guid>, 
    IRepository<QuarryModerator, Guid>
{
}
