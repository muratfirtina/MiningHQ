using FluentValidation;

namespace Application.Features.DailyFuelConsumptionDatas.Commands.Delete;

public class DeleteDailyFuelConsumptionDataCommandValidator : AbstractValidator<DeleteDailyFuelConsumptionDataCommand>
{
    public DeleteDailyFuelConsumptionDataCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}