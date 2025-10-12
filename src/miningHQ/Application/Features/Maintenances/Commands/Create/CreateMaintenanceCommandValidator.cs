using FluentValidation;

namespace Application.Features.Maintenances.Commands.Create;

public class CreateMaintenanceCommandValidator : AbstractValidator<CreateMaintenanceCommand>
{
    public CreateMaintenanceCommandValidator()
    {
        RuleFor(c => c.MachineId).NotEmpty();
        RuleFor(c => c.MaintenanceTypeId).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.MaintenanceDate).NotEmpty();
        RuleFor(c => c.MachineWorkingTimeOrKilometer).NotEmpty();
        RuleFor(c => c.MaintenanceFirm).NotEmpty();
    }
}