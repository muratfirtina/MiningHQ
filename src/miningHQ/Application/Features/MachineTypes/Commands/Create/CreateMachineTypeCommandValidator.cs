using FluentValidation;

namespace Application.Features.MachineTypes.Commands.Create;

public class CreateMachineTypeCommandValidator : AbstractValidator<CreateMachineTypeCommand>
{
    public CreateMachineTypeCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}