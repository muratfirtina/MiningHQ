using FluentValidation;

namespace Application.Features.Timekeepings.Commands.Create;

public class CreateTimekeepingCommandValidator : AbstractValidator<CreateTimekeepingCommand>
{
    public CreateTimekeepingCommandValidator()
    {
        RuleFor(c => c.Date).NotEmpty();
        RuleFor(c => c.EmployeeId).NotEmpty();

    }
}