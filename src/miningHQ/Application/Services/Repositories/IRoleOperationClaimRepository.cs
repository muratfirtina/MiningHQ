using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IRoleOperationClaimRepository : IAsyncRepository<RoleOperationClaim, Guid>, IRepository<RoleOperationClaim, Guid> { }
