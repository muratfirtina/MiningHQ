using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ModelRepository : EfRepositoryBase<Model, Guid, MiningHQDbContext>, IModelRepository
{
    public ModelRepository(MiningHQDbContext context) : base(context)
    {
    }
}