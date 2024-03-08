using FluentValidation;

namespace Application.Features.Overtimes.Commands.Create;

public class CreateOvertimeCommandValidator : AbstractValidator<CreateOvertimeCommand>
{
    public CreateOvertimeCommandValidator()
    {
        RuleFor(c => c.EmployeeId).NotEmpty();
        RuleFor(c => c.OvertimeDate).NotEmpty();
        RuleFor(c => c.OvertimeHours).NotEmpty();
    }
}