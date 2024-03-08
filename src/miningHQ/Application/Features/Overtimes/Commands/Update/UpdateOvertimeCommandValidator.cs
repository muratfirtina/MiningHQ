using FluentValidation;

namespace Application.Features.Overtimes.Commands.Update;

public class UpdateOvertimeCommandValidator : AbstractValidator<UpdateOvertimeCommand>
{
    public UpdateOvertimeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.EmployeeId).NotEmpty();
        RuleFor(c => c.OvertimeDate).NotEmpty();
        RuleFor(c => c.OvertimeHours).NotEmpty();
    }
}