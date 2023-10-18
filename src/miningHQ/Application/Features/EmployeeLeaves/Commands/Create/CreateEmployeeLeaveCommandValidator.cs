using FluentValidation;

namespace Application.Features.EmployeeLeaves.Commands.Create;

public class CreateEmployeeLeaveCommandValidator : AbstractValidator<CreateEmployeeLeaveCommand>
{
    public CreateEmployeeLeaveCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.EmployeeId).NotEmpty();
        RuleFor(c => c.Employee).NotEmpty();
        RuleFor(c => c.TotalLeaveDays).NotEmpty();
    }
}