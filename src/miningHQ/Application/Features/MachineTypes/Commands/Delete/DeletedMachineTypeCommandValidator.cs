using FluentValidation;

namespace Application.Features.MachineTypes.Commands.Delete;

public class DeleteMachineTypeCommandValidator : AbstractValidator<DeleteMachineTypeCommand>
{
    public DeleteMachineTypeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}