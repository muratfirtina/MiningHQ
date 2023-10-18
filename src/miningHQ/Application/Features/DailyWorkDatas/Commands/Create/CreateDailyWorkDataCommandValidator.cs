using FluentValidation;

namespace Application.Features.DailyWorkDatas.Commands.Create;

public class CreateDailyWorkDataCommandValidator : AbstractValidator<CreateDailyWorkDataCommand>
{
    public CreateDailyWorkDataCommandValidator()
    {
        RuleFor(c => c.Date).NotEmpty();
        RuleFor(c => c.WorkingHoursOrKm).NotEmpty();
        RuleFor(c => c.MachineId).NotEmpty();
        RuleFor(c => c.Machine).NotEmpty();
    }
}