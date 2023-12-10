using FluentValidation;

namespace Application.Features.LeaveTypes.Commands.Create;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    public CreateLeaveTypeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.EmployeeLeaveId).NotEmpty();
        RuleFor(c => c.EmployeeLeaveUsage).NotEmpty();
        RuleFor(c => c.UsageDate).NotEmpty();
        RuleFor(c => c.ReturnDate).NotEmpty();
    }
}