using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.Mapper;
using WebGP.Application.Common.VM;
using WebGP.Application.Data.Queries.GetOnlineLogByUserName;

namespace WebGP.Application.Data.Queries.GetOnlineLogListByStaticID
{
    public record GetOnlineLogListByStaticIDQuery(uint StaticID, DateTime From, DateTime To) : IRequest<IEnumerable<OnlineLogVM>>;
    public class GetOnlineLogListByStaticIDQueryHandler : IRequestHandler<GetOnlineLogListByStaticIDQuery, IEnumerable<OnlineLogVM>>
    {
        private readonly IOnlineRepository _onlineRepository;
        public GetOnlineLogListByStaticIDQueryHandler(IOnlineRepository onlineRepository)
        {
            this._onlineRepository = onlineRepository;
        }

        public async Task<IEnumerable<OnlineLogVM>> Handle(GetOnlineLogListByStaticIDQuery request, CancellationToken cancellationToken)
            => (await _onlineRepository.GetOnlineLogListByStaticIDAsync(request.StaticID, request.From, request.To)).Select(v => v.MapToVM());
    }
}
