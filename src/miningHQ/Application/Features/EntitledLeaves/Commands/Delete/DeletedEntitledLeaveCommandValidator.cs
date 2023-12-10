using FluentValidation;

namespace Application.Features.EntitledLeaves.Commands.Delete;

public class DeleteEntitledLeaveCommandValidator : AbstractValidator<DeleteEntitledLeaveCommand>
{
    public DeleteEntitledLeaveCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}