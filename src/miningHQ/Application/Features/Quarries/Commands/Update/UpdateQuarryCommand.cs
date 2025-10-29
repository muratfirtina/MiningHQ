using Application.Features.Quarries.Constants;
using Application.Features.Quarries.Rules;
using Application.Services.Repositories;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using System.Globalization;
using static Application.Features.Quarries.Constants.QuarriesOperationClaims;

namespace Application.Features.Quarries.Commands.Update;

public class UpdateQuarryCommand : IRequest<UpdatedQuarryResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    
    // Konum bilgileri (UTM 35T)
    public double? UtmEasting { get; set; }
    public double? UtmNorthing { get; set; }
    public double? Altitude { get; set; }
    public string? Pafta { get; set; }
    public string? CoordinateDescription { get; set; }
    
    public Guid? MiningEngineerId { get; set; }

    public string[] Roles => new[] { Admin, Write, QuarriesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey =>new[] {"GetQuarries"};

    public class UpdateQuarryCommandHandler : IRequestHandler<UpdateQuarryCommand, UpdatedQuarryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuarryRepository _quarryRepository;
        private readonly QuarryBusinessRules _quarryBusinessRules;

        public UpdateQuarryCommandHandler(IMapper mapper, IQuarryRepository quarryRepository,
                                         QuarryBusinessRules quarryBusinessRules)
        {
            _mapper = mapper;
            _quarryRepository = quarryRepository;
            _quarryBusinessRules = quarryBusinessRules;
        }

        public async Task<UpdatedQuarryResponse> Handle(UpdateQuarryCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "[UpdateQuarryCommand] Starting with UTM: Easting={0}, Northing={1}", 
                request.UtmEasting, request.UtmNorthing));
            
            Quarry? quarry = await _quarryRepository.GetAsync(predicate: q => q.Id == request.Id, cancellationToken: cancellationToken);
            await _quarryBusinessRules.QuarryShouldExistWhenSelected(quarry);
            
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "[UpdateQuarryCommand] Before mapping: quarry.Lat={0:F6}, quarry.Lon={1:F6}", 
                quarry!.Latitude, quarry.Longitude));
            
            quarry = _mapper.Map(request, quarry);
            
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "[UpdateQuarryCommand] After mapping: quarry.Lat={0:F6}, quarry.Lon={1:F6}", 
                quarry.Latitude, quarry.Longitude));
            
            // UTM koordinatlarını WGS84'e çevir (Google Maps için)
            if (request.UtmEasting.HasValue && request.UtmNorthing.HasValue)
            {
                Console.WriteLine("[UpdateQuarryCommand] Converting UTM to GPS...");
                
                var (latitude, longitude) = CoordinateConverter.SafeUtmToWgs84(
                    request.UtmEasting, 
                    request.UtmNorthing
                );
                
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                    "[UpdateQuarryCommand] Conversion result: Lat={0:F6}, Lon={1:F6}", 
                    latitude, longitude));
                
                quarry!.Latitude = latitude;
                quarry.Longitude = longitude;
                
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                    "[UpdateQuarryCommand] After setting: quarry.Lat={0:F6}, quarry.Lon={1:F6}", 
                    quarry.Latitude, quarry.Longitude));
            }
            else
            {
                Console.WriteLine("[UpdateQuarryCommand] UTM coordinates are null, skipping conversion");
            }

            await _quarryRepository.UpdateAsync(quarry!);
            
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "[UpdateQuarryCommand] After UpdateAsync: quarry.Lat={0:F6}, quarry.Lon={1:F6}", 
                quarry.Latitude, quarry.Longitude));

            UpdatedQuarryResponse response = _mapper.Map<UpdatedQuarryResponse>(quarry);
            
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "[UpdateQuarryCommand] Response: Lat={0:F6}, Lon={1:F6}", 
                response.Latitude, response.Longitude));

            return response;
        }
    }
}
