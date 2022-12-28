using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.Mapper;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Online.Queries.GetOnlineLogListByUUID
{
    public record GetOnlineLogListByUUIDQuery(string UUID, DateTime From, DateTime To) : IRequest<IEnumerable<OnlineLogVM>>;
    public class GetOnlineLogListByUUIDQueryHandler : IRequestHandler<GetOnlineLogListByUUIDQuery, IEnumerable<OnlineLogVM>>
    {
        private readonly IOnlineRepository _onlineRepository;
        public GetOnlineLogListByUUIDQueryHandler(IOnlineRepository onlineRepository)
        {
            _onlineRepository = onlineRepository;
        }

        public async Task<IEnumerable<OnlineLogVM>> Handle(GetOnlineLogListByUUIDQuery request, CancellationToken cancellationToken)
            => (await _onlineRepository.GetOnlineLogListByUuidAsync(request.UUID, request.From, request.To)).Select(v => v.MapToVM());
    }
}
