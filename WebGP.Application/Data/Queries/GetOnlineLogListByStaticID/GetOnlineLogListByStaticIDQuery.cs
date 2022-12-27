using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.Mapper;
using WebGP.Application.Common.VM;

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
