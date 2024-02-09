using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Timekeepings.Commands.Update;

public class UpdatedTimekeepingResponse : IResponse
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public bool? Status { get; set; }
}