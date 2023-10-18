using FluentValidation;

namespace Application.Features.Quarries.Commands.Create;

public class CreateQuarryCommandValidator : AbstractValidator<CreateQuarryCommand>
{
    public CreateQuarryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}