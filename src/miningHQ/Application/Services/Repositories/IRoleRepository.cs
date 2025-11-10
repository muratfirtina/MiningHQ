using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IRoleRepository : IAsyncRepository<Role, int>, IRepository<Role, int> { }
