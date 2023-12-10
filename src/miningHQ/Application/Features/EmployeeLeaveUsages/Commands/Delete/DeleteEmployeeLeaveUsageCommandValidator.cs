using FluentValidation;

namespace Application.Features.EmployeeLeaveUsages.Commands.Delete;

public class DeleteEmployeeLeaveUsageCommandValidator : AbstractValidator<DeleteEmployeeLeaveUsageCommand>
{
    public DeleteEmployeeLeaveUsageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}