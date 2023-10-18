using FluentValidation;

namespace Application.Features.DailyWorkDatas.Commands.Update;

public class UpdateDailyWorkDataCommandValidator : AbstractValidator<UpdateDailyWorkDataCommand>
{
    public UpdateDailyWorkDataCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Date).NotEmpty();
        RuleFor(c => c.WorkingHoursOrKm).NotEmpty();
        RuleFor(c => c.MachineId).NotEmpty();
        RuleFor(c => c.Machine).NotEmpty();
    }
}