using Application.Features.Timekeepings.Constants;
using Application.Features.Timekeepings.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Enums;
using MediatR;
using static Application.Features.Timekeepings.Constants.TimekeepingsOperationClaims;

namespace Application.Features.Timekeepings.Commands.Create;

public class CreateTimekeepingCommand : IRequest<CreatedTimekeepingResponse>
{
    public DateTime Date { get; set; }
    public Guid? EmployeeId { get; set; }
    public TimekeepingStatus Status { get; set; }

    public string[] Roles => new[] { Admin, Write, TimekeepingsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetTimekeepings";

    public class CreateTimekeepingCommandHandler : IRequestHandler<CreateTimekeepingCommand, CreatedTimekeepingResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITimekeepingRepository _timekeepingRepository;
        private readonly TimekeepingBusinessRules _timekeepingBusinessRules;

        public CreateTimekeepingCommandHandler(IMapper mapper, ITimekeepingRepository timekeepingRepository,
                                         TimekeepingBusinessRules timekeepingBusinessRules)
        {
            _mapper = mapper;
            _timekeepingRepository = timekeepingRepository;
            _timekeepingBusinessRules = timekeepingBusinessRules;
        }

        public async Task<CreatedTimekeepingResponse> Handle(CreateTimekeepingCommand request, CancellationToken cancellationToken)
        {
            // Aynı EmployeeId ve Date değerlerine sahip bir Timekeeping kaydını ara
            Timekeeping? existingTimekeeping = await _timekeepingRepository.GetAsync(
                t => t.EmployeeId == request.EmployeeId && t.Date.Date == request.Date.Date
            );

            Timekeeping timekeeping;

            if (existingTimekeeping != null)
            {
                // Eğer böyle bir kayıt varsa, status değerini güncelle
                existingTimekeeping.Status = request.Status;
                timekeeping = await _timekeepingRepository.UpdateAsync(existingTimekeeping);
            }
            else
            {
                // Eğer böyle bir kayıt yoksa, yeni bir kayıt oluştur
                timekeeping = _mapper.Map<Timekeeping>(request);
                timekeeping = await _timekeepingRepository.AddAsync(timekeeping);
            }

            CreatedTimekeepingResponse response = _mapper.Map<CreatedTimekeepingResponse>(timekeeping);
            return response;
            
            
            /*Timekeeping timekeeping = _mapper.Map<Timekeeping>(request);

            await _timekeepingRepository.AddAsync(timekeeping);

            CreatedTimekeepingResponse response = _mapper.Map<CreatedTimekeepingResponse>(timekeeping);
            return response;*/
        }
    }
}