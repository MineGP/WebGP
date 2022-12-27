using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.GetTimedByUUID
{
    public record GetTimedByUUIDQuery(string? UUID) : IRequest<IDictionary<int, OnlineVM>>;

    public class GetTimedByUUIDQueryHandler : IRequestHandler<GetTimedByUUIDQuery, IDictionary<int, OnlineVM>>
    {
        private readonly IOnlineRepository _onlineRepository;
        public GetTimedByUUIDQueryHandler(IOnlineRepository onlineRepository)
        {
            this._onlineRepository = onlineRepository;
        }

        public async Task<IDictionary<int, OnlineVM>> Handle(GetTimedByUUIDQuery request, CancellationToken cancellationToken)
            => (await _onlineRepository.GetOnlineListAsync())
                .Where(v => request.UUID is not string uuid || v.UUID == uuid)
                .ToDictionary(v => v.TimedID, v => v);
    }
}
