using FluentValidation;

namespace Application.Features.LeaveTypes.Commands.Update;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    public UpdateLeaveTypeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.EmployeeLeaveId).NotEmpty();
        RuleFor(c => c.EmployeeLeaveUsage).NotEmpty();
        RuleFor(c => c.UsageDate).NotEmpty();
        RuleFor(c => c.ReturnDate).NotEmpty();
    }
}