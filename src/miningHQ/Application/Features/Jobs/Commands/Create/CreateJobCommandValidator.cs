using FluentValidation;

namespace Application.Features.Jobs.Commands.Create;

public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}