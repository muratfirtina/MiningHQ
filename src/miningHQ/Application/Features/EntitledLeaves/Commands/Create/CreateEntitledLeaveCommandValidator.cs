using FluentValidation;

namespace Application.Features.EntitledLeaves.Commands.Create;

public class CreateEntitledLeaveCommandValidator : AbstractValidator<CreateEntitledLeaveCommand>
{
    public CreateEntitledLeaveCommandValidator()
    {
        RuleFor(c => c.EmployeeId).NotEmpty();
        RuleFor(c => c.LeaveTypeId).NotEmpty();
        RuleFor(c => c.EntitledDate).NotEmpty();
        RuleFor(c => c.EntitledDays).NotEmpty();
    }
}