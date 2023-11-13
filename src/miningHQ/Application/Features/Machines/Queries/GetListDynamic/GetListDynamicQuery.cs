using Application.Features.Machines.Dtos;
using Application.Features.Machines.Queries.GetListDynamic;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListDynamic;

public class GetListDynamicQuery: IRequest<GetListResponse<GetListDynamicDto>>
{
    public DynamicQuery DynamicQuery { get; set; }
    
    public GetListDynamicQuery(DynamicQuery dynamicQuery)
    {
        DynamicQuery = dynamicQuery;
    }
    
    public class MachineRequestQueryHandler : IRequestHandler<GetListDynamicQuery, GetListResponse<GetListDynamicDto>>
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IMapper _mapper;

        public MachineRequestQueryHandler(IMachineRepository machineRepository, IMapper mapper)
        {
            _machineRepository = machineRepository;
            _mapper = mapper;
        }
        

        public async Task<GetListResponse<GetListDynamicDto>> Handle(GetListDynamicQuery request, CancellationToken cancellationToken)
        {
            var machines = await _machineRepository.GetAllByDynamicAsync(request.DynamicQuery);
            return _mapper.Map<GetListResponse<GetListDynamicDto>>(machines);
        }
    }
}