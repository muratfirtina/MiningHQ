using Application.Features.OperationClaims.Constants;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasMany(oc => oc.UserOperationClaims);

        builder.HasData(getSeeds());
    }

    private HashSet<OperationClaim> getSeeds()
    {
        int id = 0;
        HashSet<OperationClaim> seeds =
            new()
            {
                new OperationClaim { Id = ++id, Name = GeneralOperationClaims.Admin }
            };

        
        #region Brands
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Brands.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Brands.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Brands.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Brands.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Brands.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Brands.Delete" });
        
        #endregion
        
        
        #region DailyFuelConsumptionDatas
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "DailyFuelConsumptionDatas.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "DailyFuelConsumptionDatas.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "DailyFuelConsumptionDatas.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "DailyFuelConsumptionDatas.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "DailyFuelConsumptionDatas.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "DailyFuelConsumptionDatas.Delete" });
        
        #endregion
        
        
        #region MaintenanceTypes
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "MaintenanceTypes.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "MaintenanceTypes.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "MaintenanceTypes.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "MaintenanceTypes.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "MaintenanceTypes.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "MaintenanceTypes.Delete" });
        
        #endregion
        
        
        #region Maintenances
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Maintenances.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Maintenances.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Maintenances.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Maintenances.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Maintenances.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Maintenances.Delete" });
        
        #endregion
        
        
        #region Jobs
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Jobs.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Jobs.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Jobs.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Jobs.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Jobs.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Jobs.Delete" });
        
        #endregion
        
        
        #region LeaveUsages
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "LeaveUsages.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "LeaveUsages.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LeaveUsages.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "LeaveUsages.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LeaveUsages.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LeaveUsages.Delete" });
        
        #endregion
        
        
        #region MachineTypes
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "MachineTypes.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "MachineTypes.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "MachineTypes.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "MachineTypes.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "MachineTypes.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "MachineTypes.Delete" });
        
        #endregion
        
        
        #region Quarries
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Quarries.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Quarries.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Quarries.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Quarries.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Quarries.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Quarries.Delete" });
        
        #endregion
        
        
        #region DailyWorkDatas
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "DailyWorkDatas.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "DailyWorkDatas.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "DailyWorkDatas.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "DailyWorkDatas.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "DailyWorkDatas.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "DailyWorkDatas.Delete" });
        
        #endregion
        
        
        #region Models
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Models.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Models.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Models.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Models.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Models.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Models.Delete" });
        
        #endregion
        
        
        #region EmployeeLeaves
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "EmployeeLeaves.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "EmployeeLeaves.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "EmployeeLeaves.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "EmployeeLeaves.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "EmployeeLeaves.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "EmployeeLeaves.Delete" });
        
        #endregion
        
        
        #region Machines
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Machines.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Machines.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Machines.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Machines.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Machines.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Machines.Delete" });
        
        #endregion
        
        
        #region Files
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Files.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Files.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Files.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Files.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Files.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Files.Delete" });
        
        #endregion
        
        
        #region Employees
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Employees.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Employees.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Employees.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Employees.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Employees.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Employees.Delete" });
        
        #endregion
        
        return seeds;
    }
}
