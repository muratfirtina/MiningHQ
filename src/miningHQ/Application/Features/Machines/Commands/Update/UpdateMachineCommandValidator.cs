using FluentValidation;

namespace Application.Features.Machines.Commands.Update;

public class UpdateMachineCommandValidator : AbstractValidator<UpdateMachineCommand>
{
    public UpdateMachineCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ModelId).NotEmpty();
        RuleFor(c => c.QuarryId).NotEmpty();
        RuleFor(c => c.SerialNumber).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.MachineTypeId).NotEmpty();
    }
}