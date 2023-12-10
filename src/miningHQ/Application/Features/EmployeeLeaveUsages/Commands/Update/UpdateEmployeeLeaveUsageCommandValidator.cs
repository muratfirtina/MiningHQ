using FluentValidation;

namespace Application.Features.EmployeeLeaveUsages.Commands.Update;

public class UpdateEmployeeLeaveUsageCommandValidator : AbstractValidator<UpdateEmployeeLeaveUsageCommand>
{
    public UpdateEmployeeLeaveUsageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.EmployeeId).NotEmpty();
        RuleFor(c => c.Employee).NotEmpty();
        RuleFor(c => c.TotalLeaveDays).NotEmpty();
    }
}