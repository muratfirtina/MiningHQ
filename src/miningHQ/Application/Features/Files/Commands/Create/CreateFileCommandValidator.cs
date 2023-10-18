using FluentValidation;

namespace Application.Features.Files.Commands.Create;

public class CreateFileCommandValidator : AbstractValidator<CreateFileCommand>
{
    public CreateFileCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Path).NotEmpty();
        RuleFor(c => c.Storage).NotEmpty();
    }
}