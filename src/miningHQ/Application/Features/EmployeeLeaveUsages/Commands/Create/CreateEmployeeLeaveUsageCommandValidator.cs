using Application.Features.EmployeeLeaveUsages.Commands.Create;
using FluentValidation;

namespace Application.Features.EmployeeLeaveUsages.Commands.Create;

public class CreateEmployeeLeaveUsageCommandValidator : AbstractValidator<CreateEmployeeLeaveUsageCommand>
{
    public CreateEmployeeLeaveUsageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.EmployeeId).NotEmpty();
    }
}