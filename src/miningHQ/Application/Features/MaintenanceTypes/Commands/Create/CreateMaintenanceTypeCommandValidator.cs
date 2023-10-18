using FluentValidation;

namespace Application.Features.MaintenanceTypes.Commands.Create;

public class CreateMaintenanceTypeCommandValidator : AbstractValidator<CreateMaintenanceTypeCommand>
{
    public CreateMaintenanceTypeCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}