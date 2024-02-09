using FluentValidation;

namespace Application.Features.Timekeepings.Commands.Delete;

public class DeleteTimekeepingCommandValidator : AbstractValidator<DeleteTimekeepingCommand>
{
    public DeleteTimekeepingCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}