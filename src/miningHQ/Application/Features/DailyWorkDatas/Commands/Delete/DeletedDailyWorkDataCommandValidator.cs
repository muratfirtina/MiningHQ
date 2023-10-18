using FluentValidation;

namespace Application.Features.DailyWorkDatas.Commands.Delete;

public class DeleteDailyWorkDataCommandValidator : AbstractValidator<DeleteDailyWorkDataCommand>
{
    public DeleteDailyWorkDataCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}