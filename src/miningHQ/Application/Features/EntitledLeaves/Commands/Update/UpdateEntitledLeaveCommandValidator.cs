using FluentValidation;

namespace Application.Features.EntitledLeaves.Commands.Update;

public class UpdateEntitledLeaveCommandValidator : AbstractValidator<UpdateEntitledLeaveCommand>
{
    public UpdateEntitledLeaveCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.EmployeeId).NotEmpty();
        RuleFor(c => c.Employee).NotEmpty();
        RuleFor(c => c.LeaveTypeId).NotEmpty();
        RuleFor(c => c.LeaveType).NotEmpty();
        RuleFor(c => c.EntitledDate).NotEmpty();
    }
}