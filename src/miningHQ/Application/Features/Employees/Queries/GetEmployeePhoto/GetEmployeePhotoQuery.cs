using Application.Services.Repositories;
using Application.Storage;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Employees.Queries.GetEmployeePhoto;

public class GetEmployeePhotoQuery : IRequest<GetEmployeePhotoResponse>
{
    public string EmployeeId { get; set; }

    public class GetEmployeePhotoQueryHandler : IRequestHandler<GetEmployeePhotoQuery, GetEmployeePhotoResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GetEmployeePhotoQueryHandler(IEmployeeRepository employeeRepository, IConfiguration configuration, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<GetEmployeePhotoResponse> Handle(GetEmployeePhotoQuery request, CancellationToken cancellationToken)
        {
            var photo = await _employeeRepository.GetEmployeePhoto(request.EmployeeId);
            
            if (photo == null)
            {
                return null; // veya boş response döndür
            }

            var response = _mapper.Map<GetEmployeePhotoResponse>(photo);
            
            // ⭐ URL'i oluştur
            var baseUrl = _configuration["StorageSettings:LocalStorageUrl"] ?? "http://localhost:5278";
            response.Url = $"{baseUrl}/{photo.Category}/{photo.Path}/{photo.Name}";
            
            return response;
        }
    }
}