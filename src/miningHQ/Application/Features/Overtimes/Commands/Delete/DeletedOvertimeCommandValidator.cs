using FluentValidation;

namespace Application.Features.Overtimes.Commands.Delete;

public class DeleteOvertimeCommandValidator : AbstractValidator<DeleteOvertimeCommand>
{
    public DeleteOvertimeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}