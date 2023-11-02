using Application.Features.DailyWorkDatas.Constants;
using Application.Features.DailyWorkDatas.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.DailyWorkDatas.Constants.DailyWorkDatasOperationClaims;

namespace Application.Features.DailyWorkDatas.Commands.Delete;

public class DeleteDailyWorkDataCommand : IRequest<DeletedDailyWorkDataResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, DailyWorkDatasOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetDailyWorkDatas"};

    public class DeleteDailyWorkDataCommandHandler : IRequestHandler<DeleteDailyWorkDataCommand, DeletedDailyWorkDataResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDailyWorkDataRepository _dailyWorkDataRepository;
        private readonly DailyWorkDataBusinessRules _dailyWorkDataBusinessRules;

        public DeleteDailyWorkDataCommandHandler(IMapper mapper, IDailyWorkDataRepository dailyWorkDataRepository,
                                         DailyWorkDataBusinessRules dailyWorkDataBusinessRules)
        {
            _mapper = mapper;
            _dailyWorkDataRepository = dailyWorkDataRepository;
            _dailyWorkDataBusinessRules = dailyWorkDataBusinessRules;
        }

        public async Task<DeletedDailyWorkDataResponse> Handle(DeleteDailyWorkDataCommand request, CancellationToken cancellationToken)
        {
            DailyWorkData? dailyWorkData = await _dailyWorkDataRepository.GetAsync(predicate: dwd => dwd.Id == request.Id, cancellationToken: cancellationToken);
            await _dailyWorkDataBusinessRules.DailyWorkDataShouldExistWhenSelected(dailyWorkData);

            await _dailyWorkDataRepository.DeleteAsync(dailyWorkData!);

            DeletedDailyWorkDataResponse response = _mapper.Map<DeletedDailyWorkDataResponse>(dailyWorkData);
            return response;
        }
    }
}