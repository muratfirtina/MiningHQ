using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Contexts;

namespace Persistence.DbConfig;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MiningHQDbContext>
{

    public MiningHQDbContext CreateDbContext(string[] args)
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<MiningHQDbContext>();
        dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString).EnableSensitiveDataLogging();
        
        
        return new MiningHQDbContext(dbContextOptionsBuilder.Options);
    }
    
}