// Application/Features/Employees/Queries/GetEmployeePhotoBase64/GetEmployeePhotoBase64Query.cs
using Application.Services.Repositories;
using Application.Storage;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Features.Employees.Queries.GetEmployeePhotoBase64;

public class GetEmployeePhotoBase64Query : IRequest<GetEmployeePhotoBase64Response>
{
    public string EmployeeId { get; set; }

    public class GetEmployeePhotoBase64QueryHandler : IRequestHandler<GetEmployeePhotoBase64Query, GetEmployeePhotoBase64Response>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IStorageFactory _storageFactory;
        private readonly ILogger<GetEmployeePhotoBase64QueryHandler> _logger;

        public GetEmployeePhotoBase64QueryHandler(
            IEmployeeRepository employeeRepository, 
            IConfiguration configuration, 
            IMapper mapper,
            IStorageFactory storageFactory,
            ILogger<GetEmployeePhotoBase64QueryHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _configuration = configuration;
            _mapper = mapper;
            _storageFactory = storageFactory;
            _logger = logger;
        }

        public async Task<GetEmployeePhotoBase64Response> Handle(GetEmployeePhotoBase64Query request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting employee photo base64 for employee: {EmployeeId}", request.EmployeeId);

                // Employee photo'yu al
                var photo = await _employeeRepository.GetEmployeePhoto(request.EmployeeId);
                
                if (photo == null)
                {
                    _logger.LogWarning("Employee photo not found for employee: {EmployeeId}", request.EmployeeId);
                    return new GetEmployeePhotoBase64Response
                    {
                        Success = false,
                        Message = "Employee photo not found"
                    };
                }

                // Storage service'i al (LocalStorage kullanacağız)
                var storageService = _storageFactory.GetStorageServiceByName(photo.Storage);
                
                // Base64 olarak dosyayı al
                var base64Result = await storageService.GetFileAsBase64Async(photo.Category, photo.Path, photo.Name);

                if (base64Result == null)
                {
                    _logger.LogWarning("Photo file not found in storage for employee: {EmployeeId}", request.EmployeeId);
                    return new GetEmployeePhotoBase64Response
                    {
                        Success = false,
                        Message = "Photo file not found in storage"
                    };
                }

                // Response'u oluştur ve map et
                var response = _mapper.Map<GetEmployeePhotoBase64Response>(photo);
                
                // Base64 ve ek bilgileri ekle
                response.Base64 = base64Result.Base64;
                response.MimeType = base64Result.MimeType;
                response.FileSize = base64Result.FileSize;
                response.Success = true;
                response.Message = "Photo retrieved successfully";

                // URL'i de oluştur
                var baseUrl = _configuration["StorageSettings:LocalStorageUrl"] ?? "http://localhost:5278";
                response.Url = $"{baseUrl}/{photo.Category}/{photo.Path}/{photo.Name}";

                _logger.LogInformation("Successfully converted photo to base64 for employee: {EmployeeId}", request.EmployeeId);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting employee photo base64 for employee: {EmployeeId}", request.EmployeeId);
                
                return new GetEmployeePhotoBase64Response
                {
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                };
            }
        }
    }
}