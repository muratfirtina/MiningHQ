using FluentValidation;

namespace Application.Features.Maintenances.Commands.Delete;

public class DeleteMaintenanceCommandValidator : AbstractValidator<DeleteMaintenanceCommand>
{
    public DeleteMaintenanceCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}