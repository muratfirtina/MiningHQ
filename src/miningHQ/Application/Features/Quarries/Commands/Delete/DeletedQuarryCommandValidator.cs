using FluentValidation;

namespace Application.Features.Quarries.Commands.Delete;

public class DeleteQuarryCommandValidator : AbstractValidator<DeleteQuarryCommand>
{
    public DeleteQuarryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}