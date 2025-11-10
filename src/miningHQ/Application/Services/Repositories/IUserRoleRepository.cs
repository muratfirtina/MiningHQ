using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IUserRoleRepository : IAsyncRepository<UserRole, Guid>, IRepository<UserRole, Guid> { }
