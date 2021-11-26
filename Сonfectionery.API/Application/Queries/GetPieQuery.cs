using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using MediatR;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.API.Application.ViewModels;
using Сonfectionery.Domain.Aggregates.PieAggregate;

namespace Сonfectionery.API.Application.Queries
{
    [DataContract]
    public class GetPieQuery : IQuery<PieViewModel>
    {
        [DataMember]
        public Guid PieId { get; set; }
    }

    public class GetPieQueryHandler : IRequestHandler<GetPieQuery, PieViewModel>
    {
        private readonly IPieRepository _pieRepository;
        private readonly IMapper _mapper;

        public GetPieQueryHandler(IPieRepository pieRepository, IMapper mapper)
        {
            _pieRepository = pieRepository;
            _mapper = mapper;
        }

        public async Task<PieViewModel> Handle(GetPieQuery request, CancellationToken cancellationToken)
        {
            var pie = await _pieRepository.GetAsync(request.PieId);

            return _mapper.Map<PieViewModel>(pie);
        }
    }
}
