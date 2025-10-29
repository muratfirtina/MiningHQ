using Application.Features.Quarries.Commands.Create;
using Application.Features.Quarries.Commands.Delete;
using Application.Features.Quarries.Commands.Update;
using Application.Features.Quarries.Queries.GetById;
using Application.Features.Quarries.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using GetByIdDtos = Application.Features.Quarries.Queries.GetById;
using GetListDtos = Application.Features.Quarries.Queries.GetList;

namespace Application.Features.Quarries.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // CRITICAL: Latitude ve Longitude otomatik hesaplanacak, AutoMapper bunları null yapmasın!
        CreateMap<CreateQuarryCommand, Quarry>()
            .ForMember(dest => dest.Latitude, opt => opt.Ignore())
            .ForMember(dest => dest.Longitude, opt => opt.Ignore());
        
        CreateMap<Quarry, CreateQuarryCommand>();
        CreateMap<Quarry, CreatedQuarryResponse>();
        
        // CRITICAL: Update için de aynı şekilde ignore et
        CreateMap<UpdateQuarryCommand, Quarry>()
            .ForMember(dest => dest.Latitude, opt => opt.Ignore())
            .ForMember(dest => dest.Longitude, opt => opt.Ignore());
        
        CreateMap<Quarry, UpdateQuarryCommand>();
        CreateMap<Quarry, UpdatedQuarryResponse>();
        
        CreateMap<Quarry, DeleteQuarryCommand>().ReverseMap();
        CreateMap<Quarry, DeletedQuarryResponse>().ReverseMap();
        
        // GetById Mapping - Kullanılan DTO'lar GetByIdQuarryResponse.cs içinde tanımlı
        CreateMap<Quarry, GetByIdQuarryResponse>()
            .ForMember(dest => dest.MiningEngineer, opt => opt.MapFrom(src => src.MiningEngineer != null ? new GetByIdDtos.MiningEngineerDto
            {
                Id = src.MiningEngineer.Id,
                FirstName = src.MiningEngineer.FirstName,
                LastName = src.MiningEngineer.LastName,
                Phone = src.MiningEngineer.Phone
            } : null))
            .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees != null ? src.Employees.Select(e => new GetByIdDtos.EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                JobName = e.Job != null ? e.Job.Name : null,
                DepartmentName = e.Department != null ? e.Department.Name : null,
                Phone = e.Phone
            }).ToList() : null))
            .ForMember(dest => dest.Machines, opt => opt.MapFrom(src => src.Machines != null ? src.Machines.Select(m => new GetByIdDtos.MachineDto
            {
                Id = m.Id,
                Name = m.Name,
                SerialNumber = m.SerialNumber,
                MachineTypeName = m.MachineType != null ? m.MachineType.Name : null,
                BrandName = m.Model != null && m.Model.Brand != null ? m.Model.Brand.Name : null,
                ModelName = m.Model != null ? m.Model.Name : null
            }).ToList() : null))
            .ForMember(dest => dest.QuarryFiles, opt => opt.MapFrom(src => src.QuarryFiles != null ? src.QuarryFiles.Select(f => new GetByIdDtos.QuarryFileDto
            {
                Id = f.Id,
                FileName = f.Name,
                Path = f.Path,
                Storage = f.Storage
            }).ToList() : null))
            .ForMember(dest => dest.QuarryProductions, opt => opt.MapFrom(src => src.QuarryProductions != null ? src.QuarryProductions.Select(p => new GetByIdDtos.QuarryProductionDto
            {
                Id = p.Id,
                WeekStartDate = p.WeekStartDate,
                WeekEndDate = p.WeekEndDate,
                ProductionAmount = p.ProductionAmount,
                ProductionUnit = p.ProductionUnit,
                StockAmount = p.StockAmount,
                StockUnit = p.StockUnit,
                SalesAmount = p.SalesAmount,
                SalesUnit = p.SalesUnit,
                Notes = p.Notes,
                // Konum bilgileri (UTM 35T)
                UtmEasting = p.UtmEasting,
                UtmNorthing = p.UtmNorthing,
                Altitude = p.Altitude,
                Pafta = p.Pafta,
                // GPS koordinatları
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                CoordinateDescription = p.CoordinateDescription
            }).ToList() : null));
        
        CreateMap<GetByIdQuarryResponse, Quarry>();
        
        // GetList Mapping - Kullanılan DTO'lar GetListQuarryListItemDto.cs içinde tanımlı
        CreateMap<Quarry, GetListQuarryListItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.MiningEngineerId, opt => opt.MapFrom(src => src.MiningEngineerId.HasValue ? src.MiningEngineerId.Value.ToString() : null))
            .ForMember(dest => dest.MiningEngineer, opt => opt.MapFrom(src => src.MiningEngineer != null ? new GetListDtos.MiningEngineerListDto
            {
                Id = src.MiningEngineer.Id.ToString(),
                FirstName = src.MiningEngineer.FirstName,
                LastName = src.MiningEngineer.LastName,
                Phone = src.MiningEngineer.Phone
            } : null))
            .ForMember(dest => dest.EmployeeCount, opt => opt.MapFrom(src => src.Employees != null ? src.Employees.Count : 0))
            .ForMember(dest => dest.MachineCount, opt => opt.MapFrom(src => src.Machines != null ? src.Machines.Count : 0));
        
        CreateMap<IPaginate<Quarry>, GetListResponse<GetListQuarryListItemDto>>().ReverseMap();
    }
}
