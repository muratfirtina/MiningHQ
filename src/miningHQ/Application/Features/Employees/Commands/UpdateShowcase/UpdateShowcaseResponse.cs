using Core.Application.Responses;

namespace Application.Features.Employees.Commands.UpdateShowcase;

public class UpdateShowcaseResponse: IResponse
{
    public bool Showcase { get; set; }
}