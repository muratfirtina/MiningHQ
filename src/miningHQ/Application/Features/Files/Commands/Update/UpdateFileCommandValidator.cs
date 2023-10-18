using FluentValidation;

namespace Application.Features.Files.Commands.Update;

public class UpdateFileCommandValidator : AbstractValidator<UpdateFileCommand>
{
    public UpdateFileCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Path).NotEmpty();
        RuleFor(c => c.Storage).NotEmpty();
    }
}