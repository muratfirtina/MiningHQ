using FluentValidation;

namespace Application.Features.Quarries.Commands.Update;

public class UpdateQuarryCommandValidator : AbstractValidator<UpdateQuarryCommand>
{
    public UpdateQuarryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}