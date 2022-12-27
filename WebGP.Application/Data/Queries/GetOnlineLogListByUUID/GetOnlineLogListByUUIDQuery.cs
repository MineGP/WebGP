using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.Mapper;
using WebGP.Application.Common.VM;
using WebGP.Application.Data.Queries.GetOnlineLogListByStaticID;

namespace WebGP.Application.Data.Queries.GetOnlineLogListByUUID
{
    public record GetOnlineLogListByUUIDQuery(string UUID, DateTime From, DateTime To) : IRequest<IEnumerable<OnlineLogVM>>;
    public class GetOnlineLogListByUUIDQueryHandler : IRequestHandler<GetOnlineLogListByUUIDQuery, IEnumerable<OnlineLogVM>>
    {
        private readonly IOnlineRepository _onlineRepository;
        public GetOnlineLogListByUUIDQueryHandler(IOnlineRepository onlineRepository)
        {
            this._onlineRepository = onlineRepository;
        }

        public async Task<IEnumerable<OnlineLogVM>> Handle(GetOnlineLogListByUUIDQuery request, CancellationToken cancellationToken)
            => (await _onlineRepository.GetOnlineLogListByUuidAsync(request.UUID, request.From, request.To)).Select(v => v.MapToVM());
    }
}
