using FluentValidation;

namespace Application.Features.LeaveUsages.Commands.Delete;

public class DeleteLeaveUsageCommandValidator : AbstractValidator<DeleteLeaveUsageCommand>
{
    public DeleteLeaveUsageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}