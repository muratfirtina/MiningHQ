using Application.Features.Files.Commands.Create;
using Application.Features.Files.Commands.Delete;
using Application.Features.Files.Commands.Update;
using Application.Features.Files.Queries.GetById;
using Application.Features.Files.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using File = Domain.Entities.File;

namespace Application.Features.Files.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<File, CreateFileCommand>().ReverseMap();
        CreateMap<File, CreatedFileResponse>().ReverseMap();
        CreateMap<File, UpdateFileCommand>().ReverseMap();
        CreateMap<File, UpdatedFileResponse>().ReverseMap();
        CreateMap<File, DeleteFileCommand>().ReverseMap();
        CreateMap<File, DeletedFileResponse>().ReverseMap();
        CreateMap<File, GetByIdFileResponse>().ReverseMap();
        CreateMap<File, GetListFileListItemDto>().ReverseMap();
        CreateMap<IPaginate<File>, GetListResponse<GetListFileListItemDto>>().ReverseMap();
    }
}