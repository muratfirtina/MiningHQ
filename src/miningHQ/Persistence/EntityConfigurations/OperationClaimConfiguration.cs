using Application.Features.OperationClaims.Constants;
using Core.Security.Entities;
using Domain.Constants;
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
        
        
        #region EmployeeLeaveUsages
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "EmployeeLeaveUsages.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "EmployeeLeaveUsages.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "EmployeeLeaveUsages.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "EmployeeLeaveUsages.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "EmployeeLeaveUsages.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "EmployeeLeaveUsages.Delete" });
        
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
        
        
        #region EntitledLeaves
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "EntitledLeaves.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "EntitledLeaves.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "EntitledLeaves.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "EntitledLeaves.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "EntitledLeaves.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "EntitledLeaves.Delete" });
        
        #endregion
        
        
        #region Timekeepings
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Timekeepings.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Timekeepings.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Timekeepings.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Timekeepings.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Timekeepings.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Timekeepings.Delete" });
        
        #endregion
        
        
        #region Overtimes
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Overtimes.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Overtimes.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Overtimes.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Overtimes.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Overtimes.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Overtimes.Delete" });
        
        #endregion
        
        
        #region Departments
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Departments.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Departments.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Departments.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Departments.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Departments.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Departments.Delete" });
        
        #endregion
        
        
        #region QuarryModerators   
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuarryModerators.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuarryModerators.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuarryModerators.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuarryModerators.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuarryModerators.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuarryModerators.Delete" });
        
        // Admin role claims
        new OperationClaim { Id = ++id, Name = Roles.Admin };
        new OperationClaim { Id = ++id, Name = Roles.Claims.AdminPanel };
        new OperationClaim { Id = ++id, Name = Roles.Claims.UsersRead };
        new OperationClaim { Id = ++id, Name = Roles.Claims.UsersWrite };
        
        // Moderator role claims
        new OperationClaim { Id = ++id, Name = Roles.Moderator };
        new OperationClaim { Id = ++id, Name = Roles.Claims.EmployeesRead };

        new OperationClaim { Id = ++id, Name = Roles.Claims.MachinesRead };
        new OperationClaim { Id = ++id, Name = Roles.Claims.MachinesWrite };
        new OperationClaim { Id = ++id, Name = Roles.Claims.QuarriesRead };
        new OperationClaim { Id = ++id, Name = Roles.Claims.QuarriesWrite };
        
        // HR Assistant role claims
        new OperationClaim { Id = ++id, Name = Roles.HRAssistant };
        new OperationClaim { Id = ++id, Name = Roles.Claims.EmployeesRead };
        new OperationClaim { Id = ++id, Name = Roles.Claims.EmployeesWrite };

        #endregion
        
        return seeds;
    }
}
