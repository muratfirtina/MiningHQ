using FluentValidation;

namespace Application.Features.DailyEntries.Commands.BulkCreateDailyEntry;

public class BulkCreateDailyEntryCommandValidator : AbstractValidator<BulkCreateDailyEntryCommand>
{
    public BulkCreateDailyEntryCommandValidator()
    {
        RuleFor(c => c.EntryDate)
            .NotEmpty().WithMessage("Giriş tarihi boş olamaz")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Gelecek tarihli kayıt yapılamaz");

        RuleFor(c => c.MachineEntries)
            .NotEmpty().WithMessage("En az bir makina verisi girilmelidir");

        RuleForEach(c => c.MachineEntries).ChildRules(entry =>
        {
            entry.RuleFor(e => e.MachineId)
                .NotEmpty().WithMessage("Makina ID boş olamaz");

            entry.RuleFor(e => e.NewTotalHours)
                .GreaterThanOrEqualTo(e => e.CurrentTotalHours)
                .WithMessage("Yeni saat, mevcut saatten küçük olamaz");

            entry.RuleFor(e => e.FuelConsumption)
                .GreaterThanOrEqualTo(0).WithMessage("Yakıt tüketimi negatif olamaz")
                .LessThan(10000).WithMessage("Yakıt tüketimi çok yüksek");
        });
    }
}
