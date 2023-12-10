using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IEntitledLeaveRepository : IAsyncRepository<EntitledLeave, Guid>, IRepository<EntitledLeave, Guid>
{
}