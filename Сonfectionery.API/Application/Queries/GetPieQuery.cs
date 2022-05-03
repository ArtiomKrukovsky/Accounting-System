using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Сonfectionery.API.Application.Constants;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.API.Application.ViewModels;
using Сonfectionery.Services.KSqlDb;

namespace Сonfectionery.API.Application.Queries
{
    [DataContract]
    public class GetPieQuery : IQuery<PieViewModel>
    {
        [DataMember]
        public Guid PieId { get; set; }

        public GetPieQuery(Guid pieId)
        {
            PieId = pieId;
        }
    }

    public class GetPieQueryHandler : IRequestHandler<GetPieQuery, PieViewModel>
    {
        private readonly IKSqlDbService<PieViewModel> _kSqlDbService;

        public GetPieQueryHandler(IKSqlDbService<PieViewModel> kSqlDbService)
        {
            _kSqlDbService = kSqlDbService ?? throw new ArgumentNullException(nameof(kSqlDbService));
        }

        public async Task<PieViewModel> Handle(GetPieQuery request, CancellationToken cancellationToken)
        {
            const string tableName = KafkaConstants.PiesTable;

            var pieId = request.PieId.ToString();
            var pie = await _kSqlDbService.GetAsync(tableName, pie => pie.Id == pieId);

            return pie;
        }
    }
}
