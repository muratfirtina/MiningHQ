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

namespace Application.Features.DailyWorkDatas.Commands.Create;

public class CreateDailyWorkDataCommand : IRequest<CreatedDailyWorkDataResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public DateTime Date { get; set; }
    public int WorkingHoursOrKm { get; set; }
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }

    public string[] Roles => new[] { Admin, Write, DailyWorkDatasOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetDailyWorkDatas"};

    public class CreateDailyWorkDataCommandHandler : IRequestHandler<CreateDailyWorkDataCommand, CreatedDailyWorkDataResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDailyWorkDataRepository _dailyWorkDataRepository;
        private readonly DailyWorkDataBusinessRules _dailyWorkDataBusinessRules;

        public CreateDailyWorkDataCommandHandler(IMapper mapper, IDailyWorkDataRepository dailyWorkDataRepository,
                                         DailyWorkDataBusinessRules dailyWorkDataBusinessRules)
        {
            _mapper = mapper;
            _dailyWorkDataRepository = dailyWorkDataRepository;
            _dailyWorkDataBusinessRules = dailyWorkDataBusinessRules;
        }

        public async Task<CreatedDailyWorkDataResponse> Handle(CreateDailyWorkDataCommand request, CancellationToken cancellationToken)
        {
            DailyWorkData dailyWorkData = _mapper.Map<DailyWorkData>(request);

            await _dailyWorkDataRepository.AddAsync(dailyWorkData);

            CreatedDailyWorkDataResponse response = _mapper.Map<CreatedDailyWorkDataResponse>(dailyWorkData);
            return response;
        }
    }
}