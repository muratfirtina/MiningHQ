// Application/Features/Employees/Profiles/MappingProfiles.cs - Mevcut profile'a bu mapping'i ekleyin

using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Commands.Delete;
using Application.Features.Employees.Commands.Update;
using Application.Features.Employees.Commands.UpdateShowcase;
using Application.Features.Employees.Commands.UploadEmployeeFile;
using Application.Features.Employees.Dtos;
using Application.Features.Employees.Queries.GetById;
using Application.Features.Employees.Queries.GetEmployeePhoto;
using Application.Features.Employees.Queries.GetEmployeePhotoBase64; // ⭐ YENİ
using Application.Features.Employees.Queries.GetFilesByEmployeeId;
using Application.Features.Employees.Queries.GetList;
using Application.Features.Employees.Queries.GetList.ShortDetail;
using Application.Features.Employees.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Employees.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Mevcut mapping'ler aynı kalacak...
        CreateMap<Employee, CreateEmployeeCommand>().ReverseMap();
        CreateMap<Employee, CreatedEmployeeResponse>().ReverseMap();
        CreateMap<Employee, UpdateEmployeeCommand>().ReverseMap();
        CreateMap<Employee, UpdatedEmployeeResponse>().ReverseMap();
        CreateMap<Employee, DeleteEmployeeCommand>().ReverseMap();
        CreateMap<Employee, DeletedEmployeeResponse>().ReverseMap();
        CreateMap<Employee, GetByIdEmployeeResponse>()
            .ForMember(dest => dest.TotalUsedLeaveDays, opt => opt.MapFrom(src => src.EmployeeLeaveUsages.Count))
            .ForMember(dest => dest.TotalEntitledLeaveDays, opt => opt.MapFrom(src => src.EmployeeLeaveUsages.Count))
            .ForMember(dest => dest.EmployeeFiles, opt => opt.MapFrom(src => src.EmployeeFiles))
            .ReverseMap();
        CreateMap<Employee, GetListEmployeeListItemDto>().ReverseMap();
        
        CreateMap<Employee, GetListEmployeeListItemDto>()
            .ForMember(dest => dest.Timekeepings, opt => opt.MapFrom(src => src.Timekeepings))
            .ReverseMap();
        
        CreateMap<Timekeeping, TimekeepingDto>().ReverseMap();
        
        // Mevcut EmployeePhoto mapping
        CreateMap<EmployeePhoto, GetEmployeePhotoResponse>()
            .ForMember(dest => dest.Url, opt => opt.Ignore());

        // ⭐ YENİ: EmployeePhoto -> GetEmployeePhotoBase64Response mapping
        CreateMap<EmployeePhoto, GetEmployeePhotoBase64Response>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId.ToString()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path))
            .ForMember(dest => dest.Storage, opt => opt.MapFrom(src => src.Storage))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
            .ForMember(dest => dest.Base64, opt => opt.Ignore()) // Handler'da set edilecek
            .ForMember(dest => dest.MimeType, opt => opt.Ignore()) // Handler'da set edilecek
            .ForMember(dest => dest.FileSize, opt => opt.Ignore()) // Handler'da set edilecek
            .ForMember(dest => dest.Success, opt => opt.Ignore()) // Handler'da set edilecek
            .ForMember(dest => dest.Message, opt => opt.Ignore()) // Handler'da set edilecek
            .ForMember(dest => dest.Url, opt => opt.Ignore()); // Handler'da set edilecek

        CreateMap<IPaginate<Employee>, GetListResponse<GetListEmployeeListItemDto>>().ReverseMap();
        
        CreateMap<IPaginate<Employee>, GetListResponse<GetListByDynamicEmployeeListItemDto>>().ReverseMap();

        CreateMap<Employee, GetListByDynamicEmployeeListItemDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.JobName, opt => opt.MapFrom(src => src.Job.Name))
            .ForMember(dest => dest.QuarryName, opt => opt.MapFrom(src => src.Quarry.Name))
            .ForMember(dest => dest.TypeOfBlood, opt => opt.MapFrom(src => src.TypeOfBlood))
            .ForMember(dest => dest.EmergencyContact, opt => opt.MapFrom(src => src.EmergencyContact))
            .ReverseMap();

        CreateMap<EmployeeFile, UploadEmployeeFileDto>()
            .ReverseMap();

        CreateMap<IPaginate<Employee>, GetListResponse<GetListByEmplooyeeShortDetailItemDto>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();

        CreateMap<Employee, GetListByEmplooyeeShortDetailItemDto>()
            .ReverseMap();

        CreateMap<UpdateShowcaseCommand, UpdateShowcaseResponse>()
            .ReverseMap();
        
        CreateMap<EmployeeFile, GetEmployeeFilesDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Showcase, opt => opt.MapFrom(src => src.Showcase))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ReverseMap();
    }
}