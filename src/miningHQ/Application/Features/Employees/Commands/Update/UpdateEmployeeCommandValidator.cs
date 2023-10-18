using FluentValidation;

namespace Application.Features.Employees.Commands.Update;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.BirthDate).NotEmpty();
        RuleFor(c => c.JobId).NotEmpty();
        RuleFor(c => c.Job).NotEmpty();
        RuleFor(c => c.QuarryId).NotEmpty();
        RuleFor(c => c.Quarry).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.Address).NotEmpty();
        RuleFor(c => c.HireDate).NotEmpty();
        RuleFor(c => c.DepartureDate).NotEmpty();
        RuleFor(c => c.LicenseType).NotEmpty();
        RuleFor(c => c.TypeOfBlood).NotEmpty();
        RuleFor(c => c.EmergencyContact).NotEmpty();
    }
}