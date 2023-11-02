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

namespace Application.Features.DailyWorkDatas.Commands.Update;

public class UpdateDailyWorkDataCommand : IRequest<UpdatedDailyWorkDataResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int WorkingHoursOrKm { get; set; }
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }

    public string[] Roles => new[] { Admin, Write, DailyWorkDatasOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetDailyWorkDatas"};

    public class UpdateDailyWorkDataCommandHandler : IRequestHandler<UpdateDailyWorkDataCommand, UpdatedDailyWorkDataResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDailyWorkDataRepository _dailyWorkDataRepository;
        private readonly DailyWorkDataBusinessRules _dailyWorkDataBusinessRules;

        public UpdateDailyWorkDataCommandHandler(IMapper mapper, IDailyWorkDataRepository dailyWorkDataRepository,
                                         DailyWorkDataBusinessRules dailyWorkDataBusinessRules)
        {
            _mapper = mapper;
            _dailyWorkDataRepository = dailyWorkDataRepository;
            _dailyWorkDataBusinessRules = dailyWorkDataBusinessRules;
        }

        public async Task<UpdatedDailyWorkDataResponse> Handle(UpdateDailyWorkDataCommand request, CancellationToken cancellationToken)
        {
            DailyWorkData? dailyWorkData = await _dailyWorkDataRepository.GetAsync(predicate: dwd => dwd.Id == request.Id, cancellationToken: cancellationToken);
            await _dailyWorkDataBusinessRules.DailyWorkDataShouldExistWhenSelected(dailyWorkData);
            dailyWorkData = _mapper.Map(request, dailyWorkData);

            await _dailyWorkDataRepository.UpdateAsync(dailyWorkData!);

            UpdatedDailyWorkDataResponse response = _mapper.Map<UpdatedDailyWorkDataResponse>(dailyWorkData);
            return response;
        }
    }
}