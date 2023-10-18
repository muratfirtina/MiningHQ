using Application.Features.DailyWorkDatas.Constants;
using Application.Features.DailyWorkDatas.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.DailyWorkDatas.Constants.DailyWorkDatasOperationClaims;

namespace Application.Features.DailyWorkDatas.Queries.GetById;

public class GetByIdDailyWorkDataQuery : IRequest<GetByIdDailyWorkDataResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdDailyWorkDataQueryHandler : IRequestHandler<GetByIdDailyWorkDataQuery, GetByIdDailyWorkDataResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDailyWorkDataRepository _dailyWorkDataRepository;
        private readonly DailyWorkDataBusinessRules _dailyWorkDataBusinessRules;

        public GetByIdDailyWorkDataQueryHandler(IMapper mapper, IDailyWorkDataRepository dailyWorkDataRepository, DailyWorkDataBusinessRules dailyWorkDataBusinessRules)
        {
            _mapper = mapper;
            _dailyWorkDataRepository = dailyWorkDataRepository;
            _dailyWorkDataBusinessRules = dailyWorkDataBusinessRules;
        }

        public async Task<GetByIdDailyWorkDataResponse> Handle(GetByIdDailyWorkDataQuery request, CancellationToken cancellationToken)
        {
            DailyWorkData? dailyWorkData = await _dailyWorkDataRepository.GetAsync(predicate: dwd => dwd.Id == request.Id, cancellationToken: cancellationToken);
            await _dailyWorkDataBusinessRules.DailyWorkDataShouldExistWhenSelected(dailyWorkData);

            GetByIdDailyWorkDataResponse response = _mapper.Map<GetByIdDailyWorkDataResponse>(dailyWorkData);
            return response;
        }
    }
}