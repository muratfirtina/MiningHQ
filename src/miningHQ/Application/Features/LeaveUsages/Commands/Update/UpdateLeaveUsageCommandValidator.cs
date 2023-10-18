using FluentValidation;

namespace Application.Features.LeaveUsages.Commands.Update;

public class UpdateLeaveUsageCommandValidator : AbstractValidator<UpdateLeaveUsageCommand>
{
    public UpdateLeaveUsageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.EmployeeLeaveId).NotEmpty();
        RuleFor(c => c.EmployeeLeave).NotEmpty();
        RuleFor(c => c.UsageDate).NotEmpty();
        RuleFor(c => c.ReturnDate).NotEmpty();
    }
}