using Core.Application.Responses;

namespace Application.Features.DailyWorkDatas.Commands.Delete;

public class DeletedDailyWorkDataResponse : IResponse
{
    public Guid Id { get; set; }
}