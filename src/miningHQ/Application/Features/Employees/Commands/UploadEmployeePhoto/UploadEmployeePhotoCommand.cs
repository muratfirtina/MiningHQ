using Application.Services.Repositories;
using Application.Storage;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Employees.Commands.UploadEmployeePhoto;

public class UploadEmployeePhotoCommand : IRequest<UploadEmployeePhotoResponse>
{
    public string Category { get; set; }
    public string FolderPath { get; set; }
    public IFormFile File { get; set; } // ⭐ EKSİK PROPERTİES EKLENDİ
    public string EmployeeId { get; set; }

    public class
        UploadEmployeePhotoCommandHandler : IRequestHandler<UploadEmployeePhotoCommand, UploadEmployeePhotoResponse>
    {
        private readonly IStorageFactory _storageFactory;
        private readonly IFileRepository _fileRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UploadEmployeePhotoCommandHandler(IStorageFactory storageFactory, IEmployeeRepository employeeRepository,
            IMapper mapper, IFileRepository fileRepository)
        {
            _storageFactory = storageFactory;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task<UploadEmployeePhotoResponse> Handle(UploadEmployeePhotoCommand request,
            CancellationToken cancellationToken)
        {
            // Employee validation
            Employee? employee = await _employeeRepository.GetAsync(e => e.Id == Guid.Parse(request.EmployeeId));
            if (employee == null)
            {
                throw new NotFoundException("Employee not found");
            }

            // File size validation
            if (request.File.Length > 5 * 1024 * 1024)
            {
                throw new BusinessException("File size must be less than 5MB");
            }

            // Get default storage service from factory
            var storageService = _storageFactory.GetDefaultStorageService();

            // File format validation (using storage service)
            await storageService.FileMustBeInImageFormat(request.File);

            // Delete existing photo if exists
            var existingPhoto = await _employeeRepository.GetEmployeePhoto(request.EmployeeId);
            if (existingPhoto != null)
            {
                // ⭐ Artık Path.Combine karışıklığı yok
                await storageService.DeleteAsync(Path.Combine(existingPhoto.Category, existingPhoto.Path,
                    existingPhoto.Name));
                await _fileRepository.DeleteAsync(existingPhoto);
            }

            // Upload new photo
            // ⭐ request.FolderPath kullanımı
            var uploadResults = await storageService.UploadAsync(request.Category, request.FolderPath,
                new List<IFormFile> { request.File });

            EmployeePhoto newPhoto = null;
            foreach (var uploadResult in uploadResults)
            {
                newPhoto = new EmployeePhoto
                {
                    Name = uploadResult.fileName,
                    Path = uploadResult.path,
                    Category = uploadResult.category,
                    Storage = uploadResult.storageType,
                    Employee = employee
                };

                await _fileRepository.AddAsync(newPhoto);
            }

            return new UploadEmployeePhotoResponse { Id = newPhoto?.Id.ToString(), EmployeeId = request.EmployeeId };
        }
    }
}