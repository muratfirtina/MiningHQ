using FluentValidation;

namespace Application.Features.Maintenances.Commands.Update;

public class UpdateMaintenanceCommandValidator : AbstractValidator<UpdateMaintenanceCommand>
{
    public UpdateMaintenanceCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.MachineId).NotEmpty();
        RuleFor(c => c.MaintenanceTypeId).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.MaintenanceDate).NotEmpty();
        RuleFor(c => c.MachineWorkingTimeOrKilometer).NotEmpty();
        RuleFor(c => c.MaintenanceFirm).NotEmpty();
    }
}