using FluentValidation;

namespace Application.Features.Machines.Commands.Create;

public class CreateMachineCommandValidator : AbstractValidator<CreateMachineCommand>
{
    public CreateMachineCommandValidator()
    {
        RuleFor(c => c.ModelId).NotEmpty();
        RuleFor(c => c.Model).NotEmpty();
        RuleFor(c => c.QuarryId).NotEmpty();
        RuleFor(c => c.Quarry).NotEmpty();
        RuleFor(c => c.SerialNumber).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.MachineTypeId).NotEmpty();
        RuleFor(c => c.MachineType).NotEmpty();
    }
}