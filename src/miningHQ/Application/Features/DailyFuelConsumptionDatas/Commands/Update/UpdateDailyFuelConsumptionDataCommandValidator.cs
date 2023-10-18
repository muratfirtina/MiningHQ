using FluentValidation;

namespace Application.Features.DailyFuelConsumptionDatas.Commands.Update;

public class UpdateDailyFuelConsumptionDataCommandValidator : AbstractValidator<UpdateDailyFuelConsumptionDataCommand>
{
    public UpdateDailyFuelConsumptionDataCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Date).NotEmpty();
        RuleFor(c => c.FuelConsumption).NotEmpty();
        RuleFor(c => c.MachineId).NotEmpty();
        RuleFor(c => c.Machine).NotEmpty();
    }
}