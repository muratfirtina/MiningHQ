using FluentValidation;

namespace Application.Features.Files.Commands.Delete;

public class DeleteFileCommandValidator : AbstractValidator<DeleteFileCommand>
{
    public DeleteFileCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}