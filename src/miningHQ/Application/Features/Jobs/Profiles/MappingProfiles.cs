using Application.Features.Jobs.Commands.Create;
using Application.Features.Jobs.Commands.Delete;
using Application.Features.Jobs.Commands.Update;
using Application.Features.Jobs.Queries.GetById;
using Application.Features.Jobs.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Jobs.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Job, CreateJobCommand>().ReverseMap();
        CreateMap<Job, CreatedJobResponse>().ReverseMap();
        CreateMap<Job, UpdateJobCommand>().ReverseMap();
        CreateMap<Job, UpdatedJobResponse>().ReverseMap();
        CreateMap<Job, DeleteJobCommand>().ReverseMap();
        CreateMap<Job, DeletedJobResponse>().ReverseMap();
        CreateMap<Job, GetByIdJobResponse>().ReverseMap();
        CreateMap<Job, GetListJobListItemDto>().ReverseMap();
        CreateMap<IPaginate<Job>, GetListResponse<GetListJobListItemDto>>().ReverseMap();
    }
}