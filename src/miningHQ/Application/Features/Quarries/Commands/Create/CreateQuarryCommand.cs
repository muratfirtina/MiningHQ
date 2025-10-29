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

namespace Application.Features.Quarries.Commands.Create;

public class CreateQuarryCommand : IRequest<CreatedQuarryResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
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

    public string[] Roles => new[] { Admin, Write, QuarriesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey =>new[] {"GetQuarries"};

    public class CreateQuarryCommandHandler : IRequestHandler<CreateQuarryCommand, CreatedQuarryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuarryRepository _quarryRepository;
        private readonly QuarryBusinessRules _quarryBusinessRules;

        public CreateQuarryCommandHandler(IMapper mapper, IQuarryRepository quarryRepository,
                                         QuarryBusinessRules quarryBusinessRules)
        {
            _mapper = mapper;
            _quarryRepository = quarryRepository;
            _quarryBusinessRules = quarryBusinessRules;
        }

        public async Task<CreatedQuarryResponse> Handle(CreateQuarryCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "[CreateQuarryCommand] Starting with UTM: Easting={0}, Northing={1}", 
                request.UtmEasting, request.UtmNorthing));
            
            Quarry quarry = _mapper.Map<Quarry>(request);
            
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "[CreateQuarryCommand] After mapping: Lat={0}, Lon={1}", 
                quarry.Latitude, quarry.Longitude));
            
            // UTM koordinatlarını WGS84'e çevir (Google Maps için)
            if (request.UtmEasting.HasValue && request.UtmNorthing.HasValue)
            {
                Console.WriteLine("[CreateQuarryCommand] Converting UTM to GPS...");
                
                var (latitude, longitude) = CoordinateConverter.SafeUtmToWgs84(
                    request.UtmEasting, 
                    request.UtmNorthing
                );
                
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                    "[CreateQuarryCommand] Conversion result: Lat={0:F6}, Lon={1:F6}", 
                    latitude, longitude));
                
                quarry.Latitude = latitude;
                quarry.Longitude = longitude;
                
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                    "[CreateQuarryCommand] After setting: quarry.Lat={0:F6}, quarry.Lon={1:F6}", 
                    quarry.Latitude, quarry.Longitude));
            }
            else
            {
                Console.WriteLine("[CreateQuarryCommand] UTM coordinates are null, skipping conversion");
            }

            await _quarryRepository.AddAsync(quarry);
            
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "[CreateQuarryCommand] After AddAsync: quarry.Lat={0:F6}, quarry.Lon={1:F6}", 
                quarry.Latitude, quarry.Longitude));

            CreatedQuarryResponse response = _mapper.Map<CreatedQuarryResponse>(quarry);
            
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "[CreateQuarryCommand] Response: Lat={0:F6}, Lon={1:F6}", 
                response.Latitude, response.Longitude));
            
            return response;
        }
    }
}
