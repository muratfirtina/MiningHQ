using Application.Services.Repositories;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Globalization;

namespace Application.Features.QuarryProductions.Commands.Create;

public class CreateQuarryProductionCommand : IRequest<CreatedQuarryProductionResponse>
{
    public Guid QuarryId { get; set; }
    public DateTime WeekStartDate { get; set; }
    public DateTime WeekEndDate { get; set; }
    public decimal ProductionAmount { get; set; }
    public string? ProductionUnit { get; set; }
    public decimal StockAmount { get; set; }
    public string? StockUnit { get; set; }
    public decimal SalesAmount { get; set; }
    public string? SalesUnit { get; set; }
    public string? Notes { get; set; }
    
    // Konum bilgileri (UTM 35T)
    public double? UtmEasting { get; set; }
    public double? UtmNorthing { get; set; }
    public double? Altitude { get; set; }
    public string? Pafta { get; set; }
    public string? CoordinateDescription { get; set; }

    public class CreateQuarryProductionCommandHandler : IRequestHandler<CreateQuarryProductionCommand, CreatedQuarryProductionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuarryProductionRepository _quarryProductionRepository;

        public CreateQuarryProductionCommandHandler(IMapper mapper, IQuarryProductionRepository quarryProductionRepository)
        {
            _mapper = mapper;
            _quarryProductionRepository = quarryProductionRepository;
        }

        public async Task<CreatedQuarryProductionResponse> Handle(CreateQuarryProductionCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "[CreateQuarryProduction] Starting with UTM: Easting={0}, Northing={1}", 
                request.UtmEasting, request.UtmNorthing));
            
            QuarryProduction quarryProduction = _mapper.Map<QuarryProduction>(request);
            quarryProduction.Id = Guid.NewGuid();
            
            // UTM koordinatlarını WGS84'e çevir (Google Maps için)
            if (request.UtmEasting.HasValue && request.UtmNorthing.HasValue)
            {
                Console.WriteLine("[CreateQuarryProduction] Converting UTM to GPS...");
                
                var (latitude, longitude) = CoordinateConverter.SafeUtmToWgs84(
                    request.UtmEasting, 
                    request.UtmNorthing
                );
                
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                    "[CreateQuarryProduction] Conversion result: Lat={0:F6}, Lon={1:F6}", 
                    latitude, longitude));
                
                quarryProduction.Latitude = latitude;
                quarryProduction.Longitude = longitude;
                
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                    "[CreateQuarryProduction] After setting: Lat={0:F6}, Lon={1:F6}", 
                    quarryProduction.Latitude, quarryProduction.Longitude));
            }

            await _quarryProductionRepository.AddAsync(quarryProduction);

            CreatedQuarryProductionResponse response = _mapper.Map<CreatedQuarryProductionResponse>(quarryProduction);
            
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture,
                "[CreateQuarryProduction] Response: Lat={0:F6}, Lon={1:F6}", 
                response.Latitude, response.Longitude));
            
            return response;
        }
    }
}
