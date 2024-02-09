using FluentValidation;

namespace Application.Features.Timekeepings.Commands.Update;

public class UpdateTimekeepingCommandValidator : AbstractValidator<UpdateTimekeepingCommand>
{
    public UpdateTimekeepingCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Date).NotEmpty();
        RuleFor(c => c.EmployeeId).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
    }
}