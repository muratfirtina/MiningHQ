using FluentValidation;

namespace Application.Features.DailyFuelConsumptionDatas.Commands.Create;

public class CreateDailyFuelConsumptionDataCommandValidator : AbstractValidator<CreateDailyFuelConsumptionDataCommand>
{
    public CreateDailyFuelConsumptionDataCommandValidator()
    {
        RuleFor(c => c.Date).NotEmpty();
        RuleFor(c => c.FuelConsumption).NotEmpty();
        RuleFor(c => c.MachineId).NotEmpty();
        RuleFor(c => c.Machine).NotEmpty();
    }
}