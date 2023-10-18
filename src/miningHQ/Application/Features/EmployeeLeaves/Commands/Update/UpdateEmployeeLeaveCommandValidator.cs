using FluentValidation;

namespace Application.Features.EmployeeLeaves.Commands.Update;

public class UpdateEmployeeLeaveCommandValidator : AbstractValidator<UpdateEmployeeLeaveCommand>
{
    public UpdateEmployeeLeaveCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.EmployeeId).NotEmpty();
        RuleFor(c => c.Employee).NotEmpty();
        RuleFor(c => c.TotalLeaveDays).NotEmpty();
    }
}