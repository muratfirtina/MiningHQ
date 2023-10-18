using FluentValidation;

namespace Application.Features.LeaveUsages.Commands.Create;

public class CreateLeaveUsageCommandValidator : AbstractValidator<CreateLeaveUsageCommand>
{
    public CreateLeaveUsageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.EmployeeLeaveId).NotEmpty();
        RuleFor(c => c.EmployeeLeave).NotEmpty();
        RuleFor(c => c.UsageDate).NotEmpty();
        RuleFor(c => c.ReturnDate).NotEmpty();
    }
}