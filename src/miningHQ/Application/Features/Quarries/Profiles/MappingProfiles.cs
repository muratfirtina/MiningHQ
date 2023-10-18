using Application.Features.Quarries.Commands.Create;
using Application.Features.Quarries.Commands.Delete;
using Application.Features.Quarries.Commands.Update;
using Application.Features.Quarries.Queries.GetById;
using Application.Features.Quarries.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Quarries.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Quarry, CreateQuarryCommand>().ReverseMap();
        CreateMap<Quarry, CreatedQuarryResponse>().ReverseMap();
        CreateMap<Quarry, UpdateQuarryCommand>().ReverseMap();
        CreateMap<Quarry, UpdatedQuarryResponse>().ReverseMap();
        CreateMap<Quarry, DeleteQuarryCommand>().ReverseMap();
        CreateMap<Quarry, DeletedQuarryResponse>().ReverseMap();
        CreateMap<Quarry, GetByIdQuarryResponse>().ReverseMap();
        CreateMap<Quarry, GetListQuarryListItemDto>().ReverseMap();
        CreateMap<IPaginate<Quarry>, GetListResponse<GetListQuarryListItemDto>>().ReverseMap();
    }
}