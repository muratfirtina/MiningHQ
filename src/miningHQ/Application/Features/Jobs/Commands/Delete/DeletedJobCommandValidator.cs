using FluentValidation;

namespace Application.Features.Jobs.Commands.Delete;

public class DeleteJobCommandValidator : AbstractValidator<DeleteJobCommand>
{
    public DeleteJobCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}