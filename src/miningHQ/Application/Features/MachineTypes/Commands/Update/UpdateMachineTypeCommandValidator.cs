using FluentValidation;

namespace Application.Features.MachineTypes.Commands.Update;

public class UpdateMachineTypeCommandValidator : AbstractValidator<UpdateMachineTypeCommand>
{
    public UpdateMachineTypeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}