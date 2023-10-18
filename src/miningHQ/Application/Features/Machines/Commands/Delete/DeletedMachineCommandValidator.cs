using FluentValidation;

namespace Application.Features.Machines.Commands.Delete;

public class DeleteMachineCommandValidator : AbstractValidator<DeleteMachineCommand>
{
    public DeleteMachineCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}